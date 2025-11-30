using Microsoft.Extensions.Logging;
using ProductCatalog.Data;
using ProductCatalog.Models;
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

            //Register Register
            builder.Services.AddTransient<ProductsListViewModel>();

            // Regiters Views
            builder.Services.AddTransient<ProductsListPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}