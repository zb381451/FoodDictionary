using FoodDictionary.Services;

namespace FoodDictionary.Pages
{
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();

            var appState = Application.Current.Handler.MauiContext.Services.GetService<AppState>();
            this.BackgroundColor = appState?.BackgroundColor ?? Colors.White;
            BindingContext = appState;
        }

        private void OnColorSelected(object sender, EventArgs e)
        {
            var appState = BindingContext as AppState;
            string selected = ColorPicker.SelectedItem?.ToString();

            appState.BackgroundColor = selected switch
            {
                "LightGray" => Colors.LightGray,
                "LightBlue" => Colors.LightBlue,
                "LightYellow" => Colors.LightYellow,
                "LightGreen" => Colors.LightGreen,
                _ => Colors.White
            };

            // Just change the background of the page
            this.BackgroundColor = appState.BackgroundColor;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var appState = Application.Current.Handler.MauiContext.Services.GetService<AppState>();
            this.BackgroundColor = appState?.BackgroundColor ?? Colors.White;
        }
    }
}