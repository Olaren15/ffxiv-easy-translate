namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Sheets;
using ContentType = Domain.Entities.ContentType;

public class ContentFinderConditionAdapter : IContentTypeAdapter<ContentFinderConditionLite>
{
    public Func<ContentFinderConditionLite, bool> WhereClause(string searchName)
    {
        return content => content.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<ContentFinderConditionLite, Content> MapToContent(
        ContentFinderConditionLite english,
        ContentFinderConditionLite french,
        ContentFinderConditionLite german,
        ContentFinderConditionLite japanese
    )
    {
        return content => new Content(
            ContentType.Instance,
            content.ContentType.Value?.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
