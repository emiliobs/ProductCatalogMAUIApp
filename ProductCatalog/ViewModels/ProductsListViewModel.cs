using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductCatalog.Data;
using ProductCatalog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.ViewModels;

public partial class ProductsListViewModel : ObservableObject
{
    private readonly DatabaseContext _database;

    [ObservableProperty]
    private ObservableCollection<Product> products = new();

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string searchText = string.Empty;

    public ProductsListViewModel(DatabaseContext database)
    {
        this._database = database;
        //AddSampleProductsAsync();
    }

    [RelayCommand]
    private async Task LoadProductAsync()
    {
        try
        {
            IsLoading = true;
            var prodcutList = await _database.GetAllProductsAsync();

            Products.Clear();
            foreach (var product in prodcutList)
            {
                Products.Add(product);
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
    private async Task AddProductAsync()
    {
        await Shell.Current.GoToAsync("ProductDetailPage");
    }

    [RelayCommand]
    private async Task SearchProductsAsync()
    {
        try
        {
            IsLoading = true;

            List<Product> result;

            // If search is empty, get all products
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                result = await _database.GetAllProductsAsync();
            }
            else
            {
                result = await _database.SearchProductsAsync(SearchText);
            }

            // Update collection
            Products.Clear();
            foreach (var product in result)
            {
                Products.Add(product);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Searching products: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task EditProductAsync(Product product)
    {
        var parameters = new Dictionary<string, object>()
       {
           { "ProductId",  product.Id}
       };

        await Shell.Current.GoToAsync("ProductDetailPage", parameters);
    }

    [RelayCommand]
    private async Task DeleteProductAsync(Product product)
    {
        if (product is null)
        {
            await Application.Current.MainPage.DisplayAlertAsync("Error", "Prodcut not found", "OK");
            return;
        }

        bool confirm = await Application.Current!.MainPage!.DisplayAlertAsync(
            "Confirm",
            $"Delete '{product.Name}'?",
            "Delete",
            "Cancel");

        if (!confirm)
        {
            return;
        }

        await _database.DeleteProductAsync(product.Id);
        Products.Remove(product);
        await Application.Current.MainPage.DisplayAlertAsync("Success", "Product Delete", "OK");
    }

    [RelayCommand]
    private async Task AddSampleProductsAsync()
    {
        try
        {
            // Create some sample products
            List<Product> sampleProducts = new List<Product>()
{
    new Product
    {
        Name = "iPhone 15",
        Description = "Latest generation Apple smartphone with advanced camera and A16 chip.",
        Price = 999m,
        Stock = 10,
        Category = "Electronics"
    },
    new Product
    {
        Name = "Samsung Galaxy S23",
        Description = "High-end Android phone with AMOLED display and triple camera system.",
        Price = 899m,
        Stock = 15,
        Category = "Electronics"
    },
    new Product
    {
        Name = "MacBook Pro 14\"",
        Description = "Powerful laptop with Retina display, ideal for development and design.",
        Price = 2499m,
        Stock = 5,
        Category = "Electronics"
    },
    new Product
    {
        Name = "Dell XPS 13",
        Description = "Compact ultrabook with slim bezels and strong performance for daily work.",
        Price = 1499m,
        Stock = 7,
        Category = "Electronics"
    },
    new Product
    {
        Name = "iPad Air",
        Description = "Lightweight tablet perfect for media consumption, notes, and casual work.",
        Price = 699m,
        Stock = 12,
        Category = "Electronics"
    },

    new Product
    {
        Name = "Nike Air Max",
        Description = "Comfortable sneakers with air cushioning for everyday wear and sports.",
        Price = 150m,
        Stock = 25,
        Category = "Sports"
    },
    new Product
    {
        Name = "Adidas Ultraboost",
        Description = "Running shoes with responsive cushioning for long-distance training.",
        Price = 160m,
        Stock = 20,
        Category = "Sports"
    },
    new Product
    {
        Name = "Puma Running Shorts",
        Description = "Lightweight and breathable shorts designed for running and workouts.",
        Price = 35m,
        Stock = 40,
        Category = "Sports"
    },
    new Product
    {
        Name = "Wilson Basketball",
        Description = "Official size basketball suitable for indoor or outdoor courts.",
        Price = 45m,
        Stock = 30,
        Category = "Sports"
    },
    new Product
    {
        Name = "Yoga Mat Pro",
        Description = "Non-slip yoga mat with extra cushioning for comfort and stability.",
        Price = 60m,
        Stock = 18,
        Category = "Sports"
    },

    new Product
    {
        Name = "The Pragmatic Programmer",
        Description = "Classic software development book with practical tips and best practices.",
        Price = 45m,
        Stock = 14,
        Category = "Books"
    },
    new Product
    {
        Name = "Clean Code",
        Description = "Guide to writing readable, maintainable, and high-quality code.",
        Price = 50m,
        Stock = 9,
        Category = "Books"
    },
    new Product
    {
        Name = "Design Patterns",
        Description = "Reference book on common object-oriented design patterns and solutions.",
        Price = 65m,
        Stock = 6,
        Category = "Books"
    },
    new Product
    {
        Name = "Lord of the Rings Box Set",
        Description = "Complete trilogy of the epic fantasy novels by J.R.R. Tolkien.",
        Price = 80m,
        Stock = 8,
        Category = "Books"
    },
    new Product
    {
        Name = "Harry Potter Collection",
        Description = "Box set of the Harry Potter series, perfect for fantasy lovers.",
        Price = 90m,
        Stock = 10,
        Category = "Books"
    },

    new Product
    {
        Name = "Office Chair Ergonomic",
        Description = "Ergonomic office chair with lumbar support for long working hours.",
        Price = 199m,
        Stock = 11,
        Category = "Home"
    },
    new Product
    {
        Name = "Standing Desk",
        Description = "Height-adjustable desk to switch between sitting and standing.",
        Price = 399m,
        Stock = 4,
        Category = "Home"
    },
    new Product
    {
        Name = "LED Desk Lamp",
        Description = "Adjustable LED lamp with soft light for work or study desks.",
        Price = 45m,
        Stock = 22,
        Category = "Home"
    },
    new Product
    {
        Name = "Coffee Maker",
        Description = "Compact drip coffee maker ideal for home or small office use.",
        Price = 89m,
        Stock = 16,
        Category = "Home"
    },
    new Product
    {
        Name = "Smart TV 55\"",
        Description = "55-inch 4K Smart TV with streaming apps and HDR support.",
        Price = 799m,
        Stock = 9,
        Category = "Electronics"
    }
};

            foreach (var product in sampleProducts)
            {
                await _database.SaveProductAsync(product);
            }

            await LoadProductAsync();
        }
        catch (Exception ex)
        {
            // Show the real erro instead of just JavaProxythrowable
            await Shell.Current.DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }
}