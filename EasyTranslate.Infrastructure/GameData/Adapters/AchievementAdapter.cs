namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class AchievementAdapter : IContentTypeAdapter<Achievement>
{
    public Func<Achievement, bool> WhereClause(string searchName)
    {
        return achievement => achievement.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Achievement, Content> MapToContent(
        ExcelSheet<Achievement> englishSheet,
        ExcelSheet<Achievement> frenchSheet,
        ExcelSheet<Achievement> germanSheet,
        ExcelSheet<Achievement> japaneseSheet
    )
    {
        return achievement => new Content(
            ContentType.Achievement,
            achievement.Icon,
            englishSheet.GetRow(achievement.RowId)!.Name.RawString,
            frenchSheet.GetRow(achievement.RowId)!.Name.RawString,
            germanSheet.GetRow(achievement.RowId)!.Name.RawString,
            japaneseSheet.GetRow(achievement.RowId)!.Name.RawString
        );
    }
}
