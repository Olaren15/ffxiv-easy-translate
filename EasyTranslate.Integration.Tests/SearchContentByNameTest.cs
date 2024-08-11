using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using EasyTranslate.Domain.Entities;
using EasyTranslate.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;
using Xunit;

namespace EasyTranslate.Integration.Tests;

// Assertions are done with snapshots
[SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions")]
public class SearchContentByNameTest
{
    private readonly SearchContentByNameUseCase _searchContentByNameUseCase;

    public SearchContentByNameTest()
    {
        IServiceProvider serviceProvider = ServiceProviderBuilder.Build();
        _searchContentByNameUseCase = serviceProvider.GetService<SearchContentByNameUseCase>()!;
    }

    [Fact]
    public async Task Item_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Popoto",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Action_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "cure",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }


    [Fact]
    public async Task Achievement_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "For the hoard",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task BNpcname_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "bhoot",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task ClassJob_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute("summoner", Language.English);

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Companion_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "lesser panda",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task ContentFinderCondition_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "sastasha",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task CraftAction_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Groundwork",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Emote_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "pet",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task ENpcResident_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "G'raha Tia",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task EventItemLite_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Ulan's note",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Fate_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Drink Me",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Leve_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Practical Command",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Mount_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Demi-Ozma",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Orchestrion_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "pa-paya",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task PlaceName_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Limsa Lominsa",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Quest_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "For Want Of a Memory",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Status_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Vulnerability Up",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Title_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Goddess of Magic",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Trait_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Maim and Mend",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }

    [Fact]
    public async Task Weather_SearchContentByName_ReturnsMatchingResults()
    {
        IEnumerable<Content> results = await _searchContentByNameUseCase.Execute(
            "Fair Skies",
            Language.English
        );

        Snapshot.Match(results.ToList());
    }
}
