using MauiApp1.Database;
using MauiApp1.Models;
using MauiApp1.Viewmodels;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Windows.System;

namespace MauiApp1.Views;

public partial class AdminDashBoardPage : ContentPage
{

    public ObservableCollection<Users> Users { get; set; } = new ObservableCollection<Users>();

    public AdminDashBoardPage(LoginPageViewModel viewModel)
    {
        
        InitializeComponent();
        // this.BindingContext = viewModel;

        LocalDBService dBService = new LocalDBService();

        Users loggedInUser = dBService.GetUser(viewModel.Username);
        Users.Add(loggedInUser);
        BindingContext = this;
        
    }

}