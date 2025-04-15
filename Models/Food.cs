using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDictionary.Models
{
    public class Food
    {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[NotNull]
		public string Name { get; set; }

		public string Image{ get; set; }

		public string Serving_Size { get; set; }

		public decimal? Price { get; set; }
	}
}
