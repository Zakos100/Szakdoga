using MauiApp1.Controls;
using MauiApp1;
using MauiApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Viewmodels
{
    public class LoadingPageViewModel
    {
        public LoadingPageViewModel() 
        {
            CheckUserLoginDetails();
        }
        private async void CheckUserLoginDetails()
        {
            string userDetailsstr = Preferences.Get(nameof(App.UserDetails), "");

            if (string.IsNullOrWhiteSpace(userDetailsstr))
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    AppShell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    //vissza a bejelentkezéshez

                }
            }
            else
            {
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(userDetailsstr);
                App.UserDetails = userInfo;
                await AppConstant.AddFlyoutMenusDetails();
                //Főmenü
            }
        

            }
        }
    }

