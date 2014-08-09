using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LGSA.Droid.Fragments
{
	public class ScheduleFragment : ListFragment
	{
		string[] items = new[] { "Jets 10u Tryouts @ 10am", "Jets 12u Tryouts @ noon", "Jets 14u Tryouts @ 2pm" };

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			ListAdapter = new ScheduleAdapter (Activity, items);
			SetHasOptionsMenu (true);
		}

		public static ScheduleFragment NewInstance()
		{
			var scheduleFrag = new ScheduleFragment { Arguments = new Bundle() };
			return scheduleFrag;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);
			return inflater.Inflate(Resource.Layout.schedule, null);
		}

		public override void OnCreateOptionsMenu (IMenu menu, MenuInflater inflater)
		{
			Activity.MenuInflater.Inflate (Resource.Menu.schedule_menu, menu);
			base.OnCreateOptionsMenu (menu, inflater);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Android.Resource.Id.Home:
			case Resource.Id.action_schedule_search:
				break;
			}

			return base.OnOptionsItemSelected (item);
		}
	}
}