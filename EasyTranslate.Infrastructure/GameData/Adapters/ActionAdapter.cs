using EasyTranslate.Domain.Entities;
using Action = Lumina.Excel.Sheets.Action;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class ActionAdapter : IContentTypeAdapter<Action>
{
    public Func<Action, bool> WhereClause(string searchName)
    {
        return action => action.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Action, Content> MapToContent(
        Action english,
        Action french,
        Action german,
        Action japanese
    )
    {
        return action => new Content(
            ContentType.Action,
            action.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
