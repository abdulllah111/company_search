namespace Application.Common.Interfaces
{
    public interface ICategoryService
    {
        Task UpdateCategoryUsageStatusAsync(Guid categoryId, CancellationToken cancellationToken);
    }
}