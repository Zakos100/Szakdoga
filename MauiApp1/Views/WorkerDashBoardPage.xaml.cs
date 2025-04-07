using MauiApp1.Database;
using MauiApp1.Viewmodels;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class WorkerDashBoardPage : ContentPage
{
    public ObservableCollection<Users> Users { get; set; } = new ObservableCollection<Users>();
    public WorkerDashBoardPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
        LocalDBService dBService = new LocalDBService();

        Users loggedInUser = dBService.GetUser(viewModel.Username);
        Users.Add(loggedInUser);
        BindingContext = this;
    }
}