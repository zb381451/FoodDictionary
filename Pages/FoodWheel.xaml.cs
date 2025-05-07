using FoodDictionary.Services;

namespace FoodDictionary.Pages
{
    public partial class FoodWheel : ContentPage
    {
        public FoodWheelInfoView viewModel = new FoodWheelInfoView();

        public FoodWheel()
        {
            InitializeComponent();

            // apply theme color
            var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
            if (appState != null)
                this.BackgroundColor = appState.BackgroundColor;

            pieChart.ItemsSource = viewModel.StartData;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // restore theme color
            var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
            if (appState != null)
                this.BackgroundColor = appState.BackgroundColor;
        }

        private void DataPointSelectionBehavior_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.Charts.ChartSelectionChangedEventArgs e)
        {
            if (e.NewIndexes.Count != 0)
            {
                if (pieChart.ItemsSource == viewModel.StartData) // For starting regions in pie chart
                {
                    if (e.NewIndexes[e.NewIndexes.Count - 1] == 0)
                    {
                        pieChart.ItemsSource = viewModel.VnMData;
                    }
                    else if (e.NewIndexes[e.NewIndexes.Count - 1] == 1)
                    {
                        pieChart.ItemsSource = viewModel.SDData; // Specialized Diets
                    }
                    else if (e.NewIndexes[e.NewIndexes.Count - 1] == 2)
                    {
                        pieChart.ItemsSource = viewModel.MNData; // Macronutrients
                    }
                    else if (e.NewIndexes[e.NewIndexes.Count - 1] == 3)
                    {
                        pieChart.ItemsSource = viewModel.AFData; // Allergy-free
                    }

                    resetButton.IsEnabled = true;
                    resetButton.IsVisible = true;
                    pieChart.SelectionBehavior.ClearSelection();
                }
                else if (pieChart.ItemsSource == viewModel.VnMData) // Vitamins & Minerals display
                {
                    if (e.NewIndexes[e.NewIndexes.Count - 1] == 0)
                    {
                        pieChart.ItemsSource = viewModel.MData; // Minerals
                    }
                    else if (e.NewIndexes[e.NewIndexes.Count - 1] == 1)
                    {
                        pieChart.ItemsSource = viewModel.VData; // Vitamins
                    }

                    pieChart.SelectionBehavior.ClearSelection();
                }
            }
        }

        private void resetButton_Clicked(object sender, EventArgs e) // Sets the pie chart back to the start
        {
            pieChart.ItemsSource = viewModel.StartData;
            pieChart.SelectionBehavior.ClearSelection();

            resetButton.IsEnabled = false;
            resetButton.IsVisible = false;
        }
    }
}
