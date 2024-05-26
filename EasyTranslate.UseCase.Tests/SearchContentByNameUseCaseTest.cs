using EasyTranslate.Domain.Comparers;
using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;
using Moq;

namespace EasyTranslate.UseCase.Tests;

public class SearchContentByNameUseCaseTest
{
    private static readonly Content s_content1 = new(ContentType.Item, 25204, "Popoto", "Popoto", "Toffel", "ポポト");

    private static readonly Content s_content2 = new(
        ContentType.Item,
        27455,
        "Popoto Set",
        "Tubercule de popoto",
        "Toffel-Knollen",
        "ポポトの種芋");

    private static readonly Content s_content3 = new(
        ContentType.Emote,
        64371,
        "Popoto Step",
        "Popping popoto",
        "Toffel-Tanz",
        "ポポトステップ");

    private readonly Mock<IContentRepository> _mockContentRepository;
    private readonly SearchContentByNameUseCase _searchContentByNameUseCase;

    public SearchContentByNameUseCaseTest()
    {
        _mockContentRepository = new Mock<IContentRepository>();
        _searchContentByNameUseCase = new SearchContentByNameUseCase(
            _mockContentRepository.Object,
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
        CancellationToken cancellationToken = CancellationToken.None;

        IEnumerable<Content> result = await _searchContentByNameUseCase.Execute(
            searchQuery,
            searchLanguage,
            cancellationToken
        );

        Assert.Empty(result);
        _mockContentRepository.VerifyNoOtherCalls();
    }

    public static TheoryData<string, Language, Content[]> SearchQueries()
    {
        return new TheoryData<string, Language, Content[]>
        {
            { "popoto", Language.English, [s_content1, s_content2, s_content3] },
            { "popoto", Language.French, [s_content1, s_content3, s_content2] },
            { "toffel", Language.German, [s_content1, s_content3, s_content2] },
            { "ポポト", Language.Japanese, [s_content1, s_content2, s_content3] }
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
        CancellationToken cancellationToken = CancellationToken.None;
        _mockContentRepository.Setup(
                repository => repository.SearchByName(searchQuery, searchLanguage, cancellationToken)
                    .Result
            )
            .Returns([s_content3, s_content1, s_content2]);

        IEnumerable<Content> result = await _searchContentByNameUseCase.Execute(
            searchQuery,
            searchLanguage,
            cancellationToken
        );

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task OverOneHundreadSearchResults_Execute_ReturnsTopOneHundreadResults()
    {
        const string searchQuery = "popoto";
        const Language searchLanguage = Language.English;
        CancellationToken cancellationToken = CancellationToken.None;
        List<Content> searchResults = GenerateALotOfContent().ToList();
        _mockContentRepository.Setup(
                repository => repository.SearchByName(searchQuery, searchLanguage, cancellationToken)
                    .Result)
            .Returns(searchResults);

        IEnumerable<Content> result = await _searchContentByNameUseCase.Execute(
            searchQuery,
            searchLanguage,
            cancellationToken
        );

        Assert.Equal(searchResults.Take(100), result);
    }

    private static IEnumerable<Content> GenerateALotOfContent()
    {
        Content baseContent = s_content1;

        return Enumerable.Range(0, 101)
            .Select(i => baseContent with { EnglishName = baseContent.EnglishName + new string('o', i) });
    }
}
