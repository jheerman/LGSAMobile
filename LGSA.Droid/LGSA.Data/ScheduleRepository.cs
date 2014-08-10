using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json.Serialization;
using LGSA.Domain;

namespace LGSA.Data
{
	public class ScheduleRepository
	{
		public ScheduleRepository ()
		{ }

		public List<CalendarItem> GetCalendarItems(string json)
		{
			var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CalendarItem>> (json);
			return items ?? new List<CalendarItem> ();
		}

	}
}

