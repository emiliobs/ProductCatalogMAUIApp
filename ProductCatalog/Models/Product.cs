using SQLite;

namespace ProductCatalog.Models;

[Table("products")]
public class Product
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(100), NotNull]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ImagePath { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Ignore]
    public bool HasImage => !string.IsNullOrEmpty(ImagePath);

    [Ignore]
    public bool OutOfStock => Stock == 0;
}