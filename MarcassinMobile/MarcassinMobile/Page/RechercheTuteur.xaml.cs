using MarcassinMobile.JSONObject;
using MarcassinMobile.Utilitaire;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarcassinMobile.Page
{
    //Partie ajouté
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RechercheTuteur : ContentPage
    {
        private ObservableCollection<JSEmploye> ListTuteur;
        private ObservableCollection<JSLiaisonCompetence> ListLiaisonCompetenceSelected;
        JSIntituleCompetence competence = new JSIntituleCompetence();
        public RechercheTuteur(JSIntituleCompetence competence)
        {
            InitializeComponent();
            this.competence = competence;
            CreateLsiteCompetence();
        }

        public async void CreateLsiteCompetence()
        {
            //partie nom/prenom
            var id_competence = competence.id_Competence;
            var req = await HttpRequest.getRequest(App.Url + "api/LiaisonCompetences?filter[where][Id_Competence]=" + id_competence + "&filter[where][EstTutorant]=true&filter[include][employe][NotesTuteur]");
            List<JSLiaisonCompetence> jS = JsonConvert.DeserializeObject<List<JSLiaisonCompetence>>(req);
            ListLiaisonCompetenceSelected = ObservableCollectionConvert.ObservableCollectionConvertion(jS);
            foreach (var liaisonComp in ListLiaisonCompetenceSelected)
            {
                try
                {
                    liaisonComp.note = (liaisonComp.Employe.note.Where(c => c.id_Competence == id_competence).First().note +
                                        liaisonComp.Employe.note.Where(c => c.id_Competence == id_competence).First().noteCommunication +
                                        liaisonComp.Employe.note.Where(c => c.id_Competence == id_competence).First().noteConnaissance +
                                        liaisonComp.Employe.note.Where(c => c.id_Competence == id_competence).First().notePedagogie +
                                        liaisonComp.Employe.note.Where(c => c.id_Competence == id_competence).First().noteRelationnel) / 5;
                }
                catch
                {
                    liaisonComp.note = 0;
                }
            }

            ListTuteurXAML.ItemsSource = ListLiaisonCompetenceSelected;
        }

        async private void Valider_Clicked(object sender, EventArgs e)
        {
            JSLiaisonCompetence LiaisonComp = ((JSLiaisonCompetence)ListTuteurXAML.SelectedItem);

            JSDemande demande = new JSDemande
            {

                id_Competence = LiaisonComp.id_Competence,
                id_DuTuteur = LiaisonComp.id_Employe,
                id_Employe = Settings.ActualUser.id_Employe
            };

            var req = await HttpRequest.postRequest(App.Url + "api/Demandes/", demande);
            await Navigation.PushAsync(new PageDemande());

        }
    }
}