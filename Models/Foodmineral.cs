using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDictionary.Models
{
    public class Foodmineral
    {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[Indexed]
		public int FoodId { get; set; }

		[Indexed]
		public int MineralId { get; set; }
	}
}
