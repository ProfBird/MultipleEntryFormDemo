using System;
using System.ComponentModel.DataAnnotations;

namespace MultipleEntryFormDemo.Models
{
	public class Sighting
	{
		private List<Bird> birds = new List<Bird>();
		public int SightingId { get; set; }
		[Required]
		public String Location { get; set; } = string.Empty;
		[Required]
		public String Birder { get; set; } = string.Empty;
		public DateOnly Date { get; set; }
		public List<Bird> Birds { get { return birds; } }
	}
}

