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
	public partial class PageRecherche : ContentPage
	{
        private string SearchURL;
        public PageRecherche ()
		{
			InitializeComponent ();
		}

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            switch (SearchPicker.SelectedIndex)
            {
                case 0:
                    SearchURL = App.Url + "api/IntituleCompetences?filter[include][competence]&filter[where][or][0][Intitule][like]=%" + SearchEntry.Text+"%";
                    var req = await HttpRequest.getRequest(SearchURL);
                    var val = JsonConvert.DeserializeObject<List<JSIntituleCompetence>>(req);
                    SearchViewCompetence.ItemsSource = ObservableCollectionConvert.ObservableCollectionConvertion(val);
                    break;
                case 1:
                    SearchURL = App.Url + "api/Employes?filter[where][or][0][Nom][like]=%"+SearchEntry.Text + "%&filter[where][or][1][Prenom][like]=%"+SearchEntry.Text+"%";
                    var req2 = await HttpRequest.getRequest(SearchURL);
                    var val2 = JsonConvert.DeserializeObject<List<JSEmploye>>(req2);
                    SearchViewEmploye.ItemsSource = ObservableCollectionConvert.ObservableCollectionConvertion(val2);
                    break;
                case 2:
                    SearchURL = App.Url + "api/Groupes?filter[include][competences]&filter[where][titre]=%"+SearchEntry.Text+"%";
                    var req3 = await HttpRequest.getRequest(SearchURL);
                    var val3 = JsonConvert.DeserializeObject<List<JSGroupe>>(req3);
                    SearchViewGroupe.ItemsSource = ObservableCollectionConvert.ObservableCollectionConvertion(val3);
                    break;
                default:
                    break;
            }
        }

        private void SearchPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (SearchPicker.SelectedIndex)
            {
                case 0:
                    SearchViewCompetence.IsVisible = true;
                    SearchViewEmploye.IsVisible = false;
                    SearchViewGroupe.IsVisible = false;
                    break;
                case 1:
                    SearchViewCompetence.IsVisible = false;
                    SearchViewEmploye.IsVisible = true;
                    SearchViewGroupe.IsVisible = false;
                    break;
                case 2:
                    SearchViewCompetence.IsVisible = false;
                    SearchViewEmploye.IsVisible = false;
                    SearchViewGroupe.IsVisible = true;
                    break;
                default:
                    break;
            }
        }

        private async void SearchViewEmploye_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync((new Profil(false,((JSEmploye)SearchViewEmploye.SelectedItem))));
        }

        private async void SearchViewGroupe_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync((new AffichageGroupe(((JSGroupe)SearchViewGroupe.SelectedItem))));
        }
        private async void SearchViewCompetence_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(((JSIntituleCompetence)SearchViewCompetence.SelectedItem).Competence.annee);
            await Navigation.PushAsync(new GraphiqueCompetence(((JSIntituleCompetence)SearchViewCompetence.SelectedItem)));
        }
    }
}