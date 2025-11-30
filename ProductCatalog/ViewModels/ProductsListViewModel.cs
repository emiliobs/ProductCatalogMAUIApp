using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductCatalog.Data;
using ProductCatalog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProductCatalog.ViewModels;

public partial class ProductsListViewModel : ObservableObject
{
    private readonly DatabaseContext _database;

    [ObservableProperty]
    private ObservableCollection<Product> products = new();

    [ObservableProperty]
    private bool isLoading;

    public ProductsListViewModel(DatabaseContext database)
    {
        this._database = database;
    }

    [RelayCommand]
    private async Task LoadProductAsync()
    {
        try
        {
            IsLoading = true;
            var prodcutList = await _database.GetAllProductsAsync();

            products.Clear();
            foreach (var product in prodcutList)
            {
                products.Add(product);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Something bad happening: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddSampleProductsAsync()
    {
        // Create some sample products
        List<Product> sampleProducts = new List<Product>()
         {
            new Product { Name = "iPhone 15", Price = 999m, Stock = 10, Category = "Electronics" },
            new Product { Name = "Samsung Galaxy S23", Price = 899m, Stock = 15, Category = "Electronics" },
            new Product { Name = "MacBook Pro 14\"", Price = 2499m, Stock = 5, Category = "Electronics" },
            new Product { Name = "Dell XPS 13", Price = 1499m, Stock = 7, Category = "Electronics" },
            new Product { Name = "iPad Air", Price = 699m, Stock = 12, Category = "Electronics" },

            new Product { Name = "Nike Air Max", Price = 150m, Stock = 25, Category = "Sports" },
            new Product { Name = "Adidas Ultraboost", Price = 160m, Stock = 20, Category = "Sports" },
            new Product { Name = "Puma Running Shorts", Price = 35m, Stock = 40, Category = "Sports" },
            new Product { Name = "Wilson Basketball", Price = 45m, Stock = 30, Category = "Sports" },
            new Product { Name = "Yoga Mat Pro", Price = 60m, Stock = 18, Category = "Sports" },

            new Product { Name = "The Pragmatic Programmer", Price = 45m, Stock = 14, Category = "Books" },
            new Product { Name = "Clean Code", Price = 50m, Stock = 9, Category = "Books" },
            new Product { Name = "Design Patterns", Price = 65m, Stock = 6, Category = "Books" },
            new Product { Name = "Lord of the Rings Box Set", Price = 80m, Stock = 8, Category = "Books" },
            new Product { Name = "Harry Potter Collection", Price = 90m, Stock = 10, Category = "Books" },

            new Product { Name = "Office Chair Ergonomic", Price = 199m, Stock = 11, Category = "Home" },
            new Product { Name = "Standing Desk", Price = 399m, Stock = 4, Category = "Home" },
            new Product { Name = "LED Desk Lamp", Price = 45m, Stock = 22, Category = "Home" },
            new Product { Name = "Coffee Maker", Price = 89m, Stock = 16, Category = "Home" },
            new Product { Name = "Smart TV 55\"", Price = 799m, Stock = 9, Category = "Electronics" }
         };

        foreach (var prodcut in sampleProducts)
        {
            await _database.SaveProductAsync(prodcut);
        }

        await LoadProductAsync();
    }
}