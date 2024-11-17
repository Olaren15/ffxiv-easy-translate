using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class TraitAdapter : IContentTypeAdapter<Trait>
{
    public Func<Trait, bool> WhereClause(string searchName)
    {
        return trait => trait.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Trait, Content> MapToContent(
        Trait english,
        Trait french,
        Trait german,
        Trait japanese
    )
    {
        return trait => new Content(
            ContentType.Trait,
            (uint?)trait.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
