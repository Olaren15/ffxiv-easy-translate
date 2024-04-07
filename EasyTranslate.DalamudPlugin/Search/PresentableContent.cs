namespace EasyTranslate.DalamudPlugin.Search;

using System.Collections.Generic;
using Dalamud.Interface.Internal;
using EasyTranslate.Domain.Entities;

public record PresentableContent(
    uint Id,
    uint? IconId,
    IDalamudTextureWrap? IconTexture,
    IDictionary<Language, string> LocalisedNames
) : Content(Id, IconId, LocalisedNames);
