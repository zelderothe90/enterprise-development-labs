using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Services
{
    /// <summary>
    /// Интерфейс репозитория для работы с препаратами
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>Получить все препараты</summary>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>Найти препарат по ID</summary>
        Task<Product?> GetByIdAsync(int id);

        /// <summary>Добавить новый препарат</summary>
        Task AddAsync(Product product);

        /// <summary>Обновить данные препарата</summary>
        Task UpdateAsync(Product product);

        /// <summary>Удалить препарат</summary>
        Task RemoveAsync(int id);

        /// <summary>Поиск препаратов по названию</summary>
        Task<IEnumerable<Product>> GetByNameAsync(string name);

        /// <summary>Получить препараты фармацевтической группы</summary>
        Task<IEnumerable<Product>> GetByPharmaceuticalGroupAsync(int groupId);

        /// <summary>Получить препараты товарной группы</summary>
        Task<IEnumerable<Product>> GetByProductGroupAsync(int groupId);

        /// <summary>Получить препараты с остатком выше указанного</summary>
        Task<IEnumerable<Product>> GetByMinQuantityAsync(int minQuantity);
    }
}