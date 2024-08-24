using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;

namespace EasyTranslate.UseCase;

public class GetItemNameUseCase(IContentRepository contentRepository)
{
    public async Task<string?> Execute(uint itemId, Language language, CancellationToken cancellationToken = default)
    {
        return await contentRepository.GetItemName(itemId, language, cancellationToken);
    }
}
