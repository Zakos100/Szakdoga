using MauiApp1.Viewmodels;
using MauiApp1;
using Microsoft.Extensions.Logging;
using MauiApp1.Views;
using MauiApp1.Database;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<AdminDashBoardPage>();
            builder.Services.AddSingleton<LocalDBService>();

            //Views
            
            
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<LoadingPage>();
            

            //Viewmodels
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<LoadingPageViewModel>();
            builder.Services.AddSingleton<AppShellViewModel>();

            return builder.Build();
        }
    }
}