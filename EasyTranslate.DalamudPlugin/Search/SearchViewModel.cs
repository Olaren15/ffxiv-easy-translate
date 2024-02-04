namespace EasyTranslate.DalamudPlugin.Search;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyTranslate.Domain.Entities;
using EasyTranslate.UseCase.ItemSearch;

public class SearchViewModel(SearchItemByNameCommand searchItemByNameCommand) : IDisposable
{
    private Task<IEnumerable<Item>>? currentSearchTask;
    private CancellationTokenSource? searchCancellationToken;
    public Language SearchLanguage { get; set; } = Language.English;
    public string SearchText { get; set; } = "";

    public IEnumerable<Item>? SearchResults =>
        currentSearchTask is { IsCompletedSuccessfully: true } ? currentSearchTask.Result : null;

    public bool SearchResultsAreLoading => currentSearchTask is { IsCompleted: false };

    public void Dispose()
    {
        currentSearchTask?.Dispose();
        searchCancellationToken?.Dispose();
        GC.SuppressFinalize(this);
    }

    public void ExecuteSearch()
    {
        searchCancellationToken = new CancellationTokenSource();
        currentSearchTask = searchItemByNameCommand.SearchItemByName(
            SearchText,
            SearchLanguage,
            searchCancellationToken.Token
        );
    }

    public void ResetSearch()
    {
        SearchText = "";
        searchCancellationToken?.Cancel();
        searchCancellationToken?.Dispose();
        searchCancellationToken = null;
        currentSearchTask?.Dispose();
        currentSearchTask = null;
    }
}
