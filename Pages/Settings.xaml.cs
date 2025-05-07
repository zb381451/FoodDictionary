using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using FoodDictionary.Services;
using FoodDictionary.Models;

namespace FoodDictionary.Pages;

public partial class Settings : ContentPage
{
    private UserStats user;

    public Settings()
    {
        InitializeComponent();

        user = new UserStats();

        // apply saved background color
        var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
        if (appState != null)
        {
            this.BackgroundColor = appState.BackgroundColor;
            BindingContext = appState;
        }

        user.heightFeet = 0;
        feetNumLabel.Text = "0\'";
        user.heightInches = 0;
        inchesNumLabel.Text = "0\"";
    }

    private void OnColorSelected(object sender, EventArgs e)
    {
        var appState = BindingContext as AppState;
        string? selected = ColorPicker.SelectedItem?.ToString();

        if (appState == null || string.IsNullOrEmpty(selected))
            return;

        appState.BackgroundColor = selected switch
        {
            "LightGray" => Colors.LightGray,
            "LightBlue" => Colors.LightBlue,
            "LightYellow" => Colors.LightYellow,
            "LightGreen" => Colors.LightGreen,
            _ => Colors.White
        };

        // update background after selection
        this.BackgroundColor = appState.BackgroundColor;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // re-apply saved background
        var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
        if (appState != null)
        {
            this.BackgroundColor = appState.BackgroundColor;
        }
    }

    private void OnAgeChanged(object sender, TextChangedEventArgs e)
    {
        user.age = int.Parse(ageEntry.Text);
        if (user.age > 150) // Max age of 150
        {
            user.age = 150;
            ageEntry.Text = "150";
        }

        BMRLabel.Text = user.calculateBMR().ToString() + " calories/day";
    }

    private void OnSexSelected(object sender, EventArgs e)
    {
        user.sex = (Sex)sexPicker.SelectedIndex;

        BMRLabel.Text = user.calculateBMR().ToString() + " calories/day";
    }

    private void OnAddFootClicked(object sender, EventArgs e)
    {
        user.heightFeet++;
        if (user.heightFeet > 10) // Max height in feet of 10
            user.heightFeet = 10;

        feetNumLabel.Text = user.heightFeet.ToString() + "\'";

        BMRLabel.Text = user.calculateBMR().ToString() + " calories/day";
    }

    private void OnRemoveFootClicked(object sender, EventArgs e)
    {
        user.heightFeet--;
        if (user.heightFeet < 0) // Min eight in feet of 0
            user.heightFeet = 0;

        feetNumLabel.Text = user.heightFeet.ToString() + "\'";

        BMRLabel.Text = user.calculateBMR().ToString() + " calories/day";
    }

    private void OnAddInchClicked(object sender, EventArgs e)
    {
        user.heightInches++;
        if (user.heightInches > 12) // Max height in inches of 12
            user.heightInches = 12;

        inchesNumLabel.Text = user.heightInches.ToString() + "\"";

        BMRLabel.Text = user.calculateBMR().ToString() + " calories/day";
    }

    private void OnRemoveInchClicked(object sender, EventArgs e)
    {
        user.heightInches--;
        if (user.heightInches < 0) // Min height in inches of 0
            user.heightInches = 0;

        inchesNumLabel.Text = user.heightInches.ToString() + "\"";

        BMRLabel.Text = user.calculateBMR().ToString() + " calories/day";
    }

    private void OnWeightChanged(object sender, TextChangedEventArgs e)
    {
        user.weight = int.Parse(weightEntry.Text);
        if (user.weight > 1000) // Max weight of 1000
        {
            user.weight = 1000;
            weightEntry.Text = "1000";
        }

        BMRLabel.Text = user.calculateBMR().ToString() + " calories/day";
    }

    private void OnActivitySelected(object sender, EventArgs e)
    {
        user.activity = (Activity)activityPicker.SelectedIndex;

        BMRLabel.Text = user.calculateBMR().ToString() + " calories/day";
    }
}
