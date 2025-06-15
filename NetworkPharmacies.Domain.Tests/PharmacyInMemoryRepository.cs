using NetworkPharmacies.Domain.Model;
using NetworkPharmacies.Domain.Services.inMemory;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetworkPharmacies.Domain.Tests
{
    public class ProductInMemoryRepositoryTests
    {
        private readonly ProductInMemoryRepository _repository;

        public ProductInMemoryRepositoryTests()
        {
            _repository = new ProductInMemoryRepository();
            InitializeTestData().Wait();
        }

        private async Task InitializeTestData()
        {
            var vitaminGroup = new PharmaceuticalGroup { Id = 1, Name = "Витамины" };
            var painkillersGroup = new PharmaceuticalGroup { Id = 2, Name = "Обезболивающие" };

            await _repository.AddAsync(new Product
            {
                Id = 1,
                Code = "VIT001",
                Name = "Витамин C",
                Quantity = 100,
                Group = new ProductGroup { Id = 1, Name = "Витамины" },
                PharmaceuticalGroups = new List<PharmaceuticalGroup> { vitaminGroup }
            });

            await _repository.AddAsync(new Product
            {
                Id = 2,
                Code = "PAN001",
                Name = "Парацетамол",
                Quantity = 50,
                Group = new ProductGroup { Id = 2, Name = "Анальгетики" },
                PharmaceuticalGroups = new List<PharmaceuticalGroup> { painkillersGroup }
            });
        }

        #region Basic CRUD Tests
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProducts()
        {
 
            var result = await _repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnProduct()
        {
           
            var existingId = 1;

            var result = await _repository.GetByIdAsync(existingId);

            Assert.NotNull(result);
            Assert.Equal(existingId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            var invalidId = -1;

            var result = await _repository.GetByIdAsync(invalidId);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_NewProduct_ShouldAddProduct()
        {
            var newProduct = new Product
            {
                Id = 3,
                Code = "NEW001",
                Name = "Новый препарат",
                Quantity = 75
            };

            await _repository.AddAsync(newProduct);
            var addedProduct = await _repository.GetByIdAsync(newProduct.Id);

            Assert.NotNull(addedProduct);
            Assert.Equal(newProduct.Code, addedProduct.Code);
        }

        [Fact]
        public async Task UpdateAsync_ExistingProduct_ShouldUpdateProperties()
        {
            var existingProduct = (await _repository.GetAllAsync()).First();
            var updatedProduct = new Product
            {
                Id = existingProduct.Id,
                Code = "UPD001",
                Name = "Обновленный препарат",
                Quantity = existingProduct.Quantity + 10
            };

            await _repository.UpdateAsync(updatedProduct);
            var productAfterUpdate = await _repository.GetByIdAsync(existingProduct.Id);

            Assert.Equal(updatedProduct.Code, productAfterUpdate.Code);
            Assert.Equal(updatedProduct.Name, productAfterUpdate.Name);
            Assert.Equal(updatedProduct.Quantity, productAfterUpdate.Quantity);
        }

        [Fact]
        public async Task RemoveAsync_ExistingProduct_ShouldRemoveProduct()
        {
            var existingProduct = (await _repository.GetAllAsync()).First();

            await _repository.RemoveAsync(existingProduct.Id);
            var productAfterDelete = await _repository.GetByIdAsync(existingProduct.Id);

            Assert.Null(productAfterDelete);
        }
        #endregion

        #region Product-Specific Tests
        [Fact]
        public async Task GetByNameAsync_ShouldReturnMatchingProducts()
        {
            var result = await _repository.GetByNameAsync("Витамин");

            Assert.Single(result);
            Assert.Equal("Витамин C", result.First().Name);
        }

        [Fact]
        public async Task GetByPharmaceuticalGroupAsync_ShouldReturnGroupProducts()
        {
            var groupId = 2;

            var result = await _repository.GetByPharmaceuticalGroupAsync(groupId);

            Assert.Single(result);
            Assert.Equal("Парацетамол", result.First().Name);
        }

        [Fact]
        public async Task GetByProductGroupAsync_ShouldReturnGroupProducts()
        {
            var groupId = 1;

            var result = await _repository.GetByProductGroupAsync(groupId);

            Assert.Single(result);
            Assert.Equal("Витамин C", result.First().Name);
        }

        [Theory]
        [InlineData(50, 2)]
        [InlineData(75, 1)]
        [InlineData(100, 1)]
        public async Task GetByMinQuantityAsync_ShouldFilterCorrectly(int minQuantity, int expectedCount)
        {
            var result = await _repository.GetByMinQuantityAsync(minQuantity);

            Assert.Equal(expectedCount, result.Count());
            Assert.All(result, p => Assert.True(p.Quantity >= minQuantity));
        }

        [Fact]
        public async Task GetByMinQuantityAsync_WithHighQuantity_ShouldReturnEmpty()
        {
            var minQuantity = 1000;

            var result = await _repository.GetByMinQuantityAsync(minQuantity);

            Assert.Empty(result);
        }
        #endregion
    }
}