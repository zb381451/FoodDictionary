﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDictionary.Models
{
    public class Vitamin
    {
			[PrimaryKey, AutoIncrement]
			public int Id { get; set; }

			[NotNull]
			public string Name { get; set; }
		
	}
}
