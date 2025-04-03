namespace Lab03.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Models.Category>> GetAllAsync();
        Task<Models.Category> GetByIdAsync(int Id);
        Task AddAsync(Models.Category category);
        Task UpdateAsync(Models.Category category);
        Task DeleteAsync(int Id);
    }
}
