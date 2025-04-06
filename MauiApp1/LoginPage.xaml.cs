using MauiApp1.Viewmodels;
using MauiApp1.Views;
using System.Diagnostics;

namespace MauiApp1;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
        
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        if (BindingContext is LoginPageViewModel viewModel)
        {
            if (viewModel.LoginCommand.CanExecute(null))
            {
                viewModel.LoginCommand.Execute(null);
            }
            
        }
        
        
    }




}