using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class CraftActionAdapter : IContentTypeAdapter<CraftAction>
{
    public Func<CraftAction, bool> WhereClause(string searchName)
    {
        return craftAction => craftAction.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<CraftAction, Content> MapToContent(
        CraftAction english,
        CraftAction french,
        CraftAction german,
        CraftAction japanese
    )
    {
        return craftAction => new Content(
            ContentType.Action,
            craftAction.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
