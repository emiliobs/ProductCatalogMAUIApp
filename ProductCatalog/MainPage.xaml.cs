using ProductCatalog.Data;
using ProductCatalog.Models;
using System;
using System.Linq;

namespace ProductCatalog;

public partial class MainPage : ContentPage
{
    private readonly DatabaseContext _database;

    public MainPage()
    {
        InitializeComponent();
        _database = new DatabaseContext();
    }

    // Crear producto de prueba
    private async void OnTestClicked(object sender, EventArgs e)
    {
        try
        {
            var product = new Product
            {
                Name = "iPhone 15yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy",
                Description = "Smartphone de Apple",
                Price = 999.99m,
                Stock = 10,
                Category = "Electronic"
            };

            // Guardar en la base de datos
            await _database.SaveProductAsync(product);

            // Obtener todos los products
            var products = await _database.GetAllProductsAsync();

            var last = products.LastOrDefault();

            if (last is null)
            {
                await DisplayAlertAsync("Prueba DB",
                    "No hay productos guardados.",
                    "OK");
                return;
            }

            // Mostrar resultados
            await DisplayAlertAsync("Prueba DB",
                $"Saved products: {products.Count}\n" +
                $"Last: {last.Name}",
                "OK");
        }
        catch (Exception ex)
        {
            // Si algo revienta (SQLite, null, etc.), lo ves en pantalla
            await DisplayAlertAsync("ERROR",
                ex.ToString(),
                "OK");
        }
    }
}