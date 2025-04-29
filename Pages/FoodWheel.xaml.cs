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
                if (pieChart.ItemsSource == viewModel.StartData)
                {
                    if (e.NewIndexes[e.NewIndexes.Count - 1] == 0)
                    {
                        pieChart.ItemsSource = viewModel.VnMData;
                    }
                    else if (e.NewIndexes[e.NewIndexes.Count - 1] == 1)
                    {
                        pieChart.ItemsSource = viewModel.SDData;
                    }
                    else if (e.NewIndexes[e.NewIndexes.Count - 1] == 2)
                    {
                        pieChart.ItemsSource = viewModel.MNData;
                    }
                    else if (e.NewIndexes[e.NewIndexes.Count - 1] == 3)
                    {
                        pieChart.ItemsSource = viewModel.AFData;
                    }

                    resetButton.IsEnabled = true;
                    resetButton.IsVisible = true;
                    pieChart.SelectionBehavior.ClearSelection();
                }
                else if (pieChart.ItemsSource == viewModel.VnMData)
                {
                    if (e.NewIndexes[e.NewIndexes.Count - 1] == 0)
                    {
                        pieChart.ItemsSource = viewModel.MData;
                    }
                    else if (e.NewIndexes[e.NewIndexes.Count - 1] == 1)
                    {
                        pieChart.ItemsSource = viewModel.VData;
                    }

                    pieChart.SelectionBehavior.ClearSelection();
                }
            }
        }

        private void resetButton_Clicked(object sender, EventArgs e)
        {
            pieChart.ItemsSource = viewModel.StartData;
            pieChart.SelectionBehavior.ClearSelection();

            resetButton.IsEnabled = false;
            resetButton.IsVisible = false;
        }
    }
}
