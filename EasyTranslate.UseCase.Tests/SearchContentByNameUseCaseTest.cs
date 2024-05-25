namespace EasyTranslate.UseCase.Tests;

using Domain.Comparers;
using Domain.Entities;
using Domain.Repositories;
using Moq;

public class SearchContentByNameUseCaseTest
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

    private readonly Mock<IContentRepository> mockContentRepository;
    private readonly SearchContentByNameUseCase searchContentByNameUseCase;

    public SearchContentByNameUseCaseTest()
    {
        mockContentRepository = new Mock<IContentRepository>();
        searchContentByNameUseCase = new SearchContentByNameUseCase(
            mockContentRepository.Object,
            new LongestCommonSubstringComparer()
        );
    }

    public static TheoryData<Language> SearchLanguages()
    {
        return new TheoryData<Language>((Language[])Enum.GetValues(typeof(Language)));
    }

    [Theory]
    [MemberData(nameof(SearchLanguages))]
    public async Task SearchQueryEmpty_Execute_ReturnsEmptyEnumerable(Language searchLanguage)
    {
        const string searchQuery = "";
        var cancellationToken = CancellationToken.None;

        var result = await searchContentByNameUseCase.Execute(searchQuery, searchLanguage, cancellationToken);

        Assert.Empty(result);
        mockContentRepository.VerifyNoOtherCalls();
    }

    public static TheoryData<string, Language, Content[]> SearchQueries()
    {
        return new TheoryData<string, Language, Content[]>
        {
            { "popoto", Language.English, [Content1, Content2, Content3] },
            { "popoto", Language.French, [Content1, Content3, Content2] },
            { "toffel", Language.German, [Content1, Content3, Content2] },
            { "ポポト", Language.Japanese, [Content1, Content2, Content3] },
        };
    }

    [Theory]
    [MemberData(nameof(SearchQueries))]
    public async Task SearchQueryAndSearchLanguage_Excecute_ReturnsResultsSortedByLanguageNameSimilarity(
        string searchQuery,
        Language searchLanguage,
        Content[] expected
    )
    {
        var cancellationToken = CancellationToken.None;
        mockContentRepository.Setup(
                                 repository => repository.SearchByName(searchQuery, searchLanguage, cancellationToken)
                                                         .Result
                             )
                             .Returns([Content3, Content1, Content2]);

        var result = await searchContentByNameUseCase.Execute(searchQuery, searchLanguage, cancellationToken);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task OverOneHundreadSearchResults_Execute_ReturnsTopOneHundreadResults()
    {
        const string searchQuery = "popoto";
        const Language searchLanguage = Language.English;
        var cancellationToken = CancellationToken.None;
        var searchResults = GenerateALotOfContent().ToList();
        mockContentRepository.Setup(
                                 repository => repository.SearchByName(searchQuery, searchLanguage, cancellationToken)
                                                         .Result)
                             .Returns(searchResults);

        var result = await searchContentByNameUseCase.Execute(searchQuery, searchLanguage, cancellationToken);

        Assert.Equal(searchResults.Take(100), result);
    }

    private static IEnumerable<Content> GenerateALotOfContent()
    {
        var baseContent = Content1;

        return Enumerable.Range(0, 101)
                         .Select(
                             i => baseContent with
                             {
                                 EnglishName = baseContent.EnglishName + new string('o', i),
                             });
    }
}
