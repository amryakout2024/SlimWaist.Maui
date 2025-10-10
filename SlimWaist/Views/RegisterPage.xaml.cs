using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Dispatching;
using SlimWaist.Models;
using SlimWaist.ViewModels;
using System.Xml.Linq;
using UraniumUI.Material.Controls;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class RegisterPage : UraniumContentPage
{
    private readonly RegisterVM _registerVM;
    IDispatcherTimer timer;

    public RegisterPage(RegisterVM registerVM)
    {
        InitializeComponent();

        _registerVM = registerVM;

        BindingContext = _registerVM;

        datePickerField.MinimumDate = new DateTime(1930, 1, 1);

        datePickerField.MaximumDate = DateTime.Now;
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(1000);
        timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (lblMinutes.Text=="2" && lblSeconds.Text=="0")
        {
            lblMinutes.Text = "1";
            lblSeconds.Text = "60";
        }
        if (lblMinutes.Text=="1" && lblSeconds.Text=="0")
        {
            lblMinutes.Text = "0";
            lblSeconds.Text = "60";
        }
        if (lblMinutes.Text=="0" && lblSeconds.Text=="0")
        {
            timer.Stop();
        }

        if (Convert.ToDouble(lblMinutes.Text) == 1 && Convert.ToDouble(lblSeconds.Text)>0)
        {
            lblSeconds.Text = (Convert.ToDouble(lblSeconds.Text) - 1).ToString();
        }
        if (Convert.ToDouble(lblMinutes.Text)==0 && Convert.ToDouble(lblSeconds.Text)>0)
        {
            lblSeconds.Text = (Convert.ToDouble(lblSeconds.Text) - 1).ToString();
        }

    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await _registerVM.init();
    }

    private void txt_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextField txt = (TextField)sender;

        switch (txt.AutomationId)
        {
            case "1":
                {
                    if (txt.Text.Length==1)
                    {
                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly =false;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=true;

                        txt2.Focus();
                    }
                    else if(txt.Text.Length==2)
                    {

                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=false;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=true;

                        txt2.Text = txt.Text.Substring(1,1);
                        txt2.Focus();
                        txt1.Text = txt.Text.Substring(0, 1);
                    }
                    break;
                }
            case "2":
                {
                    if (txt.Text.Length==1)
                    {
                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=false;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=true;

                        txt3.Focus();
                    }
                    else if(txt.Text.Length == 0)
                    {
                        txt1.IsReadOnly =false;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=true;

                        txt1.Focus();
                    }
                    if (txt.Text.Length == 2)
                    {
                        txt1.IsReadOnly=true; 
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=false;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=true;
                        txt3.Text = txt.Text.Substring(1, 1);
                        txt2.Text = txt.Text.Substring(0, 1);
                        txt3.Focus();
                    }

                    break;
                }
            case "3":
                {
                    if (txt.Text.Length==1)
                    {
                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=false;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=true;

                        txt4.Focus();
                    }
                    else if(txt.Text.Length == 0)
                    {
                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=false;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=true;

                        txt2.Focus();
                    }
                    if (txt.Text.Length == 2)
                    {

                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=false;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=true;
                        txt4.Text = txt.Text.Substring(1, 1);
                        txt3.Text = txt.Text.Substring(0, 1);

                        txt4.Focus();
                    }

                    break;
                }
            case "4":
                {
                    if (txt.Text.Length==1)
                    {
                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=false;
                        txt6.IsReadOnly=true;

                        txt5.Focus();
                    }
                    else if(txt.Text.Length == 0)
                    {
                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=false;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=true;

                        txt3.Focus();
                    }
                    if (txt.Text.Length == 2)
                    {

                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=false;
                        txt6.IsReadOnly=true;
                        txt5.Text = txt.Text.Substring(1, 1);
                        txt4.Text = txt.Text.Substring(0, 1);

                        txt5.Focus();
                    }

                    break;
                }
            case "5":
                {
                    if (txt.Text.Length==1)
                    {
                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=false;

                        txt6.Focus();
                    }
                    else if(txt.Text.Length == 0)
                    {
                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=false;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=true;

                        txt4.Focus();
                    }
                    if (txt.Text.Length == 2)
                    {

                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=true;
                        txt6.IsReadOnly=false;
                        txt6.Text = txt.Text.Substring(1, 1);
                        txt5.Text = txt.Text.Substring(0, 1);
                        txt6.Focus();
                    }

                    break;
                }
            case "6":
                {
                    if (txt.Text.Length==0)
                    {
                        txt1.IsReadOnly=true;
                        txt2.IsReadOnly=true;
                        txt3.IsReadOnly=true;
                        txt4.IsReadOnly=true;
                        txt5.IsReadOnly=false;
                        txt6.IsReadOnly=true;

                        txt5.Focus();
                    }
                    else
                    {
                        txt6.Text = txt.Text.Substring(0, 1);
                    }
                    break;
                }
            default:break;
        }
    }

    private void btnShowBottomsheet_Clicked(object sender, EventArgs e)
    {
        txt1.IsReadOnly=false;
        txt2.IsReadOnly=true;
        txt3.IsReadOnly=true;
        txt4.IsReadOnly=true;
        txt5.IsReadOnly=true;
        txt6.IsReadOnly=true;

        txt1.Focus();

        timer.Start();
    }
}