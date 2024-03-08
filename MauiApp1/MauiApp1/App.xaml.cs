using MauiApp1.Handlers;
using MauiApp1.Models;
using Microsoft.Maui.Platform;

namespace MauiApp1
{
    public partial class App : Application
    {

        public static UserInfo UserDetails;
        public App()
        {
            InitializeComponent();
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
            {
                if (view is BorderlessEntry)
                {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif __IOS__
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
                }
            });
            MainPage = new AppShell();
        }
    }
}