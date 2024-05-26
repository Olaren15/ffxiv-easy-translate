using System;
using System.Threading;
using System.Threading.Tasks;
using EasyTranslate.DalamudPlugin.Settings;
using EasyTranslate.Domain.Entities;
using EasyTranslate.UseCase;

namespace EasyTranslate.DalamudPlugin.Search;

public sealed class SearchViewModel(
    SearchContentByNameUseCase searchContentByNameUseCase,
    ContentMapper contentMapper,
    UserSettingsRepository userSettingsRepository
) : IDisposable
{
    private Task<PresentableContent[]>? _currentSearchTask;
    private CancellationTokenSource? _searchCancellationToken;
    private PresentableContent[]? _searchResults;

    private Language SearchLanguage => userSettingsRepository.Get().DefaultSearchLanguage;

    public string SearchText { get; set; } = "";

    public PresentableContent[]? SearchResults
    {
        get
        {
            if (_searchResults is not null)
            {
                return _searchResults;
            }

            if (_currentSearchTask is { IsCompletedSuccessfully: true })
            {
                _searchResults = _currentSearchTask.Result;

                _searchCancellationToken?.Dispose();
                _searchCancellationToken = null;
                _currentSearchTask.Dispose();
                _currentSearchTask = null;

                return _searchResults;
            }

            _searchResults = null;

            return _searchResults;
        }
    }

    public bool SearchResultsAreLoading => _currentSearchTask is { IsCompleted: false };

    public void Dispose()
    {
        _currentSearchTask?.Dispose();
        _currentSearchTask = null;
        _searchCancellationToken?.Dispose();
        _searchCancellationToken = null;
        GC.SuppressFinalize(this);
    }

    public void ExecuteSearch()
    {
        _searchResults = null;
        _searchCancellationToken = new CancellationTokenSource();
        _currentSearchTask = Task.Run(
                () => searchContentByNameUseCase.Execute(
                    SearchText,
                    SearchLanguage,
                    _searchCancellationToken.Token
                ),
                _searchCancellationToken.Token
            )
            .ContinueWith(
                searchResultsTask =>
                    contentMapper.ConvertToPresentableContents(searchResultsTask.Result),
                _searchCancellationToken.Token
            );
    }

    ~SearchViewModel()
    {
        Dispose();
    }
}
