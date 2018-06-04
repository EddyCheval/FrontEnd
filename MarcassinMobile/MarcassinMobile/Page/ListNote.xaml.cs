using MarcassinMobile.JSONObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarcassinMobile.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListNote : ContentPage
	{
		public ListNote (List<JSNote> Notes)
		{
			InitializeComponent ();
            notes.ItemsSource = Notes;
		}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NoteCompetence(((JSNote)((Grid)sender).BindingContext)));
        }
    }
}