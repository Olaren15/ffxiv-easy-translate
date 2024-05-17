namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Sheets;
using ContentType = Domain.Entities.ContentType;

public class CraftActionAdapter : IContentTypeAdapter<CraftActionLite>
{
    public Func<CraftActionLite, bool> WhereClause(string searchName)
    {
        return craftAction => craftAction.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<CraftActionLite, Content> MapToContent(
        CraftActionLite english,
        CraftActionLite french,
        CraftActionLite german,
        CraftActionLite japanese
    )
    {
        return craftAction => new Content(
            ContentType.Action,
            craftAction.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
