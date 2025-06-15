using NetworkPharmacies.Domain.Model;

namespace NetworkPharmacies.Domain.Data
{

    /// <summary>
    /// Класс для заполнения базы данных тестовыми данными
    /// </summary>
    public static class DataSeeder
    {
        public static List<Product> SeedProducts()
        {
            return
    [
        new() {
            Id = 1,
            Code = "PAN01",
            Name = "Парацетамол",
            Quantity = 150,
            Group = new ProductGroup { Id = 1, Name = "Анальгетики" },
            PharmaceuticalGroups = new List<PharmaceuticalGroup>
            {
                new() { Id = 1, Name = "Жаропонижающие" },
                new() { Id = 2, Name = "Обезболивающие" }
            }
        },
        new()
        {
            Id = 2,
            Code = "VITC01",
            Name = "Витамин C",
            Quantity = 200,
            Group = new ProductGroup { Id = 2, Name = "Витамины" },
            PharmaceuticalGroups = new List<PharmaceuticalGroup>
            {
                new() { Id = 3, Name = "Иммуномодуляторы" }
            }
        }
    ];
        }
        public static List<Pharmacy> SeedPharmacies()
        {
            return new List<Pharmacy>
    {
        new Pharmacy
        {
            Id = 1,
            Name = "Аптека №1",
            Phone = "+375291111111",
            Address = "ул. Ленина, 1",
            District = "Центральный",
            DirectorName = "Иванов И.И."
        }
    };
        }
    }
}