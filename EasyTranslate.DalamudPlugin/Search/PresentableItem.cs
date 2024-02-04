namespace EasyTranslate.DalamudPlugin.Search;

using System.Collections.Generic;
using Dalamud.Interface.Internal;
using EasyTranslate.Domain.Entities;

public record PresentableItem(
    int Id,
    string IconUrl,
    uint? IconId,
    IDalamudTextureWrap? IconTexture,
    IDictionary<Language, string> LocalisedNames,
    string DetailsUrl
) : Item(Id, IconUrl, IconId, LocalisedNames, DetailsUrl);
