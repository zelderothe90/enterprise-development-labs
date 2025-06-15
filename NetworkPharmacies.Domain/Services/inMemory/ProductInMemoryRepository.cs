using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Services.inMemory
{
    /// <summary>
    /// In-memory реализация репозитория препаратов
    /// </summary>
    public class ProductInMemoryRepository : IProductRepository
    {
        private readonly List<Product> _products = new();
        private int _nextId = 1;

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Task.FromResult(_products.AsEnumerable());
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
        }

        public async Task AddAsync(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Code = product.Code;
                existing.Name = product.Name;
                existing.Group = product.Group;
                existing.PharmaceuticalGroups = product.PharmaceuticalGroups;
                existing.Quantity = product.Quantity;
            }
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            return await Task.FromResult(_products
                .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task<IEnumerable<Product>> GetByPharmaceuticalGroupAsync(int groupId)
        {
            return await Task.FromResult(_products
                .Where(p => p.PharmaceuticalGroups.Any(g => g.Id == groupId)));
        }

        public async Task<IEnumerable<Product>> GetByProductGroupAsync(int groupId)
        {
            return await Task.FromResult(_products
                .Where(p => p.Group?.Id == groupId));
        }

        public async Task<IEnumerable<Product>> GetByMinQuantityAsync(int minQuantity)
        {
            return await Task.FromResult(_products
                .Where(p => p.Quantity >= minQuantity));
        }
    }
}