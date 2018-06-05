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
	public partial class CreationGroupe : ContentPage
	{
        JSGroupe groupe = new JSGroupe();
        List<JSIntituleCompetence> ListComp = new List<JSIntituleCompetence>();
        List<JSEmploye> ListEmp = new List<JSEmploye>();
        List<JSEmploye> listEmpInitial = new List<JSEmploye>();
        public CreationGroupe ()
		{
			InitializeComponent ();
            DateReunion.MinimumDate = DateTime.Now;
            groupe.Participant = new List<JSEmploye>();
            ChargementData();
		}
        private async void ChargementData()
        {

            var reqComp = await HttpRequest.getRequest(App.Url + "api/IntituleCompetences?filter[include][competence]&filter[where][Id_Langue]=" + Settings.ActualLanguage);
            ListComp = JsonConvert.DeserializeObject<List<JSIntituleCompetence>>(reqComp);

            var reqEmp = await HttpRequest.getRequest(App.Url + "api/Employes");
            ListEmp = JsonConvert.DeserializeObject<List<JSEmploye>>(reqEmp);

            Competence.ItemsSource = ListComp;   
            ListEmploye.ItemsSource = ListEmp;
            

        }


        private async void Confirm_Clicked(object sender, EventArgs e)
        {
            groupe.titre = Titre.Text;
            groupe.dateReunion = DateReunion.Date;
            groupe.dateCreation = DateTime.Now;
            groupe.id_Groupe = null;
            var req = await HttpRequest.postRequest(App.Url + "api/Groupes", groupe);
            var req2 = await HttpRequest.getRequest(App.Url + "api/Groupes?filter[limit]=1&filter[order]=Id_Groupe%20DESC");
            var res = JsonConvert.DeserializeObject<List<JSGroupe>>(req2);
            System.Diagnostics.Debug.WriteLine(req2);
            foreach (JSEmploye emp2 in groupe.Participant)
            {
                if (listEmpInitial.Where(c => c.id_Employe == emp2.id_Employe).Count() == 0)
                {
                     var ObjEmp = new JSMembre
                    {
                        id_Employe = emp2.id_Employe,
                        id_Groupe = res.First().id_Groupe.Value,
                        estTutorant = false
                    };
                    var reqAdd = await HttpRequest.postRequest(App.Url + "api/Membres", ObjEmp);
                }
            }
                var ObjEmp2 = new JSMembre
                {
                    id_Employe = Settings.ActualUser.id_Employe,
                    id_Groupe = res.First().id_Groupe.Value,
                    estTutorant = true
                };
                var reqAdd2 = await HttpRequest.postRequest(App.Url + "api/Membres", ObjEmp2);
            await Navigation.PushModalAsync(new Profil());
        }

        private void ListParticipant_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            groupe.Participant.Remove(((JSEmploye)ListParticipant.SelectedItem));
            ListParticipant.ItemsSource = ObservableCollectionConvert.ObservableCollectionConvertion(groupe.Participant);
        }

        private async void ListEmploye_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (groupe.Participant.Where(c => c.id_Employe == ((JSEmploye)ListEmploye.SelectedItem).id_Employe).Count() == 0)
            {
                groupe.Participant.Add(((JSEmploye)ListEmploye.SelectedItem));
                ListParticipant.ItemsSource = ObservableCollectionConvert.ObservableCollectionConvertion(groupe.Participant);
            }
            else
            {
                await DisplayAlert("Erreur", "La cible est déjâ ajoutée", "Ok");
            }

        }

        private void Competence_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            groupe.Competence = ((JSIntituleCompetence)Competence.SelectedItem).Competence;
            IntituleCompActuel.Text = ((JSIntituleCompetence)Competence.SelectedItem).intitule;
            DescriptionCompActuel.Text = ((JSIntituleCompetence)Competence.SelectedItem).description;
        }
        
    }
}