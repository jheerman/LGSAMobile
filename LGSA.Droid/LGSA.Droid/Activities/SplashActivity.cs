﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;

namespace LGSA.Droid
{
	[Activity(MainLauncher = true, Theme="@style/Theme.Splash", NoHistory=true)]			
	public class SplashActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			Thread.Sleep (3000);
			StartActivity (typeof(MainActivity));
		}
	}
}

