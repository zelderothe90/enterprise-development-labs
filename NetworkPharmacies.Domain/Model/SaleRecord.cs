using System.ComponentModel.DataAnnotations;

namespace NetworkPharmacies.Domain.Model;

/// <summary>
/// Запись о продаже препарата
/// </summary>
public class SaleRecord
{
    /// <summary>
    /// Уникальный идентификатор записи о продаже
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Препарат
    /// </summary>
    public virtual Product? Product { get; set; }

    /// <summary>
    /// Аптека, где была совершена продажа
    /// </summary>
    public virtual Pharmacy? Pharmacy { get; set; }

    /// <summary>
    /// Количество проданных единиц
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Общая сумма продажи
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Дата продажи
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Перегрузка метода, возвращающего строковое представление объекта
    /// </summary>
    /// <returns>Описание записи о продаже</returns>
    public override string ToString() =>
        $"{Quantity} x {Product?.Name} в {Pharmacy?.Name} на {TotalAmount} руб. ({SaleDate.ToShortDateString()})";
}