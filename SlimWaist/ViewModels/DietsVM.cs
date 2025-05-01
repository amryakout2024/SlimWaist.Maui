using CommunityToolkit.Mvvm.ComponentModel;
using SlimWaist.Models;

namespace SlimWaist.ViewModels
{
    public partial class DietsVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        [ObservableProperty]
        private List<Diet> _diets;

        public async Task init()
        {
            Diets = await _dataContext.LoadAsync<Diet>();

        }
    }
}
