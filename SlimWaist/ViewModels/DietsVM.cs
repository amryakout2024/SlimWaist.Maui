using CommunityToolkit.Mvvm.ComponentModel;
using SlimWaist.Models;

namespace SlimWaist.ViewModels
{
    public partial class DietsVM() : BaseVM
    {
        [ObservableProperty]
        private List<Diet> _diets;

        public async Task init()
        {
            Diets = await App.dataContext.LoadAsync<Diet>();

        }
    }
}
