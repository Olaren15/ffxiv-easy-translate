using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class MountAdapter : IContentTypeAdapter<Mount>
{
    public Func<Mount, bool> WhereClause(string searchName)
    {
        return mount => mount.Singular.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Mount, Content> MapToContent(
        Mount english,
        Mount french,
        Mount german,
        Mount japanese
    )
    {
        return mount => new Content(
            ContentType.Mount,
            mount.Icon,
            english.Singular.ExtractText(),
            french.Singular.ExtractText(),
            german.Singular.ExtractText(),
            japanese.Singular.ExtractText()
        );
    }
}
