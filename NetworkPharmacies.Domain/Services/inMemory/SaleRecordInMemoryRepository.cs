using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Services.inMemory
{
    public class SaleRecordInMemoryRepository : ISaleRecordRepository
    {
        private readonly List<SaleRecord> _saleRecords = new();
        private int _nextId = 1;

        public async Task<IEnumerable<SaleRecord>> GetAllAsync()
            => await Task.FromResult(_saleRecords.AsEnumerable());

        public async Task<SaleRecord?> GetByIdAsync(int id)
            => await Task.FromResult(_saleRecords.FirstOrDefault(s => s.Id == id));

        public async Task AddAsync(SaleRecord saleRecord)
        {
            saleRecord.Id = _nextId++;
            _saleRecords.Add(saleRecord);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(SaleRecord saleRecord)
        {
            var existing = _saleRecords.FirstOrDefault(s => s.Id == saleRecord.Id);
            if (existing != null)
            {
                existing.Product = saleRecord.Product;
                existing.Pharmacy = saleRecord.Pharmacy;
                existing.Quantity = saleRecord.Quantity;
                existing.TotalAmount = saleRecord.TotalAmount;
                existing.SaleDate = saleRecord.SaleDate;
            }
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(int id)
        {
            var record = _saleRecords.FirstOrDefault(s => s.Id == id);
            if (record != null)
            {
                _saleRecords.Remove(record);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<SaleRecord>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
            => await Task.FromResult(_saleRecords
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate));
    }
}