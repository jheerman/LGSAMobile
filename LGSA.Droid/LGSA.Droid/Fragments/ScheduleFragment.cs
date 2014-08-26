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

using LGSA.Data;
using LGSA.Domain;
using System.IO;
using Android.Support.V4.View;

namespace LGSA.Droid.Fragments
{
	public class ScheduleFragment : ListFragment
	{
		List<CalendarItem> items;
		SearchView _searchView;
		ScheduleAdapter _adapter;
		ScheduleRepository schedRepo = new ScheduleRepository();
		ArrayAdapter _spinnerAdapter;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			using (var sr = new StreamReader (Activity.Assets.Open ("calendar.json"))) {
				var json = sr.ReadToEnd ();
				items = schedRepo.GetCalendarItems(json);
			}

			_adapter = new ScheduleAdapter (Activity, items);
			ListAdapter = _adapter;
			SetHasOptionsMenu (true);

			_spinnerAdapter = new ArrayAdapter (Activity, Resource.Array.filter_division);
			Activity.ActionBar.NavigationMode = ActionBarNavigationMode.List;
			Activity.ActionBar.SetListNavigationCallbacks (_spinnerAdapter, new ScreenNavigationListener (Activity, _spinnerAdapter));
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

			var searchOption = menu.FindItem(Resource.Id.action_schedule_search);

			//Handle expand/colapse of action bar
			MenuItemCompat.SetOnActionExpandListener(searchOption, new SearchViewExpandListener(_adapter));

			var searchItem = MenuItemCompat.GetActionView(searchOption);
			_searchView = searchItem.JavaCast<SearchView>();
			_searchView.QueryTextChange += (s, e) => _adapter.Filter.InvokeFilter(e.NewText);

			_searchView.QueryTextSubmit += (s, e) =>
			{
				Toast.MakeText(Activity, "Searched for: " + e.Query, ToastLength.Short).Show();
				e.Handled = true;
			};
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

	public class SearchViewExpandListener 
		: Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
	{
		private readonly IFilterable _adapter;

		public SearchViewExpandListener(IFilterable adapter)
		{
			_adapter = adapter;
		}

		public bool OnMenuItemActionCollapse(IMenuItem item)
		{
			_adapter.Filter.InvokeFilter("");
			return true;
		}

		public bool OnMenuItemActionExpand(IMenuItem item)
		{
			return true;
		}
	}

	public class ScreenNavigationListener : Java.Lang.Object, ActionBar.IOnNavigationListener
	{
		private readonly ArrayAdapter _adapter = null;
		private readonly Activity _currentActivity;

		public ScreenNavigationListener(Activity currentActivity, ArrayAdapter adapter)
		{
			_adapter = adapter;
			_currentActivity = currentActivity;
		}

		public bool OnNavigationItemSelected(int itemPosition, long itemId)
		{
			Toast.MakeText (_currentActivity, "Clicked", ToastLength.Short);
			return true;
		}
	}



}