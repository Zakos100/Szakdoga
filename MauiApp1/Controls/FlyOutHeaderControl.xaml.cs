namespace MauiApp1.Controls;

public partial class FlyOutHeaderControl : StackLayout
{
    public FlyOutHeaderControl()
    {
        InitializeComponent();
        if (App.UserDetails != null)
        {
            lblFullName.Text = App.UserDetails.FullName;
            lblUserRole.Text = App.UserDetails.RoleText;
            lblEmail.Text = App.UserDetails.Username;
        }
    }
}
	
