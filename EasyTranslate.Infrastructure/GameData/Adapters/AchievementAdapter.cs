using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class AchievementAdapter : IContentTypeAdapter<Achievement>
{
    public Func<Achievement, bool> WhereClause(string searchName)
    {
        return achievement => achievement.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Achievement, Content> MapToContent(
        Achievement english,
        Achievement french,
        Achievement german,
        Achievement japanese
    )
    {
        return achievement => new Content(
            ContentType.Achievement,
            achievement.Icon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
