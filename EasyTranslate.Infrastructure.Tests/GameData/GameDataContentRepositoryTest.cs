namespace EasyTranslate.Infrastructure.Tests.GameData;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.GameData;
using Moq;
using Xunit;
using Lumina_Language = Lumina.Data.Language;

public class GameDataContentRepositoryTest
{
    private const string SearchName = "popoto";
    private static readonly Content Content1 = new(ContentType.Item, 25204, "Popoto", "Popoto", "Toffel", "ポポト");

    private static readonly Content Content2 = new(
        ContentType.Item,
        27455,
        "Popoto Set",
        "Tubercule de popoto",
        "Toffel-Knollen",
        "ポポトの種芋");

    private static readonly Content Content3 = new(
        ContentType.Emote,
        64371,
        "Popoto Step",
        "Popping popoto",
        "Toffel-Tanz",
        "ポポトステップ");

    private readonly CancellationToken fakeCancellationToken = CancellationToken.None;

    private readonly GameDataContentRepository gameDataContentRepository;
    private readonly Mock<ISearchByNameQuery> mockSearchQuery1;
    private readonly Mock<ISearchByNameQuery> mockSearchQuery2;
    private readonly Mock<ISearchByNameQuery> mockSearchQuery3;

    public GameDataContentRepositoryTest()
    {
        mockSearchQuery1 = new Mock<ISearchByNameQuery>();
        mockSearchQuery2 = new Mock<ISearchByNameQuery>();
        mockSearchQuery3 = new Mock<ISearchByNameQuery>();

        gameDataContentRepository = new GameDataContentRepository(
            new List<ISearchByNameQuery>
            {
                mockSearchQuery1.Object,
                mockSearchQuery2.Object,
                mockSearchQuery3.Object,
            });
    }

    [Fact]
    public async Task ResultsFromManyQueries_SearchByName_CombinesResultsAndRemovesDuplicates()
    {
        const Language searchLanguage = Language.English;
        const Lumina_Language luminaLanguage = Lumina_Language.English;
        GivenSearchNameAndLanguageQueriesWillReturn(SearchName, luminaLanguage, [Content1, Content3], [Content2], []);

        var result = await gameDataContentRepository.SearchByName(SearchName, searchLanguage, fakeCancellationToken);

        Assert.Equivalent(new List<Content> { Content1, Content2, Content3 }, result, true);
    }

    [Fact]
    public async Task NoResultsFromQueries_SearchByName_ReturnsEmptyEnumerable()
    {
        const Language searchLanguage = Language.English;
        const Lumina_Language luminaLanguage = Lumina_Language.English;
        GivenSearchNameAndLanguageQueriesWillReturn(SearchName, luminaLanguage, [], [], []);

        var result = await gameDataContentRepository.SearchByName(SearchName, searchLanguage, fakeCancellationToken);

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
        GivenSearchNameAndLanguageQueriesWillReturn(SearchName, luminaLanguage, [Content1, Content3], [Content2], []);

        var results = await gameDataContentRepository.SearchByName(SearchName, searchLanguage, fakeCancellationToken);

        _ = results.ToList(); // Enumeration required to verify query execution
        mockSearchQuery1.Verify(query => query.Execute(SearchName, luminaLanguage));
        mockSearchQuery2.Verify(query => query.Execute(SearchName, luminaLanguage));
        mockSearchQuery3.Verify(query => query.Execute(SearchName, luminaLanguage));
    }

    private void GivenSearchNameAndLanguageQueriesWillReturn(
        string searchName,
        Lumina_Language searchLanguage,
        IEnumerable<Content> query1Result,
        IEnumerable<Content> query2Result,
        IEnumerable<Content> query3Result
    )
    {
        mockSearchQuery1.Setup(query => query.Execute(searchName, searchLanguage)).Returns(query1Result);
        mockSearchQuery2.Setup(query => query.Execute(searchName, searchLanguage)).Returns(query2Result);
        mockSearchQuery3.Setup(query => query.Execute(searchName, searchLanguage)).Returns(query3Result);
    }
}
