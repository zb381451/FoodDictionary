using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDictionary.Models
{
    public class FoodDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Serving_Size { get; set; }
        public decimal? Price { get; set; }
        public string Vitamins { get; set; }
        public string Minerals { get; set; }
        public string Allergens { get; set; }
        public string Ingredients { get; set; }
        public string OtherFacts { get; set; }
    }
}
