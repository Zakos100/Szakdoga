using MauiApp1.Database;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace MauiApp1.Views;

public partial class AdminDashBoardPage : ContentPage
{
    

    public ObservableCollection<Users> Users { get; set; } = new ObservableCollection<Users>();
    public AdminDashBoardPage()
    {
        InitializeComponent();
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AdminDashBoardPage)).Assembly;
        
        {
            using (Stream stream = assembly.GetManifestResourceStream("MauiApp1.WorkersDB.db"))
            if (stream !=null)    
            {
                using ( MemoryStream memoryStream = new())
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
        foreach(var users in dBService.List())
        {
            Users.Add(users);
        }
        BindingContext = this;

    }

}