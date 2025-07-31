using CommunityToolkit.Maui.Views;
using SlimWaist.Views;

#if ANDROID
using Android.App;
using Android.Content;
using Android.Content.PM;
using Java.Lang;
#endif
namespace SlimWaist.Popups;

public partial class NeedLoginPopup : Popup
{
	public NeedLoginPopup()
	{
		InitializeComponent();
	}

    private void btnOk_Clicked(object sender, EventArgs e)
    {
#if ANDROID
        Shell.Current.GoToAsync($"//{nameof(LoginPage)}", animate: true);
#endif
        this.Close();
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
}