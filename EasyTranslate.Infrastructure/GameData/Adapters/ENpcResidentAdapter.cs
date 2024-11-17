using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class ENpcResidentAdapter : IContentTypeAdapter<ENpcResident>
{
    public Func<ENpcResident, bool> WhereClause(string searchName)
    {
        return npcName => npcName.Singular.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<ENpcResident, Content> MapToContent(
        ENpcResident english,
        ENpcResident french,
        ENpcResident german,
        ENpcResident japanese
    )
    {
        return _ => new Content(
            ContentType.Npc,
            null,
            english.Singular.ExtractText(),
            french.Singular.ExtractText(),
            german.Singular.ExtractText(),
            japanese.Singular.ExtractText()
        );
    }
}
