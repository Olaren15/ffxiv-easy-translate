using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData;
using Moq;
using Xunit;

namespace EasyTranslate.Infrastructure.Tests.GameData;

using Language = Language;
using Lumina_Language = Lumina.Data.Language;

public class GameDataContentRepositoryTest
{
    private const string SearchName = "popoto";
    private readonly Content _content1 = new(ContentType.Item, 25204, "Popoto", "Popoto", "Toffel", "ポポト");

    private readonly Content _content2 = new(
        ContentType.Item,
        27455,
        "Popoto Set",
        "Tubercule de popoto",
        "Toffel-Knollen",
        "ポポトの種芋");

    private readonly Content _content3 = new(
        ContentType.Emote,
        64371,
        "Popoto Step",
        "Popping popoto",
        "Toffel-Tanz",
        "ポポトステップ");

    private readonly CancellationToken _fakeCancellationToken = CancellationToken.None;

    private readonly GameDataContentRepository _gameDataContentRepository;
    private readonly Mock<ISearchByNameQuery> _mockSearchQuery1;
    private readonly Mock<ISearchByNameQuery> _mockSearchQuery2;
    private readonly Mock<ISearchByNameQuery> _mockSearchQuery3;

    public GameDataContentRepositoryTest()
    {
        _mockSearchQuery1 = new Mock<ISearchByNameQuery>();
        _mockSearchQuery2 = new Mock<ISearchByNameQuery>();
        _mockSearchQuery3 = new Mock<ISearchByNameQuery>();

        _gameDataContentRepository = new GameDataContentRepository(
            new List<ISearchByNameQuery>
            {
                _mockSearchQuery1.Object, _mockSearchQuery2.Object, _mockSearchQuery3.Object
            });
    }

    [Fact]
    public async Task ResultsFromManyQueries_SearchByName_CombinesResultsAndRemovesDuplicates()
    {
        const Language searchLanguage = Language.English;
        const Lumina_Language luminaLanguage = Lumina_Language.English;
        GivenSearchNameAndLanguageQueriesWillReturn(
            SearchName,
            luminaLanguage,
            [_content1, _content3],
            [_content2],
            []
        );

        IEnumerable<Content> result = await _gameDataContentRepository.SearchByName(
            SearchName,
            searchLanguage,
            _fakeCancellationToken
        );

        Assert.Equivalent(new List<Content> { _content1, _content2, _content3 }, result, true);
    }

    [Fact]
    public async Task NoResultsFromQueries_SearchByName_ReturnsEmptyEnumerable()
    {
        const Language searchLanguage = Language.English;
        const Lumina_Language luminaLanguage = Lumina_Language.English;
        GivenSearchNameAndLanguageQueriesWillReturn(SearchName, luminaLanguage, [], [], []);

        IEnumerable<Content> result = await _gameDataContentRepository.SearchByName(
            SearchName,
            searchLanguage,
            _fakeCancellationToken
        );

        Assert.Empty(result);
    }

    [Theory]
    [InlineData(Language.English, Lumina_Language.English)]
    [InlineData(Language.French, Lumina_Language.French)]
    [InlineData(Language.German, Lumina_Language.German)]
    [InlineData(Language.Japanese, Lumina_Language.Japanese)]
    public async Task SearchLanguage_SearchByName_ExecutesQueriesWithAppropriateLuminaLanguage(
        Language searchLanguage,
        Lumina_Language luminaLanguage
    )
    {
        GivenSearchNameAndLanguageQueriesWillReturn(
            SearchName,
            luminaLanguage,
            [_content1, _content3],
            [_content2],
            []
        );

        IEnumerable<Content> results = await _gameDataContentRepository.SearchByName(
            SearchName,
            searchLanguage,
            _fakeCancellationToken
        );

        _ = results.ToList(); // Enumeration required to verify query execution
        _mockSearchQuery1.Verify(query => query.Execute(SearchName, luminaLanguage));
        _mockSearchQuery2.Verify(query => query.Execute(SearchName, luminaLanguage));
        _mockSearchQuery3.Verify(query => query.Execute(SearchName, luminaLanguage));
    }

    private void GivenSearchNameAndLanguageQueriesWillReturn(
        string searchName,
        Lumina_Language searchLanguage,
        IEnumerable<Content> query1Result,
        IEnumerable<Content> query2Result,
        IEnumerable<Content> query3Result
    )
    {
        _mockSearchQuery1.Setup(query => query.Execute(searchName, searchLanguage)).Returns(query1Result);
        _mockSearchQuery2.Setup(query => query.Execute(searchName, searchLanguage)).Returns(query2Result);
        _mockSearchQuery3.Setup(query => query.Execute(searchName, searchLanguage)).Returns(query3Result);
    }
}
