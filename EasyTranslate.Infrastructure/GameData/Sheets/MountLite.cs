using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

namespace EasyTranslate.Infrastructure.GameData.Sheets;

[Sheet("Mount")]
// ReSharper disable once ClassNeverInstantiated.Global
public class MountLite : ExcelRow
{
    public SeString Singular { get; private set; } = null!;

    public ushort Icon { get; private set; }

    public override void PopulateData(RowParser parser, Lumina.GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);
        Singular = parser.ReadOffset<SeString>(0)!;
        Icon = parser.ReadOffset<ushort>(52);
    }
}
