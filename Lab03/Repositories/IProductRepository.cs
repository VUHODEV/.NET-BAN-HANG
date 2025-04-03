using Lab03.Models; 
namespace Lab03.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Models.Product>> GetAllAsync();
        Task<Models.Product> GetByIdAsync(int Id);
        Task AddAsync(Models.Product product);
        Task UpdateAsync(Models.Product product);
        Task DeleteAsync(int Id);
    }
}
