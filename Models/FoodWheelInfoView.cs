using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FoodDictionary
{
    public class FoodWheelInfoView
    {
        public List<FoodWheelInfo> StartData { get; set; }
        public List<FoodWheelInfo> VnMData { get; set; }
        public List<FoodWheelInfo> VData { get; set; }
        public List<FoodWheelInfo> MData { get; set; }
        public List<FoodWheelInfo> SDData { get; set; }
        public List<FoodWheelInfo> MNData { get; set; }
        public List<FoodWheelInfo> AFData { get; set; }

        // List to store custom brush colors for the chart
        public List<Brush> CustomBrushes { get; set; }

        public FoodWheelInfoView()
        {
            StartData = new List<FoodWheelInfo>()
            {
                new FoodWheelInfo(){Name = "Vitamins\n&\nMinerals", Region = 25},
                new FoodWheelInfo(){Name = "Specialized\nDiets", Region = 25},
                new FoodWheelInfo(){Name = "Macro\nNutrients", Region = 25},
                new FoodWheelInfo(){Name = "Allergy\nFree", Region = 25}
            };
            VnMData = new List<FoodWheelInfo>()
            {
                new FoodWheelInfo(){Name = "Minerals", Region = 50},
                new FoodWheelInfo(){Name = "Vitamins", Region = 50}
            };
            VData = new List<FoodWheelInfo>()
            {
                new FoodWheelInfo(){Name = "Vitamin A", Region = 17},
                new FoodWheelInfo(){Name = "Vitamin B", Region = 17},
                new FoodWheelInfo(){Name = "Vitamin C", Region = 17},
                new FoodWheelInfo(){Name = "Vitamin D", Region = 17},
                new FoodWheelInfo(){Name = "Vitamin E", Region = 17},
                new FoodWheelInfo(){Name = "Vitamin K", Region = 17}
            };
            MData = new List<FoodWheelInfo>()
            {
                new FoodWheelInfo(){Name = "Calcium", Region = 20},
                new FoodWheelInfo(){Name = "Iron", Region = 20},
                new FoodWheelInfo(){Name = "Magnesium", Region = 20},
                new FoodWheelInfo(){Name = "Potassium", Region = 20},
                new FoodWheelInfo(){Name = "Sodium", Region = 20}
            };
            SDData = new List<FoodWheelInfo>()
            {
                new FoodWheelInfo(){Name = "Vegetarian", Region = 17},
                new FoodWheelInfo(){Name = "Vegan", Region = 17},
                new FoodWheelInfo(){Name = "Keto", Region = 17},
                new FoodWheelInfo(){Name = "Low Carb", Region = 17},
                new FoodWheelInfo(){Name = "Gluten-free", Region = 17},
                new FoodWheelInfo(){Name = "Dairy-free", Region = 17}
            };
            MNData = new List<FoodWheelInfo>()
            {
                new FoodWheelInfo(){Name = "Carbohydrates", Region = 33},
                new FoodWheelInfo(){Name = "Fats", Region = 33},
                new FoodWheelInfo(){Name = "Protein", Region = 33}
            };
            AFData = new List<FoodWheelInfo>()
            {
                new FoodWheelInfo(){Name = "Milk", Region = 10},
                new FoodWheelInfo(){Name = "Eggs", Region = 10},
                new FoodWheelInfo(){Name = "Nuts", Region = 10},
                new FoodWheelInfo(){Name = "Peanuts", Region = 10},
                new FoodWheelInfo(){Name = "Shellfish", Region = 10},
                new FoodWheelInfo(){Name = "Wheat", Region = 10},
                new FoodWheelInfo(){Name = "Soy", Region = 10},
                new FoodWheelInfo(){Name = "Fish", Region = 10},
                new FoodWheelInfo(){Name = "Sesame", Region = 10},
                new FoodWheelInfo(){Name = "Other", Region = 10}
            };

            // Initialize the CustomBrushes list
            CustomBrushes = new List<Brush>();

            // Create and configure blue gradient
            LinearGradientBrush blueGradient = new LinearGradientBrush();
            blueGradient.EndPoint = new Point(0,1);
            blueGradient.GradientStops = new GradientStopCollection()
            {
                new GradientStop() { Offset = 1, Color = Color.FromRgb(0, 113, 188) },
                new GradientStop() { Offset = 0, Color = Color.FromRgb(79, 49, 144) }
            };
            Border blueBorder = new Border();
            blueBorder.Stroke = Color.FromRgb(41, 171, 226);

            // Create and configure red gradient
            LinearGradientBrush redGradient = new LinearGradientBrush();
            redGradient.EndPoint = new Point(0,1);
            redGradient.GradientStops = new GradientStopCollection()
            {
                new GradientStop() { Offset = 1, Color = Color.FromRgb(237, 28, 36) },
                new GradientStop() { Offset = 0, Color = Color.FromRgb(175, 28, 69) }
            };

            Border redBorder = new Border();
            redBorder.Stroke = Color.FromRgb(241, 90, 36);

            // Create and configure yellow gradient
            LinearGradientBrush yellowGradient = new LinearGradientBrush();
            yellowGradient.StartPoint = new Point(0,1);
            yellowGradient.EndPoint = new Point(0,0);
            yellowGradient.GradientStops = new GradientStopCollection()
            {
                new GradientStop() { Offset = 1, Color = Color.FromRgb(251, 176, 59) },
                new GradientStop() { Offset = 0, Color = Color.FromRgb(241, 90, 36) }
            };

            Border yellowBorder = new Border();
            yellowBorder.Stroke = Color.FromRgb(251, 176, 59);

            // Create and configure green gradient
            LinearGradientBrush greenGradient = new LinearGradientBrush();
            greenGradient.StartPoint = new Point(0,1);
            greenGradient.EndPoint = new Point(0,0);
            greenGradient.GradientStops = new GradientStopCollection()
            {
                new GradientStop() { Offset = 1, Color = Color.FromRgb(34, 181, 115) },
                new GradientStop() { Offset = 0, Color = Color.FromRgb(0, 104, 93) }
            };

            Border greenBorder = new Border();
            greenBorder.Stroke = Color.FromRgb(140, 198, 63);

            // Add all gradient brushes to the CustomBrushes list
            CustomBrushes.Add(yellowGradient);
            CustomBrushes.Add(greenGradient);
            CustomBrushes.Add(blueGradient);
            CustomBrushes.Add(redGradient);
        }
    }
}
