namespace EasyTranslate.DalamudPlugin.Search;

using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Settings;
using UseCase;

public sealed class SearchViewModel(
    SearchContentByNameUseCase searchContentByNameUseCase,
    ContentMapper contentMapper,
    UserSettingsRepository userSettingsRepository
) : IDisposable
{
    private Task<PresentableContent[]>? currentSearchTask;
    private CancellationTokenSource? searchCancellationToken;
    private PresentableContent[]? searchResults;

    private Language SearchLanguage => userSettingsRepository.Get().DefaultSearchLanguage;
    public string SearchText { get; set; } = "";

    public PresentableContent[]? SearchResults
    {
        get
        {
            if (searchResults is not null)
            {
                return searchResults;
            }

            if (currentSearchTask is { IsCompletedSuccessfully: true })
            {
                searchResults = currentSearchTask.Result;

                searchCancellationToken?.Dispose();
                searchCancellationToken = null;
                currentSearchTask.Dispose();
                currentSearchTask = null;

                return searchResults;
            }

            searchResults = null;

            return searchResults;
        }
    }

    public bool SearchResultsAreLoading => currentSearchTask is { IsCompleted: false };

    public void Dispose()
    {
        currentSearchTask?.Dispose();
        currentSearchTask = null;
        searchCancellationToken?.Dispose();
        searchCancellationToken = null;
        GC.SuppressFinalize(this);
    }

    public void ExecuteSearch()
    {
        searchResults = null;
        searchCancellationToken = new CancellationTokenSource();
        currentSearchTask = Task.Run(
                                    () => searchContentByNameUseCase.Execute(
                                        SearchText,
                                        SearchLanguage,
                                        searchCancellationToken.Token
                                    ),
                                    searchCancellationToken.Token)
                                .ContinueWith(
                                    searchResultsTask =>
                                        contentMapper.ConvertToPresentableContents(searchResultsTask.Result),
                                    searchCancellationToken.Token);
    }

    ~SearchViewModel()
    {
        Dispose();
    }
}
