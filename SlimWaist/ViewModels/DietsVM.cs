using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models.Dto;

namespace SlimWaist.ViewModels
{
    public partial class DietsVM() : BaseVM
    {
        [ObservableProperty]
        private List<Diet> _diets;

        [ObservableProperty]
        private bool _isKetoChecked;

        [ObservableProperty]
        private bool _isLowCarbChecked;

        [ObservableProperty]
        private bool _isServingSizeChecked;

        public async Task init()
        {
            Diets = await App._dataContext.GetAsync<Diet>();
        }

        [RelayCommand]
        private void SaveDiet()
        {

        }
        partial void OnIsKetoCheckedChanged(bool value)
        {
            if (IsKetoChecked)
            {
                IsLowCarbChecked = false;
                IsServingSizeChecked = false;
            }
        }
        partial void OnIsLowCarbCheckedChanged(bool value)
        {
            if (IsLowCarbChecked)
            {
                IsKetoChecked = false;
                IsServingSizeChecked = false;
            }
        }
        partial void OnIsServingSizeCheckedChanged(bool value)
        {
            if (IsServingSizeChecked)
            {
                IsKetoChecked = false;
                IsLowCarbChecked = false;
            }
        }
    }
}
