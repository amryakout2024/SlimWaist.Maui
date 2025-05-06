using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class RegimesListVM() : BaseVM
    {

        [ObservableProperty]
        private Membership? _memberShip;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private bool _isShowNewRegimeForm = false;

        [ObservableProperty]
        private DateTime _dateStart;

        [ObservableProperty]
        private DateTime _dateEnd;

        [ObservableProperty]
        private List<Regime> _regimes;

        [ObservableProperty]
        private ObservableCollection<RegimeList> _regimeLists;

        [ObservableProperty]
        private string? _weight;

        [ObservableProperty]
        private Regime _selectedRegime;

        public async Task Init()
        {
            var MemberShips = await App.dataContext.LoadAsync<Membership>();

            DateStart = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateEnd = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            Regimes = await App.dataContext.LoadAsync<Regime>();

            SelectedRegime = Regimes.FirstOrDefault() ?? new Regime();

            Email = Preferences.Get("Email", "empty");

            MemberShip = MemberShips.Where(x => x.Email == Email).FirstOrDefault();

            RegimeLists = new ObservableCollection<RegimeList>();

            List<RegimeList> rl = await App.dataContext.LoadAsync<RegimeList>();

            var rlf = rl.Where(x => x.MembershipId == MemberShip.Id);

            foreach (var r in rlf)
            {
                RegimeLists.Add(r);
            }
        }

        [RelayCommand]
        private async Task GoToHomePage()
        {
            await GoToAsyncWithStack(nameof(HomePage), true);
        }

        [RelayCommand]
        private async Task NavigateSlimWaistDaysList()
        {
            await Shell.Current.GoToAsync($"//{nameof(SlimWaistDaysListPage)}", animate: true);
        }


        [RelayCommand]
        private async Task ShowNewRegimeForm()
        {
            Weight = "0";

            DateStart = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateEnd = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            IsShowNewRegimeForm = true;
        }

        [RelayCommand]
        private async Task HideNewRegimeForm()
        {
            IsShowNewRegimeForm = false;
        }

        [RelayCommand]
        private async Task SaveNewRegime()
        {
            if (SelectedRegime == null)
            {
                await Shell.Current.DisplayAlert("خطأ", "يرجي اختيار نظام الدايت", "Ok");
            }
            else if (Weight == "0")
            {
                await Shell.Current.DisplayAlert("خطأ", "يرجي ادخال الوزن", "Ok");
            }
            else
            {
                await App.dataContext.InsertAsync<RegimeList>(new RegimeList()
                {
                    MembershipId = MemberShip.Id,
                    RegimeName = SelectedRegime.RegimeName,
                    Datestart = DateStart.ToString(),
                    DateEnd = DateEnd.ToString(),
                    Weight = Weight,
                });

                IsShowNewRegimeForm = false;

                List<RegimeList> rl = await App.dataContext.LoadAsync<RegimeList>();

                var rlf = rl.Where(x => x.MembershipId == MemberShip.Id);

                RegimeLists.Clear();

                foreach (var r in rlf)
                {
                    RegimeLists.Add(r);
                }

            }


        }

    }
}
