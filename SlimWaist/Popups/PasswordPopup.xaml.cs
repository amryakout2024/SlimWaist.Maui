using CommunityToolkit.Maui.Views;
using SlimWaist.Models;

namespace SlimWaist.Popups;

public partial class PasswordPopup : Popup
{
    private readonly DataContext _dataContext;

    public PasswordPopup(DataContext dataContext)
	{
		InitializeComponent();

        _dataContext = dataContext;
    }

    private void UpdatePassword_Clicked(object sender, EventArgs e)
    {

    }
}