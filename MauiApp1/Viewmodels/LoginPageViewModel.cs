using MauiApp1.Models;
using MauiApp1;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Action;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MauiApp1.Viewmodels
{
    public partial class LoginPageViewModel : BaseViewModel
    {

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _hidePassword;



        [ICommand]

        public async void Login()
        {
            if (!string.IsNullOrWhiteSpace(Username))
            {
                var userDetails = new UserInfo();
                userDetails.Username = Username;
                userDetails.FullName = "Bejelentkezve, mint: \n Zarándi Ákos";

                // Student Role, Teacher Role, Admin Role,
                List<Users> users = dBService.ListUsers();
                bool isUserExist = users.Select(user => user.Username).Contains(Username);
                bool isPaswordCorrect = users.Where(u => u.Username == Username).Select(u => u.Password).FirstOrDefault() == HidePassword;


                if (Username.ToLower().Contains("worker"))
                {
                    userDetails.RoleID = (int)RoleDetails.Worker;
                    userDetails.RoleText = "Worker";
                }
                else if (Username.ToLower().Contains("admin"))
                {
                    userDetails.RoleID = (int)RoleDetails.Admin;
                    userDetails.RoleText = "Admin";
                }

                // calling api 


                if (Preferences.ContainsKey(nameof(App.UserDetails)))
                {
                    Preferences.Remove(nameof(App.UserDetails));
                }

                string userDetailStr = JsonConvert.SerializeObject(userDetails);
                Preferences.Set(nameof(App.UserDetails), userDetailStr);
                App.UserDetails = userDetails;
                await AppConstant.AddFlyoutMenusDetails();
            }


        }
    }
}
