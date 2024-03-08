using MauiApp1.Viewmodels;

namespace MauiApp1;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;


    }
}