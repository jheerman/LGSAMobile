using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using Android.Support.V4.Widget;

using LGSA.Droid.Helpers;
using LGSA.Droid.Fragments;

namespace LGSA.Droid
{
	[Activity (Label = "LGSA", Theme="@style/LGSA.Purple", Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private DrawerToggle drawerToggle;
		private string drawerTitle;
		private string currentSectionTitle;

		private DrawerLayout drawerLayout;
		private ListView drawerListView;
		private string[] sections;
		private int lastSelectedSection = -1;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.main);

			sections = Resources.GetTextArray(Resource.Array.drawer_sections);

			currentSectionTitle = drawerTitle = Title;

			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			drawerListView = FindViewById<ListView>(Resource.Id.left_drawer);

			drawerListView.Adapter = new ArrayAdapter<string>(this, Resource.Layout.item_menu, sections);

			drawerListView.ItemClick += (sender, args) => ListItemClicked(args.Position);

			drawerLayout.SetDrawerShadow(Resource.Drawable.drawer_shadow_light, (int)GravityFlags.Start);

			//DrawerToggle is the animation that happens with the indicator next to the actionbar
			drawerToggle = new DrawerToggle(this, drawerLayout,
				Resource.Drawable.ic_navigation_drawer_dark,
				Resource.String.drawer_open,
				Resource.String.drawer_close);

			//Display the current fragments title and update the options menu
			drawerToggle.DrawerClosed += (o, args) =>
			{
				ActionBar.Title = currentSectionTitle;
				InvalidateOptionsMenu();
			};

			//Display the drawer title and update the options menu
			drawerToggle.DrawerOpened += (o, args) =>
			{
				ActionBar.Title = drawerTitle;
				InvalidateOptionsMenu();
			};

			//Set the drawer lister to be the toggle.
			drawerLayout.SetDrawerListener(drawerToggle);

			//If first time you will want to go ahead and click first item.
			if (savedInstanceState == null)
			{
				ListItemClicked(0);
			}

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);
		}

		protected override void OnPostCreate(Bundle savedInstanceState)
		{
			base.OnPostCreate(savedInstanceState);
			drawerToggle.SyncState();
		}

		public override void OnConfigurationChanged(Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);
			drawerToggle.OnConfigurationChanged(newConfig);
		}

		// Pass the event to ActionBarDrawerToggle, if it returns
		// true, then it has handled the app icon touch event
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (drawerToggle.OnOptionsItemSelected(item))
				return true;

			return base.OnOptionsItemSelected(item);
		}

		private void ListItemClicked(int position)
		{
			var transition = false;

			if (lastSelectedSection == position)
				return;

			lastSelectedSection = position;

			Fragment fragment = null;

			switch (position)
			{
				case 0:
					fragment = ScheduleFragment.NewInstance ();
					transition = true;
					break;
				case 1:
					fragment = NewsFragment.NewInstance ();
					transition = true;
					break;
				case 2:
					transition = false;
					var email = new Intent (Android.Content.Intent.ActionSend);
					email.PutExtra (Android.Content.Intent.ExtraEmail, 
					new string[]{ "info@lgsasoftball.org" });
					email.PutExtra (Android.Content.Intent.ExtraSubject, "Question");
					email.SetType ("message/rfc822");
					StartActivity (email);
					
					break;
			}

			if (transition) {
				FragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, fragment)
					.Commit ();

				drawerListView.SetItemChecked (position, true);
				ActionBar.Title = currentSectionTitle = sections [position];
			}

			drawerLayout.CloseDrawer(drawerListView);
		}

		public override bool OnPrepareOptionsMenu(IMenu menu)
		{
			var drawerOpen = drawerLayout.IsDrawerOpen(drawerListView);

			//When we open the drawer we usually do not want to show any menu options
			for (int i = 0; i < menu.Size(); i++)
				menu.GetItem(i).SetVisible(!drawerOpen);

			return base.OnPrepareOptionsMenu(menu);
		}
	}
}


