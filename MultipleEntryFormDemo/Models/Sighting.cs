using System;
namespace MultipleEntryFormDemo.Models
{
	public class Sighting
	{
		private List<Bird> birds = new List<Bird>();
		public int SightingId { get; set; }
		public String Location { get; set; }
		public String Birder { get; set; }
		public DateOnly Date { get; set; }
		public List<Bird> Birds { get { return birds; } }
	}
}

