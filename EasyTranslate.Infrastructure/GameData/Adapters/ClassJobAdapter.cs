using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class ClassJobAdapter : IContentTypeAdapter<ClassJobLite>
{
    private const uint BaseJobIconId = 62100;

    public Func<ClassJobLite, bool> WhereClause(string searchName)
    {
        return classJob => classJob.RowId != 0 && // Filter out Adventurer
                           classJob.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<ClassJobLite, Content> MapToContent(ClassJobLite english, ClassJobLite french, ClassJobLite german,
        ClassJobLite japanese)
    {
        return classJob => new Content(
            ContentType.ClassJob,
            BaseJobIconId + classJob.RowId,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
