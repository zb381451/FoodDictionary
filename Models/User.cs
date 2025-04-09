﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDictionary.Models
{
    public class User
    {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[Unique, NotNull]
		public string Username { get; set; }

		[Unique, NotNull]
		public string Email { get; set; }

		[NotNull]
		public string Password { get; set; }
	}
}
