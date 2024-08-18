using MauiApp1.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Viewmodels.DashBoard
{
    public partial class DashboardPageViewModel : BaseViewModel
    {
        public DashboardPageViewModel()
        {
            AppShell.Current.FlyoutHeader = new FlyOutHeaderControl();
        }
    }
}
