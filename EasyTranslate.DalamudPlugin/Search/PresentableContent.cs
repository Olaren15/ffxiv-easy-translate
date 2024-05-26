using Dalamud.Interface.Internal;
using EasyTranslate.Domain.Entities;

namespace EasyTranslate.DalamudPlugin.Search;

public record PresentableContent(
    ContentType Type,
    uint? IconId,
    IDalamudTextureWrap? IconTexture,
    string EnglishName,
    string FrenchName,
    string GermanName,
    string JapaneseName
) : Content(Type, IconId, EnglishName, FrenchName, GermanName, JapaneseName);
