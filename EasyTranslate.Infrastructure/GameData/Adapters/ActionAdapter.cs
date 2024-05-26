using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class ActionAdapter : IContentTypeAdapter<ActionLite>
{
    public Func<ActionLite, bool> WhereClause(string searchName)
    {
        return action => action.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<ActionLite, Content> MapToContent(
        ActionLite english,
        ActionLite french,
        ActionLite german,
        ActionLite japanese
    )
    {
        return action => new Content(
            ContentType.Action,
            action.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            german.Name.RawString
        );
    }
}
