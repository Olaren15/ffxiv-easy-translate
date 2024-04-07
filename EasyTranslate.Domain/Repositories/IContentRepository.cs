namespace EasyTranslate.Domain.Repositories;

using EasyTranslate.Domain.Entities;

public interface IContentRepository
{
    public Task<IEnumerable<Content>> SearchByName(
        string searchName,
        Language searchLanguage,
        CancellationToken cancellationToken
    );
}
