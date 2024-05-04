namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class OrchestrionAdapter : IContentTypeAdapter<Orchestrion>
{
    private const uint MusicNoteIcon = 76406;

    public Func<Orchestrion, bool> WhereClause(string searchName)
    {
        return orchestrion => orchestrion.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Orchestrion, Content> MapToContent(
        ExcelSheet<Orchestrion> englishSheet,
        ExcelSheet<Orchestrion> frenchSheet,
        ExcelSheet<Orchestrion> germanSheet,
        ExcelSheet<Orchestrion> japaneseSheet
    )
    {
        return orchestrion => new Content(
            ContentType.Orchestrion,
            MusicNoteIcon,
            englishSheet.GetRow(orchestrion.RowId)!.Name.RawString,
            frenchSheet.GetRow(orchestrion.RowId)!.Name.RawString,
            germanSheet.GetRow(orchestrion.RowId)!.Name.RawString,
            japaneseSheet.GetRow(orchestrion.RowId)!.Name.RawString
        );
    }
}
