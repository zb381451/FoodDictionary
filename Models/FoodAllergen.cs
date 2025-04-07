using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDictionary.Models
{
    public class FoodAllergen
    {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[Indexed]
		public int FoodId { get; set; }

		[Indexed]
		public int AllergenId { get; set; }
	}
}
