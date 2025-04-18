using MauiApp1.Database;
using MauiApp1.Services;
using MauiApp1.Viewmodels;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace MauiApp1;

public partial class AboutDevices : ContentPage
{
    [Obsolete]
    public ObservableCollection<Database.Device> Device { get; set; } = new ObservableCollection<Database.Device>();

    [Obsolete]

    public AboutDevices()
    {
        InitializeComponent();
        LocalDBService dBService = new LocalDBService();

        if (AppSession.LoggedInUser != null && !string.IsNullOrEmpty(AppSession.LoggedInUser.DeviceID))
        {
            var device = dBService.GetDeviceByID(AppSession.LoggedInUser.DeviceID);
            if (device != null)
                Device.Add(device);
        }

        BindingContext = this;
    }

}
