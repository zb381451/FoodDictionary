using FoodDictionary.Services;

namespace FoodDictionary.Pages
{
    public partial class FoodWheel : ContentPage
    {
        public FoodWheel()
        {
            InitializeComponent();

            // apply theme color
            var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
            if (appState != null)
                this.BackgroundColor = appState.BackgroundColor;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // restore theme color
            var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
            if (appState != null)
                this.BackgroundColor = appState.BackgroundColor;
        }
    }
}
