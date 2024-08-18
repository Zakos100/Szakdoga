using MauiApp1.Viewmodels;

namespace MauiApp1;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;

    }


}