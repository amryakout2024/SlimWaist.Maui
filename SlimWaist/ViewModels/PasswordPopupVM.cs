
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.ViewModels
{
    public partial class PasswordPopupVM(DataContext dataContext):BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        private Membership membership;
        private List<Membership> memberships;
        private Setting setting;
        private List<Setting> settings;

        public async Task init()
        {
            memberships = await _dataContext.LoadAsync<Membership>();

            settings = await _dataContext.LoadAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            membership = memberships.Where(x => x.Id == setting.CurrentMemberShipId).FirstOrDefault();

        }

        [RelayCommand]
        private async Task UpdatePassword()
        {

        }
    }
}
