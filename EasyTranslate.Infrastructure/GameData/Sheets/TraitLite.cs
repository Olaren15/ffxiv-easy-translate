namespace EasyTranslate.Infrastructure.GameData.Sheets;

using Lumina;
using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

[Sheet("Trait")]
// ReSharper disable once ClassNeverInstantiated.Global
public class TraitLite : ExcelRow
{
    public SeString Name { get; private set; } = null!;
    public int Icon { get; private set; }

    public override void PopulateData(RowParser parser, GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);
        Name = parser.ReadOffset<SeString>(0)!;
        Icon = parser.ReadOffset<int>(8);
    }
}
