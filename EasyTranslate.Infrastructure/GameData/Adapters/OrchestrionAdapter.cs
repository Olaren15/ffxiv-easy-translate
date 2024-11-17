using EasyTranslate.Domain.Entities;
using Lumina.Excel.Sheets;
using ContentType = EasyTranslate.Domain.Entities.ContentType;

namespace EasyTranslate.Infrastructure.GameData.Adapters;

public class OrchestrionAdapter : IContentTypeAdapter<Orchestrion>
{
    private const uint MusicNoteIcon = 230206;

    public Func<Orchestrion, bool> WhereClause(string searchName)
    {
        return orchestrion => orchestrion.Name.ExtractText().Contains(searchName, StringComparison.OrdinalIgnoreCase);
    }

    public Func<Orchestrion, Content> MapToContent(
        Orchestrion english,
        Orchestrion french,
        Orchestrion german,
        Orchestrion japanese
    )
    {
        return _ => new Content(
            ContentType.Orchestrion,
            MusicNoteIcon,
            english.Name.ExtractText(),
            french.Name.ExtractText(),
            german.Name.ExtractText(),
            japanese.Name.ExtractText()
        );
    }
}
