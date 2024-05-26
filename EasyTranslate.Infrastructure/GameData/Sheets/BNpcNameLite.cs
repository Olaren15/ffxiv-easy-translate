using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

namespace EasyTranslate.Infrastructure.GameData.Sheets;

[Sheet("BNpcName")]
// ReSharper disable once ClassNeverInstantiated.Global
public class BNpcNameLite : ExcelRow
{
    public SeString Singular { get; private set; } = null!;

    public override void PopulateData(RowParser parser, Lumina.GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);
        Singular = parser.ReadOffset<SeString>(0)!;
    }
}
