using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Helpers;
using SlimWaist.Models;
using SlimWaist.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel.Communication;

namespace SlimWaist.ViewModels
{
    public partial class RegisterVM(DataContext dataContext) :BaseVM
    {        
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string? _weight;

        [ObservableProperty]
        private string? _height;

        [ObservableProperty]
        private DateTime _birthDate;

        [ObservableProperty]
        private string _gender;

        [ObservableProperty]
        private string? _bodyActivity;

        [ObservableProperty]
        private string? _bMI;

        [ObservableProperty]
        private string? _idealWeight;

        [ObservableProperty]
        private string? _modifiedWeight;

        [ObservableProperty]
        private string? _totalEnergy;

        [ObservableProperty]
        private bool _isPassword;

        [ObservableProperty]
        private bool _isCheckBoxChecked;

        [ObservableProperty]
        private bool _isMale;

        [ObservableProperty]
        private string _pickerSelectedIndex;

        [ObservableProperty]
        private List<BodyActivity> _bodyActivities;

        [ObservableProperty]
        private BodyActivity _selectedBodyActivity;

        private readonly DataContext _dataContext = dataContext;

        public async Task init()
        {
            IsCheckBoxChecked = false;

            IsMale=true;

            IsPassword = true;

            //BirthDate = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            BodyActivities =await _dataContext.LoadAsync<BodyActivity>();

            SelectedBodyActivity = BodyActivities.FirstOrDefault()?? new BodyActivity();

            Email = "amrnewstory@gmail.com";
            Password = "1";
            Name = "عمرو ياقوت";
            Height = "180";
            Weight = "91";
        }

        [RelayCommand]
        private async void SaveNewMemberShip()
        {
            Gender = IsMale ? "ذكر" : "أنثي";

            BodyActivity = SelectedBodyActivity.BodyActivityName ?? "";

            if (!string.IsNullOrEmpty(Email)&&
                !string.IsNullOrEmpty(Password) &&
                !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Weight) &&
                !string.IsNullOrEmpty(Height) &&
                !string.IsNullOrEmpty(BirthDate.ToString()) &&
                !string.IsNullOrEmpty(Gender) &&
                !string.IsNullOrEmpty(BodyActivity) )
            {
                var IsRegisteredEmailBefore = _dataContext.FindEmailAsync(Email);

                if (!IsRegisteredEmailBefore.Result)
                {
                    BmiCalculator();

                    IdealWeightCalculator();

                    ModifiedWeightCalculator();

                    TotalEnergyCalculator(BodyActivity ?? "");

                    await _dataContext.InsertAsync(new Membership()
                    {
                        Email = Email,
                        Password = Password,
                        Name = Name,
                        Weight = Convert.ToDouble(Weight),
                        WeightDate = DateTime.Now.ToString("dd/MM/yyyy"),
                        Height = Convert.ToDouble(Height),
                        BirthDate = DateTime.Now.ToString("dd/MM/yyyy"),
                        Gender = Gender,
                        BodyActivity = BodyActivity,
                        BMI = BMI,
                        IdealWeight = IdealWeight,
                        ModifiedWeight = ModifiedWeight,
                        TotalEnergy = TotalEnergy

                    });

                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                    await Toast.Make("تم تسجيل العضوية بنجاح", ToastDuration.Short).Show(cancellationTokenSource.Token);

                    await GoToAsyncWithShell(nameof(LoginPage),true);

                }
                else
                {
                    await Shell.Current.DisplayAlert("خطأ", "الايميل مسجل مسبقا", "Ok");
                }

            }



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
            else if (bodyActivity == "قليل النشاط")
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



    }
}
