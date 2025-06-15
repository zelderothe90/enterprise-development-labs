using System.ComponentModel.DataAnnotations;

namespace NetworkPharmacies.Domain.Model;

/// <summary>
/// Препарат (лекарственное средство)
/// </summary>
public class Product
{
    /// <summary>
    /// Уникальный идентификатор препарата
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Код препарата
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Наименование препарата
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Товарная группа препарата
    /// </summary>
    public virtual ProductGroup? Group { get; set; }

    /// <summary>
    /// Фармацевтические группы, к которым относится препарат
    /// </summary>
    public virtual List<PharmaceuticalGroup> PharmaceuticalGroups { get; set; } = new();

    /// <summary>
    /// Количество единиц препарата в наличии
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Список записей о ценах на данный препарат
    /// </summary>
    public virtual List<PriceRecord> PriceRecords { get; set; } = new();

    /// <summary>
    /// Перегрузка метода, возвращающего строковое представление объекта
    /// </summary>
    /// <returns>Описание препарата</returns>
    public override string ToString() =>
        $"{Code} - {Name} (Остаток: {Quantity})";
}