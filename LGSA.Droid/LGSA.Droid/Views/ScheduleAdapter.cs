using Android.App;
using Android.Views;
using Android.Widget;
using LGSA.Domain;
using System.Collections.Generic;
using Java.Lang;
using System.Linq;

namespace LGSA.Droid
{
	public class ScheduleAdapter : BaseAdapter, IFilterable
	{
		public ScheduleAdapter ()
		{ }

		readonly Activity _context;
		List<CalendarItem> _originalData;
		List<CalendarItem> _items;

		public ScheduleAdapter (Activity context, List<CalendarItem> items)
		{
			_context = context;
			_items = items;
			Filter = new ScheduleFilter (this);
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
			var month = view.FindViewById<TextView> (Resource.Id.schedule_month);
			var day = view.FindViewById<TextView> (Resource.Id.schedule_date);
			var dow = view.FindViewById<TextView> (Resource.Id.schedule_weekday);

			dow.Text = item.Date.ToString ("ddd");
			day.Text = item.Date.Day.ToString();
			month.Text = item.Date.ToString("MMM");
			description.Text = item.Description;

			return view;
		}

		public Filter Filter { get; private set; }

		class ScheduleFilter : Filter
		{
			readonly ScheduleAdapter _adapter;
			public ScheduleFilter(ScheduleAdapter adapter)
			{
				_adapter = adapter;
			}

			protected override FilterResults PerformFiltering(ICharSequence constraint)
			{
				var returnObj = new FilterResults();
				var results = new List<CalendarItem>();
				_adapter._originalData = _adapter._originalData ?? _adapter._items;

				if (constraint == null) return returnObj;

				if (_adapter._originalData != null && _adapter._originalData.Any())
				{
					// Compare constraint to all names lowercased. 
					// It they are contained they are added to results.
					results.AddRange(
						_adapter._originalData.Where(
							item => item.Description.ToLower().Contains(constraint.ToString())));
				}

				// Nasty piece of .NET to Java wrapping, be careful with this!
				returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
				returnObj.Count = results.Count;

				constraint.Dispose();

				return returnObj;
			}

			protected override void PublishResults(ICharSequence constraint, FilterResults results)
			{
				using (var values = results.Values)
					_adapter._items = values.ToArray<Java.Lang.Object>()
						.Select(r => r.ToNetObject<CalendarItem>()).ToList();

				_adapter.NotifyDataSetChanged();

				// Don't do this and see GREF counts rising
				constraint.Dispose();
				results.Dispose();
			}
		}
	}
}

