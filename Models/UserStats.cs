using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDictionary.Models
{
    enum Sex { MALE, FEMALE }
    enum Activity { SEDENTARY, LIGHT, MODERATE, ACTIVE, VERY_ACTIVE, EXTRA_ACTIVE }
    class UserStats
    {
        public int age { get; set; }
        public Sex sex { get; set; }
        public int heightFeet { get; set; }
        public int heightInches { get; set; }
        public double weight { get; set; }
        public Activity activity { get; set; }
        public double BMR { get; set; }

        public double calculateBMR() // Calculated through the Mifflin-St Jeor equation
        {
            if (sex == Sex.MALE)
            {
                BMR = (10 * (weight * 0.4535924)) + (6.25 * ((heightFeet * 30.48) + (heightInches * 2.54))) - (5 * age) + 5; // 0.4535924 to convert ibs to kg, 30.48 to convert ft to cm, 2.54 to convert inches to cm
            }
            else if (sex == Sex.FEMALE)
            {
                BMR = (10 * (weight * 0.4535924)) + (6.25 * ((heightFeet * 30.48) + (heightInches * 2.54))) - (5 * age) - 161; // 30.48 to convert ft to cm, 2.54 to convert inches to cm
            }

            return BMR;
        }
    }
}
