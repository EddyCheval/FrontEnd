using MarcassinMobile.JSONObject;
using MarcassinMobile.Utilitaire;
using Newtonsoft.Json;
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
	public partial class SelectionGroupe : ContentPage
    {
        JSGroupe groupe = new JSGroupe();
        List<JSMembre> Listgr = new List<JSMembre>();
        public SelectionGroupe()
        {
            InitializeComponent();
            Title = "Selection du Groupe";
            requeteGroupe();
        }

        public async void requeteGroupe()
        {
            var request = await HttpRequest.getRequest(App.Url + "api/Membres?filter[include][groupe][competences][intituleCompetences]&filter[where][Id_Employe]=" + Settings.ActualUser.id_Employe);
            System.Diagnostics.Debug.WriteLine(request);
            var res = JsonConvert.DeserializeObject<List<JSMembre>>(request);
            Listgr = res;
            Liste.ItemsSource = ObservableCollectionConvert.ObservableCollectionConvertion(Listgr);
        }

        private async void Liste_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, ((JSMembre)Liste.SelectedItem).Groupe.dateReunion) > 0)
            {
                await DisplayAlert("Groupe sélectionné", "Vous avez sélectionné un groupe", "suivant");
                await Navigation.PushAsync(new PageNote(((JSMembre)Liste.SelectedItem).Groupe));
            }
            else
            {
                await DisplayAlert("erreur", "La réunion n'a pas eu lieu", "ok");
            }
        }
    }
}