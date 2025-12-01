using ProductCatalog.ViewModels;
using ProductCatalog.Views;

namespace ProductCatalog
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("ProductDetailPage", typeof(ProductDetailPage));
        }
    }
}