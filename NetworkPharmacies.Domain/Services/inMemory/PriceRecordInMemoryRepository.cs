using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Services.inMemory
{
    public class PriceRecordInMemoryRepository : IPriceRecordRepository
    {
        private readonly List<PriceRecord> _priceRecords = [];
        private int _nextId = 1;

        public async Task<IEnumerable<PriceRecord>> GetAllAsync()
            => await Task.FromResult(_priceRecords.AsEnumerable());

        public async Task<PriceRecord?> GetByIdAsync(int id)
            => await Task.FromResult(_priceRecords.FirstOrDefault(pr => pr.Id == id));

        public async Task AddAsync(PriceRecord priceRecord)
        {
            priceRecord.Id = _nextId++;
            _priceRecords.Add(priceRecord);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(PriceRecord priceRecord)
        {
            var existing = _priceRecords.FirstOrDefault(pr => pr.Id == priceRecord.Id);
            if (existing != null)
            {
                existing.Product = priceRecord.Product;
                existing.Pharmacy = priceRecord.Pharmacy;
                existing.Manufacturer = priceRecord.Manufacturer;
                existing.PaymentCondition = priceRecord.PaymentCondition;
                existing.SellingFirm = priceRecord.SellingFirm;
                existing.Price = priceRecord.Price;
                existing.Date = priceRecord.Date;
            }
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(int id)
        {
            var record = _priceRecords.FirstOrDefault(pr => pr.Id == id);
            if (record != null)
            {
                _priceRecords.Remove(record);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<PriceRecord>> GetCurrentPricesForProductAsync(int productId)
        {
            var latestDates = _priceRecords
                .Where(pr => pr.Product?.Id == productId)
                .GroupBy(pr => pr.Pharmacy?.Id)
                .Select(g => g.Max(pr => pr.Date));

            return await Task.FromResult(_priceRecords
                .Where(pr => pr.Product?.Id == productId && latestDates.Contains(pr.Date)));
        }

        public async Task<IEnumerable<PriceRecord>> GetPricesForProductInPharmacyAsync(int productId, int pharmacyId)
            => await Task.FromResult(_priceRecords
                .Where(pr => pr.Product?.Id == productId && pr.Pharmacy?.Id == pharmacyId)
                .OrderByDescending(pr => pr.Date));

        public async Task<PriceRecord?> GetMinPriceForProductAsync(int productId)
        {
            var currentPrices = await GetCurrentPricesForProductAsync(productId);
            return currentPrices.OrderBy(pr => pr.Price).FirstOrDefault();
        }
    }
}