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
	public class NewsFragment : ListFragment
  	{
		string[] items = new[] { "Coach Needed", "Golf Outing Fundraiser" };

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			ListAdapter = new ScheduleAdapter (Activity, items);
		}

		public static NewsFragment NewInstance()
		{
			var newsFragment = new NewsFragment { Arguments = new Bundle() };
			return newsFragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);
			return inflater.Inflate(Resource.Layout.news, null);
		}
	}
}