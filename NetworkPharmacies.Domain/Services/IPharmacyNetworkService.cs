using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Services
{
    public interface IPharmacyNetworkService
    {
        /// <summary>
        // 1. Сведения о препаратах в аптеке
        /// </summary>
        Task<IEnumerable<Product>> GetProductsByPharmacyAsync(int pharmacyId);

        /// <summary>
        // 2. Аптеки с препаратом
        /// </summary>
        Task<IEnumerable<PharmacyProductInfo>> GetPharmaciesByProductAsync(int productId);

        /// <summary>
        // 3. Средняя стоимость по группам
        /// </summary>
        Task<IEnumerable<PharmacyGroupPriceInfo>> GetAveragePricesByGroupsAsync();

        /// <summary>
        // 4. Топ-5 аптек по продажам
        /// </summary>
        Task<IEnumerable<PharmacySalesInfo>> GetTopPharmaciesByProductSalesAsync(int productId, DateTime startDate, DateTime endDate);

        /// <summary>
        // 5. Аптеки района с объемом продаж
        /// </summary>
        Task<IEnumerable<Pharmacy>> GetPharmaciesInDistrictWithMinSalesAsync(string district, int productId, int minQuantity);

        /// <summary>
        // 6. Аптеки с минимальной ценой
        /// </summary>
        Task<IEnumerable<Pharmacy>> GetPharmaciesWithMinProductPriceAsync(int productId);
    }

    public class PharmacyProductInfo
    {
        public Pharmacy? Pharmacy { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class PharmacyGroupPriceInfo
    {
        public Pharmacy? Pharmacy { get; set; }
        public PharmaceuticalGroup? Group { get; set; }
        public decimal AveragePrice { get; set; }
    }

    public class PharmacySalesInfo
    {
        public Pharmacy? Pharmacy { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}