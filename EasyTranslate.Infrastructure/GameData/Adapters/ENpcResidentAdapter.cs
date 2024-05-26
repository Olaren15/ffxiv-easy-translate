using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class ENpcResidentAdapter : IContentTypeAdapter<ENpcResidentLite>
{
    public Func<ENpcResidentLite, bool> WhereClause(string searchName)
    {
        return npcName => npcName.Singular.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<ENpcResidentLite, Content> MapToContent(
        ENpcResidentLite english,
        ENpcResidentLite french,
        ENpcResidentLite german,
        ENpcResidentLite japanese
    )
    {
        return _ => new Content(
            ContentType.Npc,
            null,
            english.Singular.RawString,
            french.Singular.RawString,
            german.Singular.RawString,
            japanese.Singular.RawString
        );
    }
}
