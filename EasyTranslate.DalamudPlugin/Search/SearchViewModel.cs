namespace EasyTranslate.DalamudPlugin.Search;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyTranslate.DalamudPlugin.Settings;
using EasyTranslate.Domain.Entities;
using EasyTranslate.UseCase.ItemSearch;

public sealed class SearchViewModel : IDisposable
{
    private readonly ItemMapper itemMapper;
    private readonly SearchItemByNameCommand searchItemByNameCommand;
    private readonly UserSettingsRepository userSettingsRepository;
    private Task<IEnumerable<Item>>? currentSearchTask;
    private CancellationTokenSource? searchCancellationToken;
    private IEnumerable<PresentableItem>? searchResults;

    public SearchViewModel(
        SearchItemByNameCommand searchItemByNameCommand,
        ItemMapper itemMapper,
        UserSettingsRepository userSettingsRepository
    )
    {
        this.searchItemByNameCommand = searchItemByNameCommand;
        this.itemMapper = itemMapper;
        this.userSettingsRepository = userSettingsRepository;
    }

    private Language SearchLanguage => userSettingsRepository.Get().DefaultSearchLanguage;
    public string SearchText { get; set; } = "";

    public IEnumerable<PresentableItem>? SearchResults
    {
        get
        {
            if (searchResults is not null)
            {
                return searchResults;
            }

            if (currentSearchTask is { IsCompletedSuccessfully: true })
            {
                searchResults = itemMapper.ConvertToPresentableItems(currentSearchTask.Result);

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
        currentSearchTask = searchItemByNameCommand.SearchItemByName(
            SearchText,
            SearchLanguage,
            searchCancellationToken.Token
        );
    }

    ~SearchViewModel()
    {
        Dispose();
    }
}
