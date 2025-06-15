using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Services
{
    public class PharmacyNetworkService : IPharmacyNetworkService
    {
        private readonly IPharmacyRepository _pharmacyRepo;
        private readonly IProductRepository _productRepo;
        private readonly IPriceRecordRepository _priceRepo;
        private readonly ISaleRecordRepository _saleRepo;

        public PharmacyNetworkService(
            IPharmacyRepository pharmacyRepo,
            IProductRepository productRepo,
            IPriceRecordRepository priceRepo,
            ISaleRecordRepository saleRepo)
        {
            _pharmacyRepo = pharmacyRepo;
            _productRepo = productRepo;
            _priceRepo = priceRepo;
            _saleRepo = saleRepo;
        }

        public async Task<IEnumerable<Product>> GetProductsByPharmacyAsync(int pharmacyId)
        {
            var pharmacy = await _pharmacyRepo.GetByIdAsync(pharmacyId);
            return pharmacy?.Products.OrderBy(p => p.Name) ?? Enumerable.Empty<Product>();
        }

        public async Task<IEnumerable<PharmacyProductInfo>> GetPharmaciesByProductAsync(int productId)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null) return Enumerable.Empty<PharmacyProductInfo>();

            var pharmacies = await _pharmacyRepo.GetPharmaciesWithProductAsync(productId);
            var result = new List<PharmacyProductInfo>();

            foreach (var pharmacy in pharmacies)
            {
                var pharmacyProduct = pharmacy.Products.First(p => p.Id == productId);
                var price = (await _priceRepo.GetPricesForProductInPharmacyAsync(productId, pharmacy.Id))
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();

                result.Add(new PharmacyProductInfo
                {
                    Pharmacy = pharmacy,
                    Quantity = pharmacyProduct.Quantity,
                    Price = price?.Price ?? 0
                });
            }

            return result;
        }

        public async Task<IEnumerable<PharmacyGroupPriceInfo>> GetAveragePricesByGroupsAsync()
        {
            var result = new List<PharmacyGroupPriceInfo>();
            var pharmacies = await _pharmacyRepo.GetAllAsync();

            foreach (var pharmacy in pharmacies)
            {
                var groups = pharmacy.Products
                    .SelectMany(p => p.PharmaceuticalGroups)
                    .Distinct();

                foreach (var group in groups)
                {
                    var prices = (await _priceRepo.GetAllAsync())
                        .Where(p => p.Pharmacy?.Id == pharmacy.Id &&
                                  p.Product?.PharmaceuticalGroups.Contains(group) == true)
                        .Select(p => p.Price)
                        .ToList();

                    if (prices.Any())
                    {
                        result.Add(new PharmacyGroupPriceInfo
                        {
                            Pharmacy = pharmacy,
                            Group = group,
                            AveragePrice = prices.Average()
                        });
                    }
                }
            }

            return result;
        }

        public async Task<IEnumerable<PharmacySalesInfo>> GetTopPharmaciesByProductSalesAsync(int productId, DateTime startDate, DateTime endDate)
        {
            var sales = (await _saleRepo.GetAllAsync())
                .Where(s => s.Product?.Id == productId &&
                           s.SaleDate >= startDate &&
                           s.SaleDate <= endDate)
                .GroupBy(s => s.Pharmacy)
                .Select(g => new PharmacySalesInfo
                {
                    Pharmacy = g.Key,
                    TotalQuantity = g.Sum(s => s.Quantity),
                    TotalAmount = g.Sum(s => s.TotalAmount)
                })
                .OrderByDescending(s => s.TotalQuantity)
                .ThenByDescending(s => s.TotalAmount)
                .Take(5);

            return sales;
        }

        public async Task<IEnumerable<Pharmacy>> GetPharmaciesInDistrictWithMinSalesAsync(string district, int productId, int minQuantity)
        {
            var sales = (await _saleRepo.GetAllAsync())
                .Where(s => s.Pharmacy?.District.Equals(district, StringComparison.OrdinalIgnoreCase) == true &&
                           s.Product?.Id == productId &&
                           s.Quantity >= minQuantity)
                .Select(s => s.Pharmacy)
                .Distinct();

            return sales;
        }

        public async Task<IEnumerable<Pharmacy>> GetPharmaciesWithMinProductPriceAsync(int productId)
        {
            var minPriceRecord = await _priceRepo.GetMinPriceForProductAsync(productId);
            if (minPriceRecord == null) return Enumerable.Empty<Pharmacy>();

            var pharmacy = await _pharmacyRepo.GetByIdAsync(minPriceRecord.Pharmacy?.Id ?? 0);
            return pharmacy != null ? new[] { pharmacy } : Enumerable.Empty<Pharmacy>();
        }
    }
}