using System;
using System.ComponentModel.DataAnnotations;

namespace MultipleEntryFormDemo.Models
{
	public class Bird
	{
		public int BirdId { get; set; }
		[Required]
		public String Name { get; set; } = string.Empty;
		[Required]
		public String Order { get; set; } = string.Empty;
		[Required]
		public int Number { get; set; }
	}
}

