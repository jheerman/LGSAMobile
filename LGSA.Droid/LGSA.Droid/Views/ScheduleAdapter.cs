using System;
using Android.App;
using Android.Views;
using Android.Widget;

namespace LGSA.Droid
{
	public class ScheduleAdapter : BaseAdapter
	{
		public ScheduleAdapter ()
		{ }

		private readonly Activity _context;
		private readonly string[] _items;

		public ScheduleAdapter (Activity context, string[] items)
		{
			_context = context;
			_items = items;
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return position;
		}

		public string GetNewsEvent (int position)
		{
			return _items[position];
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override int Count 
		{
			get { return _items.Length; }
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var item = _items[position];

			var view = convertView ??
				_context.LayoutInflater.Inflate(Resource.Layout.schedule_item, null);
				
			var description = view.FindViewById<TextView> (Resource.Id.schedule_description);
			description.Text = item;

			return view;
		}
	}
}

