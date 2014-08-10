using System;
using System.Collections.Generic;

namespace LGSA.Domain
{
	public class CalendarItem
	{
		public CalendarItem ()
		{ }

		public string Id { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public List<string> Tags { get; set; }
	}
}

