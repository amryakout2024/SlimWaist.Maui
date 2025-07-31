
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;

namespace SlimWaist.ViewModels
{
    public partial class PasswordPopupVM() : BaseVM
    {

        private Membership membership;
        private List<Membership> memberships;
        private Setting setting;
        private List<Setting> settings;

        public async Task init()
        {
            memberships = await App._dataContext.LoadAsync<Membership>();

            settings = await App._dataContext.LoadAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            membership = memberships.Where(x => x.Id == setting.SavedMembershipId).FirstOrDefault();

        }

        [RelayCommand]
        private async Task UpdatePassword()
        {

        }
    }
}
