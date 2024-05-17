namespace EasyTranslate.Infrastructure.GameData.Sheets;

using Lumina;
using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

[Sheet("ENpcResident")]
// ReSharper disable once ClassNeverInstantiated.Global
public class ENpcResidentLite : ExcelRow
{
    public SeString Singular { get; private set; } = null!;

    public override void PopulateData(RowParser parser, GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);
        Singular = parser.ReadOffset<SeString>(0)!;
    }
}
