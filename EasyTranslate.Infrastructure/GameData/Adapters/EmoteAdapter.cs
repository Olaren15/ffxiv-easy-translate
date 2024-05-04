namespace EasyTranslate.Infrastructure.GameData.Adapters;

using Domain.Entities;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets2;
using ContentType = Domain.Entities.ContentType;

public class EmoteAdapter : IContentTypeAdapter<Emote>
{
    public Func<Emote, bool> WhereClause(string searchName)
    {
        return emote => emote.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Emote, Content> MapToContent(
        ExcelSheet<Emote> englishSheet,
        ExcelSheet<Emote> frenchSheet,
        ExcelSheet<Emote> germanSheet,
        ExcelSheet<Emote> japaneseSheet
    )
    {
        return emote => new Content(
            ContentType.Emote,
            emote.Icon,
            englishSheet.GetRow(emote.RowId)!.Name.RawString,
            frenchSheet.GetRow(emote.RowId)!.Name.RawString,
            germanSheet.GetRow(emote.RowId)!.Name.RawString,
            japaneseSheet.GetRow(emote.RowId)!.Name.RawString
        );
    }
}
