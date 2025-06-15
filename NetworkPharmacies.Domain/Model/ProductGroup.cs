using System.ComponentModel.DataAnnotations;

namespace NetworkPharmacies.Domain.Model;

/// <summary>
/// Товарная группа препаратов
/// </summary>
public class ProductGroup
{
    /// <summary>
    /// Уникальный идентификатор товарной группы
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Название товарной группы
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Список препаратов, входящих в данную группу
    /// </summary>
    public virtual List<Product> Products { get; set; } = new();

    /// <summary>
    /// Перегрузка метода, возвращающего строковое представление объекта
    /// </summary>
    /// <returns>Название товарной группы</returns>
    public override string ToString() => Name ?? string.Empty;
}