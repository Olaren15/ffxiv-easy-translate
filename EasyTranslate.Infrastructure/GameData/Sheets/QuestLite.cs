using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

namespace EasyTranslate.Infrastructure.GameData.Sheets;

[Sheet("Quest")]
// ReSharper disable once ClassNeverInstantiated.Global
public class QuestLite : ExcelRow
{
    public SeString Name { get; private set; } = null!;

    public override void PopulateData(RowParser parser, Lumina.GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);
        Name = parser.ReadOffset<SeString>(0)!;
    }
}
