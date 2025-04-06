using MauiApp1.Handlers;
using MauiApp1.Interface;
using MauiApp1.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using System.Diagnostics;
using Windows.UI.ViewManagement;
using WinRT.Interop;

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
        protected override void OnStart()
        {
            base.OnStart();

            // 🔹 Teljes képernyő mód aktiválása
            MakeFullScreen();
        }

        private void MakeFullScreen()
        {
            var window = Application.Current.Windows[0]; // Az elsődleges ablakot szerezzük meg
            IntPtr hWnd = WindowNative.GetWindowHandle(window.Handler.PlatformView);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            if (appWindow is not null)
            {
                // 🔹 Visszaállítjuk normál módba, hogy megmaradjon az ablakkeret
                appWindow.SetPresenter(AppWindowPresenterKind.Overlapped);

                // 🔹 Képernyő méretének lekérése
                var displayArea = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Primary);
                if (displayArea != null)
                {
                    var screenBounds = displayArea.WorkArea;

                    // 🔹 Az ablak áthelyezése és átméretezése a teljes képernyőre (de kerettel)
                    appWindow.MoveAndResize(new Windows.Graphics.RectInt32
                    {
                        X = screenBounds.X,
                        Y = screenBounds.Y,
                        Width = screenBounds.Width,
                        Height = screenBounds.Height
                    });
                }
            }
        }




    }
}
