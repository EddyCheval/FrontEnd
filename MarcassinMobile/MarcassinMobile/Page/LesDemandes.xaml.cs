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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LesDemandes : ContentPage
    {

        private ObservableCollection<JSDemande> ListDemande;
        private ObservableCollection<JSLiaisonCompetence> ListCompetence;
        private ObservableCollection<JSDemande> ListeDemande;
        private ObservableCollection<JSDemande> ListeMesDemandes;

        public LesDemandes()
        {
            InitializeComponent();
            ListDemandesFonctions();
            ListMesDemandesFonctions();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender == Demandes)
            {
                ListDemandes.IsVisible = true;
                ListDemandeTutorat.IsVisible = false;
                BtnDemandes.IsVisible = true;
                BtnMesDemandes.IsVisible = false;
            }
            else if (sender == MesDemandes)
            {
                ListDemandes.IsVisible = false;
                ListDemandeTutorat.IsVisible = true;
                BtnDemandes.IsVisible = false;
                BtnMesDemandes.IsVisible = true;
            }
        }

        public async void ListDemandesFonctions()
        {
            var requestCompetence = await HttpRequest.getRequest(App.Url + "api/LiaisonCompetences?filter[where][Id_Employe]=" + Settings.ActualUser.id_Employe);
            List<JSLiaisonCompetence> jSCompetence = JsonConvert.DeserializeObject<List<JSLiaisonCompetence>>(requestCompetence);
            ListCompetence = ObservableCollectionConvert.ObservableCollectionConvertion(jSCompetence);

            List<JSDemande> ListeDemande = new List<JSDemande>();

            foreach (var competence in ListCompetence)
            {
                var request = await HttpRequest.getRequest(App.Url + "api/Demandes?filter[include][Competence][intituleCompetences]&filter[where][id_tuteur]=null&filter[where][Id_Competence]=" + competence.id_Competence);
                List<JSDemande> jS = JsonConvert.DeserializeObject<List<JSDemande>>(request);
                var ListDemandeAdd = ObservableCollectionConvert.ObservableCollectionConvertion(jS);
                ListeDemande.AddRange(ListDemandeAdd);
            }

            if (ListeDemande.Count() > 0)
            {
                foreach (var Demande in ListeDemande)
                {
                    var requestNomPrenom = await HttpRequest.getRequest(App.Url + "api/Employes?filter[where][Id_Employe]=" + Demande.id_Employe);
                    List<JSEmploye> jSEmploye = JsonConvert.DeserializeObject<List<JSEmploye>>(requestNomPrenom);
                    Demande.Employe = jSEmploye.First();
                }
            }

            ListDemandes.ItemsSource = ListeDemande;
        }

        public async void ListMesDemandesFonctions()
        {
            var requestDemande = await HttpRequest.getRequest(App.Url + "api/Demandes?filter[include][Competence][intituleCompetences]&filter[where][Id_DuTuteur]=" + Settings.ActualUser.id_Employe);
            List<JSDemande> jSCompetence = JsonConvert.DeserializeObject<List<JSDemande>>(requestDemande);
            ListeMesDemandes = ObservableCollectionConvert.ObservableCollectionConvertion(jSCompetence);
            System.Diagnostics.Debug.WriteLine(ListeMesDemandes.Count());
            if (ListeMesDemandes.Count() > 0)
            {
                System.Diagnostics.Debug.WriteLine(ListeMesDemandes);
                foreach (var demande in ListeMesDemandes)
                {
                    System.Diagnostics.Debug.WriteLine(demande.id_Employe);
                    var requestNomPrenom = await HttpRequest.getRequest(App.Url + "api/Employes?filter[where][Id_Employe]=" + demande.id_Employe);
                    System.Diagnostics.Debug.WriteLine(requestNomPrenom);
                    List<JSEmploye> jSEmploye = JsonConvert.DeserializeObject<List<JSEmploye>>(requestNomPrenom);
                    demande.Employe = jSEmploye.First();
                }
            }
            System.Diagnostics.Debug.WriteLine(ListeMesDemandes.Count());
            ListDemandeTutorat.ItemsSource = ListeMesDemandes;
        }

        async private void CreateGroupe_Clicked(object sender, EventArgs e)
        {
            
                await Navigation.PushAsync(new CreationGroupe());
        }
    }
}