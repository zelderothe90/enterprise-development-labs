using NetworkPharmacies.Domain.Data;
using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Services.inMemory
{
    public class PharmacyInMemoryRepository : IPharmacyRepository
    {
        private readonly List<Pharmacy> _pharmacies = DataSeeder.SeedPharmacies();

        public async Task<IEnumerable<Pharmacy>> GetAllAsync()
            => await Task.FromResult(_pharmacies);

        public async Task<Pharmacy?> GetByIdAsync(int id)
            => await Task.FromResult(_pharmacies.FirstOrDefault(p => p.Id == id));

        public async Task AddAsync(Pharmacy pharmacy)
        {
            pharmacy.Id = _pharmacies.Max(p => p.Id) + 1;
            _pharmacies.Add(pharmacy);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Pharmacy pharmacy)
        {
            var existing = _pharmacies.FirstOrDefault(p => p.Id == pharmacy.Id);
            if (existing != null)
            {
                existing.Name = pharmacy.Name;
                existing.Phone = pharmacy.Phone;
                existing.Address = pharmacy.Address;
                existing.District = pharmacy.District;
                existing.DirectorName = pharmacy.DirectorName;
            }
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(int id)
        {
            var pharmacy = _pharmacies.FirstOrDefault(p => p.Id == id);
            if (pharmacy != null)
            {
                _pharmacies.Remove(pharmacy);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Pharmacy>> GetByDistrictAsync(string district)
            => await Task.FromResult(_pharmacies.Where(p => p.District.Equals(district, StringComparison.OrdinalIgnoreCase)));

        public async Task<IEnumerable<Pharmacy>> GetPharmaciesWithProductAsync(int productId)
            => await Task.FromResult(_pharmacies.Where(p => p.Products.Any(prod => prod.Id == productId)));
    }
}