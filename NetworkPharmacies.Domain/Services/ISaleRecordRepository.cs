using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Services
{
    /// <summary>
    /// Интерфейс репозитория для работы с записями о продажах препаратов
    /// </summary>
    public interface ISaleRecordRepository
    {
        /// <summary>
        /// Получить все записи о продажах
        /// </summary>
        Task<IEnumerable<SaleRecord>> GetAllAsync();

        /// <summary>
        /// Получить запись о продаже по идентификатору
        /// </summary>
        Task<SaleRecord?> GetByIdAsync(int id);

        /// <summary>
        /// Добавить новую запись о продаже
        /// </summary>
        Task AddAsync(SaleRecord saleRecord);

        /// <summary>
        /// Обновить существующую запись о продаже
        /// </summary>
        Task UpdateAsync(SaleRecord saleRecord);

        /// <summary>
        /// Удалить запись о продаже по идентификатору
        /// </summary>
        Task RemoveAsync(int id);

        /// <summary>
        /// Получить записи о продажах за указанный период
        /// </summary>
        Task<IEnumerable<SaleRecord>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}