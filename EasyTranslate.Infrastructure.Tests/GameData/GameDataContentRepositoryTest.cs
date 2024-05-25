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
        const string searchName = "popoto";
        const Language searchLanguage = Language.English;
        const Lumina_Language luminaLanguage = Lumina_Language.English;
        var cancellationToken = CancellationToken.None;
        mockSearchQuery1.Setup(query => query.Execute(searchName, luminaLanguage))
                        .Returns([Content1, Content3]);
        mockSearchQuery2.Setup(query => query.Execute(searchName, luminaLanguage)).Returns([Content2]);
        mockSearchQuery3.Setup(query => query.Execute(searchName, luminaLanguage)).Returns([]);

        var result = await gameDataContentRepository.SearchByName(searchName, searchLanguage, cancellationToken);

        Assert.Equivalent(new List<Content> { Content1, Content2, Content3 }, result, true);
    }

    [Fact]
    public async Task NoResultsFromQueries_SearchByName_ReturnsEmptyEnumerable()
    {

        const string searchName = "popoto";
        const Language searchLanguage = Language.English;
        const Lumina_Language luminaLanguage = Lumina_Language.English;
        var cancellationToken = CancellationToken.None;
        mockSearchQuery1.Setup(query => query.Execute(searchName, luminaLanguage)).Returns([]);
        mockSearchQuery2.Setup(query => query.Execute(searchName, luminaLanguage)).Returns([]);
        mockSearchQuery3.Setup(query => query.Execute(searchName, luminaLanguage)).Returns([]);

        var result = await gameDataContentRepository.SearchByName(searchName, searchLanguage, cancellationToken);

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
        const string searchName = "popoto";
        var cancellationToken = CancellationToken.None;
        mockSearchQuery1.Setup(query => query.Execute(searchName, luminaLanguage))
                        .Returns([Content1, Content3]);
        mockSearchQuery2.Setup(query => query.Execute(searchName, luminaLanguage)).Returns([Content2]);
        mockSearchQuery3.Setup(query => query.Execute(searchName, luminaLanguage)).Returns([]);

        var results = await gameDataContentRepository.SearchByName(searchName, searchLanguage, cancellationToken);

        _ = results.ToList(); // Enumeration required to verify query execution
        mockSearchQuery1.Verify(query => query.Execute(searchName, luminaLanguage));
        mockSearchQuery2.Verify(query => query.Execute(searchName, luminaLanguage));
        mockSearchQuery3.Verify(query => query.Execute(searchName, luminaLanguage));
    }
}
