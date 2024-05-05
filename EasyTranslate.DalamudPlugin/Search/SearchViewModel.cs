namespace EasyTranslate.DalamudPlugin.Search;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Settings;
using UseCase;

public sealed class SearchViewModel(
    SearchContentByNameCommand searchContentByNameCommand,
    ContentMapper contentMapper,
    UserSettingsRepository userSettingsRepository
) : IDisposable
{
    private Task<IEnumerable<Content>>? currentSearchTask;
    private CancellationTokenSource? searchCancellationToken;
    private IEnumerable<PresentableContent>? searchResults;

    private Language SearchLanguage => userSettingsRepository.Get().DefaultSearchLanguage;
    public string SearchText { get; set; } = "";

    public IEnumerable<PresentableContent>? SearchResults
    {
        get
        {
            if (searchResults is not null)
            {
                return searchResults;
            }

            if (currentSearchTask is { IsCompletedSuccessfully: true })
            {
                searchResults = contentMapper.ConvertToPresentableItems(currentSearchTask.Result);

                searchCancellationToken?.Dispose();
                searchCancellationToken = null;
                currentSearchTask.Dispose();
                currentSearchTask = null;

                return searchResults;
            }

            return null;
        }
    }

    public bool SearchResultsAreLoading => currentSearchTask is { IsCompleted: false };

    public void Dispose()
    {
        currentSearchTask?.Dispose();
        searchCancellationToken?.Dispose();
        GC.SuppressFinalize(this);
    }

    public void ExecuteSearch()
    {
        searchResults = null;
        searchCancellationToken = new CancellationTokenSource();
        currentSearchTask = Task.Run(
            () => searchContentByNameCommand.Execute(
                SearchText,
                SearchLanguage,
                searchCancellationToken.Token
            ),
            searchCancellationToken.Token);
    }

    ~SearchViewModel()
    {
        Dispose();
    }
}
