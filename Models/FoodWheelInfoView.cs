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
        public List<FoodWheelInfo> Data { get; set; }

        // List to store custom brush colors for the chart
        public List<Brush> CustomBrushes { get; set; }

        public FoodWheelInfoView()
        {
            Data = new List<FoodWheelInfo>()
        {
            new FoodWheelInfo(){Name = "Vitamins\n&\nMinerals", Region = 25},
            new FoodWheelInfo(){Name = "Specialized\nDiets", Region = 25},
            new FoodWheelInfo(){Name = "Macro\nNutrients", Region = 25},
            new FoodWheelInfo(){Name = "Allergy\nFree", Region = 25},
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
