namespace EasyTranslate.DalamudPlugin.Search;

using System.Collections.Generic;
using Dalamud.Interface.Internal;
using EasyTranslate.Domain.Entities;

public record PresentableContent(
    uint Id,
    ContentType Type,
    uint? IconId,
    IDalamudTextureWrap? IconTexture,
    IDictionary<Language, string> LocalisedNames
) : Content(Id, Type, IconId, LocalisedNames);
