using SlimWaist.Models;
using SlimWaist.ViewModels;
using System.Runtime.Intrinsics.X86;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class BodyMassAnalysisPage : UraniumContentPage
{
    private readonly BodyMassAnalysisVM _bodyMassAnalysisVM;
    private List<Membership> memberships;
    private Membership membership;
    private Setting setting;
    private List<Setting> settings;

    public BodyMassAnalysisPage(BodyMassAnalysisVM bodyMassAnalysisVM)
	{
		InitializeComponent();

        _bodyMassAnalysisVM = bodyMassAnalysisVM;
        
        BindingContext = _bodyMassAnalysisVM;

    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await _bodyMassAnalysisVM.init();

        bmi1.ScaleX = 1;
        bmi2.ScaleX = 1;
        bmi3.ScaleX = 1;
        bmi4.ScaleX = 1;
        bmi5.ScaleX = 1;

        obesityDegreeNameLabel.Scale = 0;

        await Task.Delay(500);

        double mi = (Convert.ToDouble(HomeVM.CurrentMembership.Weight)) / ((Convert.ToDouble(HomeVM.CurrentMembership.Height) / 100) * (Convert.ToDouble(HomeVM.CurrentMembership.Height) / 100));

        var BMI = Math.Round(mi, 2).ToString();

        if (Convert.ToDouble(BMI) >= 18 && Convert.ToDouble(BMI) <= 24)
        {
            await bmi1.ScaleXTo(1.4, 150);
            obesityDegreeNameLabel.TextColor = Colors.Green;
        }
        else if (Convert.ToDouble(BMI) > 24 && Convert.ToDouble(BMI) <= 29)
        {
            await bmi1.ScaleXTo(1.4, 150);
            await bmi1.ScaleXTo(1);
            await bmi2.ScaleXTo(1.4, 150);
            obesityDegreeNameLabel.TextColor = Colors.Orange;
        }
        else if (Convert.ToDouble(BMI) > 29 && Convert.ToDouble(BMI) <= 34)
        {
            await bmi1.ScaleXTo(1.4, 150);
            await bmi1.ScaleXTo(1);
            await bmi2.ScaleXTo(1.4, 150);
            await bmi2.ScaleXTo(1);
            await bmi3.ScaleXTo(1.4, 150);
            obesityDegreeNameLabel.TextColor = Colors.DarkOrange;
        }
        else if (Convert.ToDouble(BMI) > 34 && Convert.ToDouble(BMI) <= 39)
        {
            await bmi1.ScaleXTo(1.4, 150);
            await bmi1.ScaleXTo(1);
            await bmi2.ScaleXTo(1.4, 150);
            await bmi2.ScaleXTo(1);
            await bmi3.ScaleXTo(1.4, 150);
            await bmi3.ScaleXTo(1);
            await bmi4.ScaleXTo(1.4, 150);
            obesityDegreeNameLabel.TextColor = Colors.OrangeRed;
        }
        else if (Convert.ToDouble(BMI) > 39)
        {
            await bmi1.ScaleXTo(1.4, 150);
            await bmi1.ScaleXTo(1);
            await bmi2.ScaleXTo(1.4, 150);
            await bmi2.ScaleXTo(1);
            await bmi3.ScaleXTo(1.4, 150);
            await bmi3.ScaleXTo(1);
            await bmi4.ScaleXTo(1.4, 150);
            await bmi4.ScaleXTo(1);
            await bmi5.ScaleXTo(1.4, 150);
            obesityDegreeNameLabel.TextColor = Colors.Red;
        }

        await obesityDegreeNameLabel.ScaleTo(1, 250);


    }

    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        Shell.Current.GoToAsync("..", animate: true);
#endif
        return true;
    }

}