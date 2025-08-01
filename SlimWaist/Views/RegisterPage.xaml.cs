using CommunityToolkit.Mvvm.ComponentModel;
using SlimWaist.Models;
using SlimWaist.ViewModels;
using System.Xml.Linq;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class RegisterPage : UraniumContentPage
{
    private readonly RegisterVM _registerVM;

    public RegisterPage(RegisterVM registerVM)
    {
        InitializeComponent();

        _registerVM = registerVM;

        BindingContext = _registerVM;

        datePickerField.MinimumDate = new DateTime(1930, 1, 1);

        datePickerField.MaximumDate = DateTime.Now;

    }
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await _registerVM.init();
    }
}