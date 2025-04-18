using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Storage;
using SlimWaist.Helpers;
using SlimWaist.Models;
using SlimWaist.Popups;
using SlimWaist.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.ViewModels
{
    public partial class ProfileVM(DataContext dataContext, Setting setting) : BaseVM
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
        private bool _isMale;

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
        private BodyActivity? _selectedBodyActivity;

        [ObservableProperty]
        private string? _totalEnergy;

        [ObservableProperty]
        private ObservableCollection<RegimeList> _regimeLists;

        [ObservableProperty]
        private List<BodyActivity> _bodyActivities;

        [ObservableProperty]
        private bool _isShowChangePasswordGrid=false;

        Setting setting;

        public async Task init()
        {
            //Preferences.Set("Email", "");

            //File.Delete(DataContext.DbPath);

            var memberShips = await _dataContext.LoadAsync<Membership>();

            var settings = await _dataContext.LoadAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            MemberShip = memberShips.Where(x => x.Id == setting.SavedMemberShipId).FirstOrDefault();

            Name = MemberShip?.Name ?? "";

            Email = MemberShip?.Email ?? "";

            Weight = MemberShip?.Weight.ToString() ?? "";

            Height = MemberShip?.Height.ToString() ?? "";

            BirthDate = MemberShip?.BirthDate ?? "";

            Gender = MemberShip?.Gender ?? "";

            IsMale=(Gender=="ذكر")? true:false;

            BodyActivity = MemberShip?.BodyActivity ?? "";

            BodyActivities = await _dataContext.LoadAsync<BodyActivity>();

            SelectedBodyActivity = BodyActivities.Where(x => x.BodyActivityName == MemberShip.BodyActivity).FirstOrDefault();

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
        private void BmiCalculator()
        {
            double mi = (Convert.ToDouble(Weight)) / ((Convert.ToDouble(Height) / 100) * (Convert.ToDouble(Height) / 100));

            BMI = Math.Round(mi, 2).ToString();
        }

        private void IdealWeightCalculator()
        {
            if (Gender == "أنثي")
            {
                double iw = ((((Convert.ToDouble(Height)) - 152.4) / 2.5) * 1.7) + 49;
                IdealWeight = Math.Round(iw, 2).ToString();
            }
            if (Gender == "ذكر")
            {
                double iw = ((((Convert.ToDouble(Height)) - 152.4) / 2.5) * 1.9) + 52;
                IdealWeight = Math.Round(iw, 2).ToString();
            }

        }

        private void ModifiedWeightCalculator()
        {
            double mi = (Convert.ToDouble(IdealWeight)) + (0.4 * ((Convert.ToDouble(Weight) - Convert.ToDouble(IdealWeight))));
            ModifiedWeight = Math.Round(mi, 2).ToString();
        }

        private void BodyActivityCalculator()
        {
            double bm = Convert.ToDouble(BMI);
            if (bm <= 18.5)
            {
                BodyActivity = "خامل";
            }
            else if (bm <= 18.5)
            {
                BodyActivity = "قليل النشاط";
            }
            else if (bm <= 18.5)
            {
                BodyActivity = "نشط";
            }
            else if (bm <= 18.5)
            {
                BodyActivity = "نشط جدا";
            }
        }

        private void TotalEnergyCalculator(string bodyActivity)
        {
            double BodyActivityDouble2 = 0;

            if (bodyActivity == "خامل")
            {

                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble2 = 35;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble2 = 30;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble2 = 20;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble2 = 15;
                }

            }
            else if (bodyActivity == "منخفض النشاط")
            {
                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble2 = 40;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble2 = 35;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble2 = 25;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble2 = 20;
                }

            }
            else if (bodyActivity == "نشط")
            {
                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble2 = 45;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble2 = 40;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble2 = 30;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble2 = 25;
                }

            }
            else if (bodyActivity == "نشط جدا")
            {
                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble2 = 50;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble2 = 45;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble2 = 35;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble2 = 30;
                }

            }

            TotalEnergy = Math.Round((Convert.ToDouble(ModifiedWeight) * BodyActivityDouble2), 2).ToString();

        }

        [RelayCommand]
        private async Task GoToDietsPage()
        {
            await GoToAsyncWithStack(nameof(DietsPage), true);
        }

        [RelayCommand]
        private async Task UpdateMemberShip()
        {
            MemberShip.Email = Email ?? "";

            MemberShip.Name = Name;

            MemberShip.Height = Convert.ToDouble(Height);

            MemberShip.Weight = Convert.ToDouble(Weight);

            MemberShip.BirthDate = BirthDate;

            MemberShip.BodyActivity =SelectedBodyActivity.BodyActivityName;

            Gender = (IsMale == true) ? "ذكر" : "أنثي";

            MemberShip.Gender = Gender;

            BmiCalculator();

            IdealWeightCalculator();

            ModifiedWeightCalculator();

            TotalEnergyCalculator(BodyActivity ?? "");

            MemberShip.BMI = BMI;

            MemberShip.IdealWeight = IdealWeight;

            MemberShip.ModifiedWeight = ModifiedWeight;

            MemberShip.TotalEnergy = TotalEnergy;

            await _dataContext.UpdateAsync<Membership>(MemberShip);

            await Toast.Make("تم التحديث", ToastDuration.Short).Show();

        }

        [RelayCommand]
        private async void LogOut()
        {
            setting.SavedMemberShipId = 0;

            await _dataContext.UpdateAsync<Setting>(setting);

            await GoToAsyncWithShell(nameof(LoginPage), true);
        }

        [RelayCommand]
        private async Task GoBack()
        {

        }

    }
}
//[QueryProperty(nameof(Membership), nameof(Membership))]
