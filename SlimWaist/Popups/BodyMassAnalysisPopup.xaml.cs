using CommunityToolkit.Maui.Views;
using SlimWaist.Extentions;
using SlimWaist.Models;
#if ANDROID
using Android.App;
using Android.Content;
using Android.Content.PM;
using Java.Lang;
#endif

namespace SlimWaist.Popups;

public partial class BodyMassAnalysisPopup : Popup
{
    private List<Membership> memberships;
    private Membership membership;
    private Setting setting;
    private List<Setting> settings;


    public BodyMassAnalysisPopup()
	{
		InitializeComponent();

        Dispatcher.DispatchAsync(async () =>
        {
            memberships = await App.dataContext.LoadAsync<Membership>();

            settings = await App.dataContext.LoadAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            membership = memberships.Where(x => x.Id == setting.SavedMembershipId).FirstOrDefault();

            if (membership.CultureInfo == "ar-SA")
            {
            }
            else
            {
            }


            bmi1.ScaleX = 1;
            bmi2.ScaleX = 1;
            bmi3.ScaleX = 1;
            bmi4.ScaleX = 1;
            bmi5.ScaleX = 1;

            obesityDegreeNameLabel.Scale = 0;

            await Task.Delay(500);

            obesityDegreeNameLabel.Text = App.obesityDegrees.Where(x => x.ObesityDegreeId == 4).FirstOrDefault().ObesityDegreeName;

            double mi = (Convert.ToDouble(App.currentMembership.Weight)) / ((Convert.ToDouble(App.currentMembership.Height) / 100) * (Convert.ToDouble(App.currentMembership.Height) / 100));

            var BMI = System.Math.Round(mi, 2).ToString();

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


        });

    }
}