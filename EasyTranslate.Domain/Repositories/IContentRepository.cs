using EasyTranslate.Domain.Entities;

namespace EasyTranslate.Domain.Repositories;

public interface IContentRepository
{
    public Task<IEnumerable<Content>> SearchByName(
        string searchName,
        Language searchLanguage,
        CancellationToken cancellationToken
    );

    public Task<string?> GetItemName(uint itemId, Language searchLanguage, CancellationToken cancellationToken);
}
