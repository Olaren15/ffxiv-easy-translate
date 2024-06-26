﻿using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class BNpcNameAdapter : IContentTypeAdapter<BNpcNameLite>
{
    public Func<BNpcNameLite, bool> WhereClause(string searchName)
    {
        return npcName => npcName.Singular.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<BNpcNameLite, Content> MapToContent(
        BNpcNameLite english,
        BNpcNameLite french,
        BNpcNameLite german,
        BNpcNameLite japanese
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
