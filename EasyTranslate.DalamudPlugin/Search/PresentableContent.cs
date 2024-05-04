namespace EasyTranslate.DalamudPlugin.Search;

using Dalamud.Interface.Internal;
using Domain.Entities;

public record PresentableContent(
    ContentType Type,
    uint? IconId,
    IDalamudTextureWrap? IconTexture,
    string englishName,
    string frenchName,
    string germanName,
    string japaneseName
) : Content(Type, IconId, englishName, frenchName, germanName, japaneseName);
