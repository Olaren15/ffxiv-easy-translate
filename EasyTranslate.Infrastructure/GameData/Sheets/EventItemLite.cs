using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

namespace EasyTranslate.Infrastructure.GameData.Sheets;

[Sheet("EventItem")]
// ReSharper disable once ClassNeverInstantiated.Global
public class EventItemLite : ExcelRow
{
    // Needed because the Name property doesn't seem to be used in japanese
    public SeString Singular { get; private set; } = null!;
    public SeString Name { get; private set; } = null!;
    public ushort Icon { get; private set; }

    public override void PopulateData(RowParser parser, Lumina.GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);

        Singular = parser.ReadOffset<SeString>(0)!;
        Name = parser.ReadOffset<SeString>(8)!;
        Icon = parser.ReadOffset<ushort>(24);
    }
}
