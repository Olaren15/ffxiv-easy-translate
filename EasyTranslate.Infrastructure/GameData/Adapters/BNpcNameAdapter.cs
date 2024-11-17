using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class BNpcNameAdapter : IContentTypeAdapter<BNpcName>
{
    public Func<BNpcName, bool> WhereClause(string searchName)
    {
        return npcName => npcName.Singular.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<BNpcName, Content> MapToContent(
        BNpcName english,
        BNpcName french,
        BNpcName german,
        BNpcName japanese
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
