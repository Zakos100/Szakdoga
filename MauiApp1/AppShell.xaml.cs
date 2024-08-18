using MauiApp1.Viewmodels;
using MauiApp1.Models;
using MauiApp1.Viewmodels.DashBoard;

namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.BindingContext = new AppShellViewModel();
        }
    }
}