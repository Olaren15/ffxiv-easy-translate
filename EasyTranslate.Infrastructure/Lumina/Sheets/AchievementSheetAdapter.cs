using Achievement = Lumina.Excel.GeneratedSheets2.Achievement;

namespace EasyTranslate.Infrastructure.Lumina.Sheets;

using EasyTranslate.Domain.Entities;

public class AchievementSheetAdapter : ISheetQueryAdapter<Achievement>
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
            achievement.RowId,
            achievement.Icon,
            new Dictionary<Language, string>
            {
                { Language.English, englishSheet.GetRow(achievement.RowId)!.Name.RawString },
                { Language.French, frenchSheet.GetRow(achievement.RowId)!.Name.RawString },
                { Language.German, germanSheet.GetRow(achievement.RowId)!.Name.RawString },
                { Language.Japanese, japaneseSheet.GetRow(achievement.RowId)!.Name.RawString },
            }
        );
    }
}
