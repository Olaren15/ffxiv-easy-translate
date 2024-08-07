﻿using Lumina.Data;
using Lumina.Excel;
using Lumina.Text;

namespace EasyTranslate.Infrastructure.GameData.Sheets;

[Sheet("ClassJob")]
// ReSharper disable once ClassNeverInstantiated.Global
public class ClassJobLite : ExcelRow
{
    public SeString Name { get; private set; } = null!;

    public override void PopulateData(RowParser parser, Lumina.GameData gameData, Language language)
    {
        base.PopulateData(parser, gameData, language);

        Name = parser.ReadOffset<SeString>(0)!;
    }
}
