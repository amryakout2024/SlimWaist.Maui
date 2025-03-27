using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Helpers;
using SlimWaist.Models;
using SlimWaist.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel.Communication;

namespace SlimWaist.ViewModels
{
    public partial class HomeVM(DataContext dataContext,Setting setting):BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly Setting _setting = setting;
        [ObservableProperty]
        private Membership? _memberShip;

        [ObservableProperty]
        private Membership? _memberShipFromQueryProperty;
        
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _birthDate;

        [ObservableProperty]
        private string? _gender;

        [ObservableProperty]
        private string? _weight;

        [ObservableProperty]
        private string? _height;

        [ObservableProperty]
        private string? _bMI;

        [ObservableProperty]
        private string? _idealWeight;

        [ObservableProperty]
        private string? _modifiedWeight;

        [ObservableProperty]
        private string? _bodyActivity;

        [ObservableProperty]
        private string? _totalEnergy;

        [ObservableProperty]
        private ObservableCollection<RegimeList> _regimeLists;

        Setting setting;

        public async Task init()
        {
            //Preferences.Set("Email", "");

            //File.Delete(DataContext.DbPath);

            var memberShips =await _dataContext.LoadAsync<Membership>();

            var settings = await _dataContext.LoadAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            MemberShip = memberShips.Where(x=>x.Id== setting.CurrentMemberShipId).FirstOrDefault();

            Name = MemberShip?.Name ?? "";
           
            Weight = MemberShip?.Weight.ToString() ?? "";
           
            Height = MemberShip?.Height.ToString() ?? "";

            BirthDate = MemberShip?.BirthDate ?? "";

            Gender = MemberShip?.Gender ?? "";

            BodyActivity = MemberShip?.BodyActivity ?? "";

            BMI = MemberShip?.BMI ?? "";

            IdealWeight = MemberShip?.IdealWeight ?? "";

            ModifiedWeight = MemberShip?.ModifiedWeight ?? "";

            TotalEnergy = MemberShip?.TotalEnergy ?? "";

            RegimeLists = null;
            //List<RegimeList> AllRegimeLists = await _dataContext.LoadAsync<RegimeList>();

            //var MaxRegimeId = AllRegimeLists.Where(x => x.MembershipId == MemberShip.Id).Select(x=>x.RegimeId).Max();

            //var FilteredRegimeList = AllRegimeLists.Where(x => x.MembershipId == MemberShip.Id).Where(x => x.RegimeId == MaxRegimeId);

            //foreach (var r in FilteredRegimeList)
            //{
            //    RegimeLists.Add(r);
            //}
            
        }

        [RelayCommand]
        private async Task GoToDietsPage()
        {
            await GoToAsyncWithStack(nameof(DietsPage),true);
        }

        [RelayCommand]
        private async void GoToSettingPage()
        {
            await GoToAsyncWithStack(nameof(SettingPage), true);
        }

        [RelayCommand]
        private async Task GoBack()
        {

        }

    }
}
//[QueryProperty(nameof(Membership), nameof(Membership))]
