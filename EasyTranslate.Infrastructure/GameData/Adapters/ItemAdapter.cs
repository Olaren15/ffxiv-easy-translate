using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class ItemAdapter : IContentTypeAdapter<ItemLite>
{
    public Func<ItemLite, bool> WhereClause(string searchName)
    {
        return item => item.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<ItemLite, Content> MapToContent(
        ItemLite english,
        ItemLite french,
        ItemLite german,
        ItemLite japanese
    )
    {
        return item => new Content(
            ContentType.Item,
            item.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
