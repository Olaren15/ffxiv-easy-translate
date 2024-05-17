namespace EasyTranslate.Infrastructure.GameData.Sheets;

using Lumina;
using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

[Sheet("Title")]
// ReSharper disable once ClassNeverInstantiated.Global
public class TitleLite : ExcelRow
{
    public SeString Masculine { get; private set; } = null!;
    public SeString Feminine { get; private set; } = null!;

    public override void PopulateData(RowParser parser, GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);
        Masculine = parser.ReadOffset<SeString>(0)!;
        Feminine = parser.ReadOffset<SeString>(4)!;
    }
}
