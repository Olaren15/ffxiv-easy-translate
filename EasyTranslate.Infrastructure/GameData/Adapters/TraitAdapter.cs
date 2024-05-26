using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class TraitAdapter : IContentTypeAdapter<TraitLite>
{
    public Func<TraitLite, bool> WhereClause(string searchName)
    {
        return trait => trait.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<TraitLite, Content> MapToContent(
        TraitLite english,
        TraitLite french,
        TraitLite german,
        TraitLite japanese
    )
    {
        return trait => new Content(
            ContentType.Trait,
            (uint?)trait.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
