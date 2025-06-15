using System.ComponentModel.DataAnnotations;

namespace NetworkPharmacies.Domain.Model;

/// <summary>
/// Фармацевтическая группа препаратов
/// </summary>
public class PharmaceuticalGroup
{
    /// <summary>
    /// Уникальный идентификатор фармацевтической группы
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Название фармацевтической группы
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Список препаратов, входящих в данную группу
    /// </summary>
    public virtual List<Product> Products { get; set; } = new();

    /// <summary>
    /// Перегрузка метода, возвращающего строковое представление объекта
    /// </summary>
    /// <returns>Название фармацевтической группы</returns>
    public override string ToString() => Name ?? string.Empty;
}