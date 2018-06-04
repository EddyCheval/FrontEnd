using MarcassinMobile.JSONObject;
using MarcassinMobile.Utilitaire;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarcassinMobile.Page
{
    //Partie ajouté
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDemande : ContentPage
    {

        private ObservableCollection<JSDemande> ListDemande;
        private ObservableCollection<JSCompetence> ListCompetence;
        private ObservableCollection<JSIntituleCompetence> ListIntituleCompetence;
        public PageDemande()
        {
            InitializeComponent();
            GetAllDemande();
            GetAllComp();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender == testScroll)
            {
                xurblax.IsVisible = true;
                xurblux.IsVisible = false;
            }
            else if (sender == testScroll2)
            {
                xurblax.IsVisible = false;
                xurblux.IsVisible = true;
            }
        }

        // Partie view liste demande
        public async void GetAllDemande()
        {
            var request = await HttpRequest.getRequest(App.Url + "api/Demandes?filter[include][Competence][intituleCompetences]&filter[where][Id_Employe]=" + Settings.ActualUser.id_Employe);
            List<JSDemande> jS = JsonConvert.DeserializeObject<List<JSDemande>>(request);
            ListDemande = ObservableCollectionConvert.ObservableCollectionConvertion(jS);
            foreach (var demande in ListDemande)
            {
                demande.Employe = Settings.ActualUser;
                var requestTuteur = await HttpRequest.getRequest(App.Url + "api/Employes?filter[where][Id_Employe]=" + demande.id_DuTuteur);
                List<JSEmploye> tuteur = JsonConvert.DeserializeObject<List<JSEmploye>>(requestTuteur);
                if (tuteur.Count > 0)
                {
                    demande.Tuteur = tuteur.First();
                }
            }
            ListDemandeXAML.ItemsSource = ListDemande;
        }

        public async void GetAllComp()
        {
            var req = await HttpRequest.getRequest(App.Url + "api/IntituleCompetences?filter[include][competence]&filter[where][Id_Langue]=" + Settings.ActualLanguage);

            List<JSIntituleCompetence> jS = JsonConvert.DeserializeObject<List<JSIntituleCompetence>>(req);
            ListIntituleCompetence = ObservableCollectionConvert.ObservableCollectionConvertion(jS);
            ListCompetenceXAML.ItemsSource = ListIntituleCompetence;
        }

        async private void RechercheTuteur_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RechercheTuteur((JSIntituleCompetence)ListCompetenceXAML.SelectedItem));
        }

        async private void PosterDemande_Clicked(object sender, EventArgs e)
        {
            JSIntituleCompetence IntituleComp = ((JSIntituleCompetence)ListCompetenceXAML.SelectedItem);

            JSDemande demande = new JSDemande
            {
                id_Competence = IntituleComp.id_Competence,
                id_Employe = Settings.ActualUser.id_Employe
            };
            var req = await HttpRequest.postRequest(App.Url + "api/Demandes", demande);
            await Navigation.PushAsync(new PageDemande());
        }
    }
}
