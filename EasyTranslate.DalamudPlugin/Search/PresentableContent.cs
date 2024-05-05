namespace EasyTranslate.DalamudPlugin.Search;

using Dalamud.Interface.Internal;
using Domain.Entities;

public record PresentableContent(
    ContentType Type,
    uint? IconId,
    IDalamudTextureWrap? IconTexture,
    string EnglishName,
    string FrenchName,
    string GermanName,
    string JapaneseName
) : Content(Type, IconId, EnglishName, FrenchName, GermanName, JapaneseName);
