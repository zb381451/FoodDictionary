using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDictionary.Models
{
    public class OtherFact
    {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public int FoodId { get; set; }

		public string Fact { get; set; }
	}
}
