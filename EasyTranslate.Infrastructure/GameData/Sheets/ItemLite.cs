namespace EasyTranslate.Infrastructure.GameData.Sheets;

using Lumina;
using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

[Sheet("Item")]
// ReSharper disable once ClassNeverInstantiated.Global
public class ItemLite : ExcelRow
{
    public SeString Name { get; private set; } = null!;
    public ushort Icon { get; private set; }

    public override void PopulateData(RowParser parser, GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);
        Name = parser.ReadOffset<SeString>(12)!;
        Icon = parser.ReadOffset<ushort>(136);
    }
}
