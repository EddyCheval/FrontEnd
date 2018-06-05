using MarcassinMobile.Utilitaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MarcassinMobile.JSONObject;

namespace MarcassinMobile.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageConnexion : ContentPage
	{
		public PageConnexion ()
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var Id = EntryLogin.Text;
            var MDP = EntryMDP.Text;
            if (MDP != null & Id != null)
            {
                MDP = HttpRequest.ComputeSha256Hash(MDP);
                Id = HttpRequest.ComputeSha256Hash(Id);
                try
                {
                    var request = await HttpRequest.getRequest(App.Url+"api/Employes/count?[where][and][1][MotDePasse]=" + MDP + "&[where][and][0][Identifiant]=" + Id);
                    var val = JsonConvert.DeserializeObject<JSCount>(request);
                    if (val.Count == 0)
                    {
                        await DisplayAlert("Erreur", "Aucun compte associé ","ok");
                    }
                    else if(val.Count == 1)
                    {
                        String Uri = App.Url +"api/Employes?filter[where][and][1][MotDePasse]=" + MDP + "&filter[where][and][0][Identifiant]=" + Id;
                        System.Diagnostics.Debug.WriteLine(Uri);
                        var requestReception = await HttpRequest.getRequest(Uri);
                        var Des = JsonConvert.DeserializeObject<List<JSEmploye>>(requestReception);
                        System.Diagnostics.Debug.WriteLine(requestReception);
                        Settings.ActualUser = Des.First();
                        await DisplayAlert("Confirmation", "Connexion Effectué de "+ Settings.ActualUser.prenom + Settings.ActualUser.nom+" " + Des.First().id_Employe, "ok");
                        
                        Settings.ActualLanguage = 2;
                        
                        App.Current.MainPage = new MasterDetail();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Trop de réponse :(", "ok");
                    }
                            
                }
                catch(Exception error)
                {
                    await DisplayAlert("Erreur", "Erreur lors de la connexion :"+error, "ok");
                }
            }
        }
    }
}