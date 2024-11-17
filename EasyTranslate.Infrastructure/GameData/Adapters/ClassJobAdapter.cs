using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class ClassJobAdapter : IContentTypeAdapter<ClassJob>
{
    private const uint BaseJobIconId = 62100;

    public Func<ClassJob, bool> WhereClause(string searchName)
    {
        return classJob => classJob.RowId != 0 && // Filter out Adventurer
                           classJob.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<ClassJob, Content> MapToContent(ClassJob english, ClassJob french, ClassJob german,
        ClassJob japanese)
    {
        return classJob => new Content(
            ContentType.ClassJob,
            BaseJobIconId + classJob.RowId,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
