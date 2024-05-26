using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

namespace EasyTranslate.Infrastructure.GameData.Sheets;

[Sheet("ContentFinderCondition")]
// ReSharper disable once ClassNeverInstantiated.Global
public class ContentFinderConditionLite : ExcelRow
{
    public SeString Name { get; set; } = null!;

    public LazyRow<ContentTypeLite> ContentType { get; private set; } = null!;

    public override void PopulateData(RowParser parser, Lumina.GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);
        Name = parser.ReadOffset<SeString>(0)!;
        ContentType = new LazyRow<ContentTypeLite>(gameData, parser.ReadOffset<byte>(110), language);
    }
}
