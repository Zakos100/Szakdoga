using MauiApp1.Database;
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
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AboutDevices)).Assembly;

        {
            using (Stream stream = assembly.GetManifestResourceStream("MauiApp1.WorkersDB.db"))
                if (stream != null)
                {
                    using (MemoryStream memoryStream = new())
                    {
                        stream.CopyTo(memoryStream);

                        File.WriteAllBytes(LocalDBService.DB_Name, memoryStream.ToArray());

                    }

                }
                else
                {

                }
        }


        LocalDBService dBService = new LocalDBService();
        foreach (var device in dBService.ListDevices())
        {
            Device.Add(device);
        }
        BindingContext = this;

    }
}