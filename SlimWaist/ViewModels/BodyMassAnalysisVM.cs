using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.ViewModels
{
    public partial class BodyMassAnalysisVM:BaseVM
    {
        [ObservableProperty]
        private List<BodyActivity> _bodyActivities;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _birthDate;

        [ObservableProperty]
        private int _genderIndex;

        [ObservableProperty]
        private string? _weight;

        [ObservableProperty]
        private string? _height;

        [ObservableProperty]
        private string? _bMI;

        [ObservableProperty]
        private string? _idealWeight;

        [ObservableProperty]
        private string? _targetedWeight;

        [ObservableProperty]
        private string? _modifiedWeight;

        [ObservableProperty]
        private string? _waistCircumferenceMeasurement;

        [ObservableProperty]
        private string? _bodyActivity;

        [ObservableProperty]
        private string? _totalEnergy;

        [ObservableProperty]
        private bool _isBottomSheetPresented;

        private int ObesityDegreeId;

        [ObservableProperty]
        private string? _obesityDegreeName;

        private int WaistCircumferenceId;

        [ObservableProperty]
        private string? _waistCircumferenceName;

        [ObservableProperty]
        private ObservableCollection<RegimeList> _regimeLists;

        Setting setting;

        public async Task init()
        {
            BodyActivities = App.BodyActivities;

            Name = App.currentMembership?.Name ?? "";

            Weight = App.currentMembership?.Weight.ToString() ?? "";

            Height = App.currentMembership?.Height.ToString() ?? "";

            BirthDate = App.currentMembership?.BirthDateDay.ToString() ?? "";

            BodyActivity = BodyActivities.Where(x => x.BodyActivityId == App.currentMembership.BodyActivityId).FirstOrDefault().BodyActivityName;

            WaistCircumferenceMeasurement = App.currentMembership.WaistCircumferenceMeasurement.ToString();

            BmiCalculator();

            IdealWeightCalculator();

            TargetedWeight = IdealWeight;

            ModifiedWeightCalculator();

            //BodyActivityCalculator();

            TotalEnergyCalculator(App.currentMembership.BodyActivityId);

            WaistCircumferenceEvaluationCalculator();

            ObesityDegreeCalculator();

        }

        [RelayCommand]
        private async Task GoProfilePage()
        {
            //await GoToAsyncWithStack(nameof(ProfilePage), true);
        }

        private void BmiCalculator()
        {
            double mi = (Convert.ToDouble(Weight)) / ((Convert.ToDouble(Height) / 100) * (Convert.ToDouble(Height) / 100));

            BMI = Math.Round(mi, 2).ToString();
        }

        private void IdealWeightCalculator()
        {
            if (GenderIndex == 1)
            {
                double iw = ((((Convert.ToDouble(Height)) - 152.4) / 2.5) * 1.7) + 49;
                IdealWeight = Math.Round(iw, 2).ToString();
            }
            if (GenderIndex == 0)
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

        private void TotalEnergyCalculator(int bodyActivityIndex)
        {
            double BodyActivityDouble = 0;

            if (bodyActivityIndex == 0)
            {

                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble = 35;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble = 30;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble = 20;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble = 15;
                }

            }
            else if (bodyActivityIndex == 1)
            {
                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble = 40;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble = 35;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble = 25;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble = 20;
                }

            }
            else if (bodyActivityIndex == 2)
            {
                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble = 45;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble = 40;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble = 30;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble = 25;
                }

            }
            else if (bodyActivityIndex == 3)
            {
                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble = 50;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble = 45;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble = 35;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble = 30;
                }

            }

            TotalEnergy = Math.Round((Convert.ToDouble(ModifiedWeight) * BodyActivityDouble), 2).ToString();

        }

        private void WaistCircumferenceEvaluationCalculator()
        {
            if (App.currentMembership.WaistCircumferenceMeasurement < 94)
                WaistCircumferenceId = 1;
            else if (App.currentMembership.WaistCircumferenceMeasurement >= 94 && App.currentMembership.WaistCircumferenceMeasurement <= 101)
                WaistCircumferenceId = 2;
            else if (App.currentMembership.WaistCircumferenceMeasurement > 101)
                WaistCircumferenceId = 3;

            WaistCircumferenceName = App.waistCircumferences.Where(x => x.WaistCircumferenceId == WaistCircumferenceId).FirstOrDefault().WaistCircumferenceName;
        }

        private void ObesityDegreeCalculator()
        {
            if (Convert.ToDouble(BMI) >= 18 && Convert.ToDouble(BMI) <= 24)
                ObesityDegreeId = 1;
            if (Convert.ToDouble(BMI) > 24 && Convert.ToDouble(BMI) <= 29)
                ObesityDegreeId = 2;
            if (Convert.ToDouble(BMI) > 29 && Convert.ToDouble(BMI) <= 34)
                ObesityDegreeId = 3;
            if (Convert.ToDouble(BMI) > 34 && Convert.ToDouble(BMI) <= 39)
                ObesityDegreeId = 4;
            if (Convert.ToDouble(BMI) > 39)
                ObesityDegreeId = 5;

            ObesityDegreeName = App.obesityDegrees.Where(x => x.ObesityDegreeId == ObesityDegreeId).FirstOrDefault().ObesityDegreeName;
        }

    }
}
