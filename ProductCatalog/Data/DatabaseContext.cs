using ProductCatalog.Models;
using SQLite;

namespace ProductCatalog.Data;

public class DatabaseContext
{
    private SQLiteAsyncConnection? _database;
    private readonly string _dbPath;

    public DatabaseContext()
    {
        // CAMBIA EL NOMBRE DEL ARCHIVO PARA FORZAR UNA BD NUEVA
        _dbPath = Path.Combine(FileSystem.AppDataDirectory, "products_v7.db3");
    }

    private async Task InitializeAsync()
    {
        if (_database != null)
            return;

        _database = new SQLiteAsyncConnection(_dbPath);
        await _database.CreateTableAsync<Product>();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        await InitializeAsync();

        var result = await _database!.Table<Product>().ToListAsync();
        return result;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        await InitializeAsync();

        return await _database!.Table<Product>().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> SearchProductsAsync(string searchText)
    {
        await InitializeAsync();

        var search = (searchText ?? string.Empty).ToLower();

        return await _database!.Table<Product>().Where(p => (p.Name != null && p.Name.ToLower()
                     .Contains(search)) || (p.Category != null && p.Category.ToLower().Contains(search)))
                     .ToListAsync();
    }

    public async Task<int> SaveProductAsync(Product product)
    {
        await InitializeAsync();

        if (product.Id == 0)
        {
            return await _database!.InsertAsync(product);
        }
        else
        {
            return await _database!.UpdateAsync(product);
        }
    }

    public async Task<int> DeleteProductAsync(int id)
    {
        await InitializeAsync();
        return await _database!.DeleteAsync<Product>(id);
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(string category)
    {
        await InitializeAsync();
        return await _database!.Table<Product>().Where(p => p.Category == category).ToListAsync();
    }

    public async Task<List<Product>> GetLowStockProductsAsync(int minimumStock = 5)
    {
        await InitializeAsync();
        return await _database!.Table<Product>().Where(p => p.Stock <= minimumStock).ToListAsync();
    }
}