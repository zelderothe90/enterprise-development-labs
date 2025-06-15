using System.ComponentModel.DataAnnotations;

namespace NetworkPharmacies.Domain.Model;

/// <summary>
/// Аптека
/// </summary>
public class Pharmacy
{
    /// <summary>
    /// Уникальный идентификатор аптеки
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Название аптеки
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Телефон аптеки
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Адрес аптеки
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Район города, где расположена аптека
    /// </summary>
    public string? District { get; set; }

    /// <summary>
    /// ФИО директора аптеки
    /// </summary>
    public string? DirectorName { get; set; }

    /// <summary>
    /// Список препаратов, доступных в аптеке
    /// </summary>
    public virtual List<Product> Products { get; set; } = new();

    /// <summary>
    /// Список записей о продажах в данной аптеке
    /// </summary>
    public virtual List<SaleRecord> SaleRecords { get; set; } = new();

    /// <summary>
    /// Перегрузка метода, возвращающего строковое представление объекта
    /// </summary>
    /// <returns>Описание аптеки</returns>
    public override string ToString() =>
        $"{Name} ({Address}, {District}) - тел.: {Phone}";
}