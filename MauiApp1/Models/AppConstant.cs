using MauiApp1;
using MauiApp1.Controls;
using MauiApp1.Models;
using MauiApp1.Views;
using SimpleLoginUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class AppConstant
    {

        public async static Task AddFlyoutMenusDetails()
        {
            AppShell.Current.FlyoutHeader = new FlyOutHeaderControl();

            var studentDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(WorkerDashBoardPage)).FirstOrDefault();
            if (studentDashboardInfo != null) AppShell.Current.Items.Remove(studentDashboardInfo);


            var adminDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(AdminDashBoardPage)).FirstOrDefault();
            if (adminDashboardInfo != null) AppShell.Current.Items.Remove(adminDashboardInfo);


            if (App.UserDetails.RoleID == (int)RoleDetails.Worker)
            {
                var flyoutItem = new FlyoutItem()
                {
                    Title = "Dashboard Page",
                    Route = nameof(WorkerDashBoardPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                            {
                                new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = "Worker Dashboard",
                                    ContentTemplate = new DataTemplate(typeof(WorkerDashBoardPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Worker Profile",
                                    ContentTemplate = new DataTemplate(typeof(WorkerDashBoardPage)),
                                },
                            }
                };
                if (!AppShell.Current.Items.Contains(flyoutItem))
                {
                    AppShell.Current.Items.Add(flyoutItem);
                    if (DeviceInfo.Platform == DevicePlatform.WinUI)
                    {
                        AppShell.Current.Dispatcher.Dispatch(async () =>
                        {
                            await Shell.Current.GoToAsync($"//{nameof(WorkerDashBoardPage)}");
                        });
                    }
                    else
                    {
                        await Shell.Current.GoToAsync($"//{nameof(WorkerDashBoardPage)}");
                    }
                }

            }


            if (App.UserDetails.RoleID == (int)RoleDetails.Admin)
            {
                var flyoutItem = new FlyoutItem()
                {
                    Title = "Dashboard Page",
                    Route = nameof(AdminDashBoardPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                                new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = "Admin Dashboard",
                                    ContentTemplate = new DataTemplate(typeof(AdminDashBoardPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Admin Profile",
                                    ContentTemplate = new DataTemplate(typeof(AdminDashBoardPage)),
                                },
                   }
                };

                if (!AppShell.Current.Items.Contains(flyoutItem))
                {
                    AppShell.Current.Items.Add(flyoutItem);
                    if (DeviceInfo.Platform == DevicePlatform.WinUI)
                    {
                        AppShell.Current.Dispatcher.Dispatch(async () =>
                        {
                            await Shell.Current.GoToAsync($"//{nameof(AdminDashBoardPage)}");
                        });
                    }
                    else
                    {
                        await Shell.Current.GoToAsync($"//{nameof(AdminDashBoardPage)}");
                    }
                }


            }
        }
    }
}
