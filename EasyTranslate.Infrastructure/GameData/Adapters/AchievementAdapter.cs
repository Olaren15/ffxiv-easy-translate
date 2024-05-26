using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class AchievementAdapter : IContentTypeAdapter<AchievementLite>
{
    public Func<AchievementLite, bool> WhereClause(string searchName)
    {
        return achievement => achievement.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<AchievementLite, Content> MapToContent(
        AchievementLite english,
        AchievementLite french,
        AchievementLite german,
        AchievementLite japanese
    )
    {
        return achievement => new Content(
            ContentType.Achievement,
            achievement.Icon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
