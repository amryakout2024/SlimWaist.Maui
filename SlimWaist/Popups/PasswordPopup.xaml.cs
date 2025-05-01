using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using SlimWaist.Models;

namespace SlimWaist.Popups;

public partial class PasswordPopup : Popup
{
    private readonly DataContext _dataContext;
    private Membership membership;
    private List<Membership> memberships;
    private Setting setting;
    private List<Setting> settings;

    public PasswordPopup(DataContext dataContext)
    {
        InitializeComponent();

        _dataContext = dataContext;

        Dispatcher.DispatchAsync(async () =>
        {
            memberships = await _dataContext.LoadAsync<Membership>();

            settings = await _dataContext.LoadAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            membership = memberships.Where(x => x.Id == setting.CurrentMemberShipId).FirstOrDefault();
        });
    }

    private async void UpdatePassword_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(PasswordOld.Text) &&
            !string.IsNullOrEmpty(PasswordNew1.Text) &&
            !string.IsNullOrEmpty(PasswordNew2.Text))
        {
            if (PasswordNew1.Text == PasswordNew2.Text)
            {
                if (membership.Password == PasswordOld.Text)
                {
                    membership.Password = PasswordNew1.Text;

                    await _dataContext.UpdateAsync<Membership>(membership);

                    await Toast.Make("تم التحديث", ToastDuration.Short).Show();

                    this.Close();
                }
                else
                {
                    await Shell.Current.DisplayAlert("", "كلمة المرور القديمة غير صحيحة", "ok");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("", "كلمة المرور الجديدة غير متطابقة", "ok");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("", "برجاء مليء جميع الحقول", "ok");
        }
    }
}