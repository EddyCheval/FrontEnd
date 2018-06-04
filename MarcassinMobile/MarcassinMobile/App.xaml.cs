using MarcassinMobile.JSONObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MarcassinMobile
{
	public partial class App : Application
	{
        public static int ScreenWidth;
        public static int ScreenHeight;
        public static string Url = "http://192.168.42.215:3000/"; //192.168.0.30:3000/"; 

        public App ()
		{
			InitializeComponent();
			MainPage = new MarcassinMobile.Page.PageConnexion();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
