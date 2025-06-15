using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Services
{
    /// <summary>
    /// Интерфейс репозитория для работы с данными аптек
    /// </summary>
    public interface IPharmacyRepository
    {
        /// <summary>
        /// Получить список всех аптек
        /// </summary>
        Task<IEnumerable<Pharmacy>> GetAllAsync();

        /// <summary>
        /// Получить аптеку по идентификатору
        /// </summary>
        Task<Pharmacy?> GetByIdAsync(int id);

        /// <summary>
        /// Добавить новую аптеку
        /// </summary>
        Task AddAsync(Pharmacy pharmacy);

        /// <summary>
        /// Обновить данные аптеки
        /// </summary>
        Task UpdateAsync(Pharmacy pharmacy);

        /// <summary>
        /// Удалить аптеку по идентификатору
        /// </summary>
        Task RemoveAsync(int id);

        /// <summary>
        /// Получить аптеки по району города
        /// </summary>
        Task<IEnumerable<Pharmacy>> GetByDistrictAsync(string district);

        /// <summary>
        /// Получить аптеки, в которых есть указанный препарат
        /// </summary>
        Task<IEnumerable<Pharmacy>> GetPharmaciesWithProductAsync(int productId);
    }
}