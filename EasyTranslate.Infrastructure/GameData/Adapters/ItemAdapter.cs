using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class ItemAdapter : IContentTypeAdapter<Item>
{
    public Func<Item, bool> WhereClause(string searchName)
    {
        return item => item.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Item, Content> MapToContent(
        Item english,
        Item french,
        Item german,
        Item japanese
    )
    {
        return item => new Content(
            ContentType.Item,
            item.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
