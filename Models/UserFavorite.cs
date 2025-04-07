using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDictionary.Models
{
    class UserFavorite
    {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[Indexed]
		public int UserId { get; set; }

		[Indexed]
		public int FoodId { get; set; }

		public string Category { get; set; }
	}
}
