namespace EasyTranslate.Infrastructure.GameData.Sheets;

using Lumina;
using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

[Sheet("Companion")]
// ReSharper disable once ClassNeverInstantiated.Global
public class CompanionLite : ExcelRow
{
    public SeString Singular { get; private set; } = null!;
    public ushort Icon { get; private set; }

    public override void PopulateData(RowParser parser, GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);
        Singular = parser.ReadOffset<SeString>(0)!;
        Icon = parser.ReadOffset<ushort>(20);
    }
}
