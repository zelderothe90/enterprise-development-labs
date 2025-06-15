using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Services
{
    /// <summary>
    /// Интерфейс репозитория для работы с записями цен на препараты
    /// </summary>
    public interface IPriceRecordRepository
    {
        /// <summary>
        /// Получить все записи о ценах
        /// </summary>
        Task<IEnumerable<PriceRecord>> GetAllAsync();

        /// <summary>
        /// Получить запись о цене по идентификатору
        /// </summary>
        Task<PriceRecord?> GetByIdAsync(int id);

        /// <summary>
        /// Добавить новую запись о цене
        /// </summary>
        Task AddAsync(PriceRecord priceRecord);

        /// <summary>
        /// Обновить существующую запись о цене
        /// </summary>
        Task UpdateAsync(PriceRecord priceRecord);

        /// <summary>
        /// Удалить запись о цене по идентификатору
        /// </summary>
        Task RemoveAsync(int id);

        /// <summary>
        /// Получить текущие цены на указанный препарат
        /// </summary>
        Task<IEnumerable<PriceRecord>> GetCurrentPricesForProductAsync(int productId);

        /// <summary>
        /// Получить историю цен на препарат в конкретной аптеке
        /// </summary>
        Task<IEnumerable<PriceRecord>> GetPricesForProductInPharmacyAsync(int productId, int pharmacyId);

        /// <summary>
        /// Найти минимальную цену на указанный препарат
        /// </summary>
        Task<PriceRecord?> GetMinPriceForProductAsync(int productId);
    }
}