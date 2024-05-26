using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.GameData.Sheets;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class OrchestrionAdapter : IContentTypeAdapter<OrchestrionLite>
{
    private const uint MusicNoteIcon = 76406;

    public Func<OrchestrionLite, bool> WhereClause(string searchName)
    {
        return orchestrion => orchestrion.Name.RawString.Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<OrchestrionLite, Content> MapToContent(
        OrchestrionLite english,
        OrchestrionLite french,
        OrchestrionLite german,
        OrchestrionLite japanese
    )
    {
        return _ => new Content(
            ContentType.Orchestrion,
            MusicNoteIcon,
            english.Name.RawString,
            french.Name.RawString,
            german.Name.RawString,
            japanese.Name.RawString
        );
    }
}
