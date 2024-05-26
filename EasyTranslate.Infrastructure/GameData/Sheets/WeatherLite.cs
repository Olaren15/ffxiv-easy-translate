﻿using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

namespace EasyTranslate.Infrastructure.GameData.Sheets;

[Sheet("Weather")]
// ReSharper disable once ClassNeverInstantiated.Global
public class WeatherLite : ExcelRow
{
    public SeString Name { get; private set; } = null!;

    public int Icon { get; private set; }

    public override void PopulateData(RowParser parser, Lumina.GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);
        Name = parser.ReadOffset<SeString>(0)!;
        Icon = parser.ReadOffset<int>(24);
    }
}
