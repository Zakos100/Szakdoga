using MauiApp1.Viewmodels.DashBoard;

namespace MauiApp1.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}