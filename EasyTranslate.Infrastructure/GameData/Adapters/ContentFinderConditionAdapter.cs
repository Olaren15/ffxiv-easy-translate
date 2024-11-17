using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class ContentFinderConditionAdapter : IContentTypeAdapter<ContentFinderCondition>
{
    public Func<ContentFinderCondition, bool> WhereClause(string searchName)
    {
        return content => content.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<ContentFinderCondition, Content> MapToContent(
        ContentFinderCondition english,
        ContentFinderCondition french,
        ContentFinderCondition german,
        ContentFinderCondition japanese
    )
    {
        return content => new Content(
            ContentType.Instance,
            content.ContentType.Value.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
