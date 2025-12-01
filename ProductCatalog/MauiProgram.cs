using Microsoft.Extensions.Logging;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Services;
using ProductCatalog.ViewModels;
using ProductCatalog.Views;

namespace ProductCatalog
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register Data Bases
            builder.Services.AddSingleton<DatabaseContext>();

            //Register services
            builder.Services.AddSingleton<ImageService>();

            //Register viewmodels
            builder.Services.AddTransient<ProductsListViewModel>();
            builder.Services.AddTransient<ProductDetailViewModel>();

            // Regiters Views
            builder.Services.AddTransient<ProductsListPage>();
            builder.Services.AddTransient<ProductDetailPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}