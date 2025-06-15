using System.ComponentModel.DataAnnotations;

namespace NetworkPharmacies.Domain.Model;

/// <summary>
/// Запись в прайс-листе
/// </summary>
public class PriceRecord
{
    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Препарат
    /// </summary>
    public virtual Product? Product { get; set; }

    /// <summary>
    /// Аптека
    /// </summary>
    public virtual Pharmacy? Pharmacy { get; set; }

    /// <summary>
    /// Производитель препарата
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Условие оплаты (наличные/безналичные)
    /// </summary>
    public string? PaymentCondition { get; set; }

    /// <summary>
    /// Реализующая фирма
    /// </summary>
    public string? SellingFirm { get; set; }

    /// <summary>
    /// Стоимость препарата
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Дата актуальности цены
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Перегрузка метода, возвращающего строковое представление объекта
    /// </summary>
    /// <returns>Описание записи прайс-листа</returns>
    public override string ToString() =>
        $"{Product?.Name} в {Pharmacy?.Name} - {Price} руб. (на {Date.ToShortDateString()})";
}