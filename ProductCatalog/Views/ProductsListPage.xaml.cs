using ProductCatalog.ViewModels;
using System.Threading.Tasks;

namespace ProductCatalog.Views;

public partial class ProductsListPage : ContentPage
{
    private readonly ProductsListViewModel _viewModel;

    public ProductsListPage(ProductsListViewModel viewModel)
    {
        InitializeComponent();
        this._viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadProductCommand.ExecuteAsync(null);
    }
}