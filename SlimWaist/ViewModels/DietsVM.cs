using CommunityToolkit.Mvvm.ComponentModel;
using SlimWaist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.ViewModels
{
    public partial class DietsVM(DataContext dataContext) :BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        [ObservableProperty]
        private List<Diet> _diets;

        public async Task init()
        {
            Diets=await _dataContext.LoadAsync<Diet>();

        }
    }
}
