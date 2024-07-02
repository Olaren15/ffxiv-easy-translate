using Dalamud.Interface.Textures;
using EasyTranslate.Domain.Entities;

namespace EasyTranslate.DalamudPlugin.Search;

public record PresentableContent(
    ContentType Type,
    uint? IconId,
    ISharedImmediateTexture? IconTexture,
    string EnglishName,
    string FrenchName,
    string GermanName,
    string JapaneseName
) : Content(Type, IconId, EnglishName, FrenchName, GermanName, JapaneseName);
