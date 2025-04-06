using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;

public static class AppWindowExtensions
{
    public static void SetFullScreen(this Window xamlWindow)
    {
        // Először le kell kérnünk a Win32 ablak-handle-t
        nint hWnd = WindowNative.GetWindowHandle(xamlWindow);

        // Ezután ebből az ablak-handle-ből kérjük le a WindowId-t
        WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);

        // Végül létrehozzuk az AppWindow példányt, és beállítjuk teljes képernyős módra
        AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
        appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
    }
}
