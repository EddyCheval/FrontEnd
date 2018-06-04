using MarcassinMobile.JSONObject;
using MarcassinMobile.Utilitaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MarcassinMobile
{
	public partial class MainPage : ContentPage
	{
        public MainPage()
        {
            InitializeComponent();
        }
        private async void Request()
        {
            var val = entry.Text;
            var val2 = await HttpRequest.getRequest("http://192.168.42.215:3000/api/Employes?filter[include][languePossedes]=langue&filter[where][Nom]=" + val);// "http://192.168.0.29:3000/api/Employes?filter[where][Nom]=" + val);
            List<JSEmploye> JSE = new List<JSEmploye>();
            JSE = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JSONObject.JSEmploye>>(val2);
            label.Text = JSE.First().nom;
            labelLanguePossede.Text = JSE.First().LanguePossedes.First()._Default.ToString();
            labelLangue.Text = JSE.First().LanguePossedes.First().Langue.nom;
        }
        public void mButton_Clicked(object sender, EventArgs e)
        {
            Request();
            System.Diagnostics.Debug.WriteLine(label.Text);
        }

    }
}
