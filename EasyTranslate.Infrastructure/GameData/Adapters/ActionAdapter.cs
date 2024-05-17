namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Sheets;
using ContentType = Domain.Entities.ContentType;

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
