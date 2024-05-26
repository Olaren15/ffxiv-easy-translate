using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class MountAdapter : IContentTypeAdapter<MountLite>
{
    public Func<MountLite, bool> WhereClause(string searchName)
    {
        return mount => mount.Singular.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<MountLite, Content> MapToContent(
        MountLite english,
        MountLite french,
        MountLite german,
        MountLite japanese
    )
    {
        return mount => new Content(
            ContentType.Mount,
            mount.Icon,
            english.Singular.RawString,
            french.Singular.RawString,
            german.Singular.RawString,
            japanese.Singular.RawString
        );
    }
}
