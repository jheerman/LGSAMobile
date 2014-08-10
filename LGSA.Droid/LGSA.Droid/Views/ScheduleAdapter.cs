using System;
using Android.App;
using Android.Views;
using Android.Widget;
using LGSA.Domain;
using System.Collections.Generic;

namespace LGSA.Droid
{
	public class ScheduleAdapter : BaseAdapter
	{
		public ScheduleAdapter ()
		{ }

		private readonly Activity _context;
		private readonly List<CalendarItem> _items;

		public ScheduleAdapter (Activity context, List<CalendarItem> items)
		{
			_context = context;
			_items = items;
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return position;
		}

		public CalendarItem GetAppointment (int position)
		{
			return _items[position];
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override int Count 
		{
			get { return _items.Count; }
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var item = _items[position];

			var view = convertView ??
				_context.LayoutInflater.Inflate(Resource.Layout.schedule_item, null);
				
			var description = view.FindViewById<TextView> (Resource.Id.schedule_description);
			description.Text = item.Description;

			return view;
		}
	}
}

