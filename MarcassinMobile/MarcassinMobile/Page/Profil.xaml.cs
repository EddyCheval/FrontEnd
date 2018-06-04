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
	public partial class Profil : ContentPage
	{
        private bool Edition;
        private JSEmploye employe;

        private ObservableCollection<JSLiaisonCompetence> ListCompetence;
        private ObservableCollection<JSMembre> ListGroupe;
        private List<int> listnote;
		public Profil (bool edition = false, JSEmploye jSEmploye = null)
		{
			InitializeComponent ();

            Edition = edition;
            Title = "Profil";
            if (jSEmploye == null)
            {
                employe = Settings.ActualUser;
            }
            else
            {
                employe = jSEmploye;
            }
            Nom.Text = employe.nom;
            Prenom.Text = employe.prenom;
            Adresse.Text = employe.adresseMail;
            Metier.Text = employe.metier;
            Service.Text = employe.service;
            Entreprise.Text = employe.entreprise;
            LinkedIn.Text = employe.linkedIn;
            EstAdmin.IsToggled = employe.estAdmin;
            EstChef.IsToggled = employe.estChefDeService;
            EstInterne.IsToggled = employe.estInterne;
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);

            BtnGBox.IsVisible = true;
            BtnCBox.IsVisible = false;
            BtnRBox.IsVisible = false;
            ReqCompetenceProfil();
            ReqGroupeProfil();
        }
        public async void ReqGroupeProfil()
        {
            var req = await HttpRequest.getRequest(App.Url + "api/Membres?filter[include][groupe][competences][intituleCompetences]&filter[where][Id_Employe]=" + employe.id_Employe);
            List<JSMembre> jS = JsonConvert.DeserializeObject<List<JSMembre>>(req);
            ListGroupe = ObservableCollectionConvert.ObservableCollectionConvertion(jS);
            CntPGroupe.ItemsSource = ListGroupe;
        }
        public async void ReqCompetenceProfil()
        {
            var req = await HttpRequest.getRequest(App.Url+ "api/LiaisonCompetences?filter[include][competence][intituleCompetences]&filter[where][Id_Employe]=" + employe.id_Employe);
            List<JSLiaisonCompetence> jS = JsonConvert.DeserializeObject<List<JSLiaisonCompetence>>(req);
            ListCompetence = ObservableCollectionConvert.ObservableCollectionConvertion(jS);
            ListViewComp.ItemsSource = ListCompetence;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(sender == BtnCompetence)
            {
                CntPCompetence.IsVisible = true;
                CntPGeneral.IsVisible = false;
                CntPGroupe.IsVisible = false;
                BtnGBox.IsVisible = false;
                BtnCBox.IsVisible = true;
                BtnRBox.IsVisible = false;
            }
            else if(sender == BtnGeneral) {

                BtnGBox.IsVisible = true;
                BtnCBox.IsVisible = false;
                BtnRBox.IsVisible = false;
                CntPCompetence.IsVisible = false;
                CntPGeneral.IsVisible = true;
                CntPGroupe.IsVisible = false;
            }
            else if(sender == BtnGroupe)
            {
                CntPCompetence.IsVisible = false;
                CntPGeneral.IsVisible = false;
                CntPGroupe.IsVisible = true;

                BtnGBox.IsVisible = false;
                BtnCBox.IsVisible = false;
                BtnRBox.IsVisible = true;
            }
            
        }

        private async void TapGestureRecognizer_GridReunion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AffichageGroupe(((JSMembre)((Grid)sender).BindingContext).Groupe,false, employe));
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AffichageCompetence(((JSLiaisonCompetence)((Grid)sender).BindingContext).Competence,employe));
        }

        private async void EstTutorant_Toggled(object sender, ToggledEventArgs e)
        {
                var Competence = ((JSLiaisonCompetence)((Grid)((Switch)sender).Parent).BindingContext);
                var req2 = await HttpRequest.getRequest(App.Url + "api/LiaisonCompetences?filter[where][Id_Competence]=" + Competence.id_Competence + "&filter[where][Id_Employe]=" + Settings.ActualUser.id_Employe);
                var res = JsonConvert.DeserializeObject<List<JSLiaisonCompetence>>(req2);
                if (((Switch)sender).IsToggled != res.First().estTutorant)
                {
                    if (Edition == true)
                    {
                        JSLiaisonCompetence jSCompetence = new JSLiaisonCompetence
                        {
                            estTutorant = ((Switch)sender).IsToggled,
                            id_Competence = Competence.id_Competence,
                            id_Employe = Settings.ActualUser.id_Employe
                        };
                        var req = await HttpRequest.postRequest(App.Url + "api/LiaisonCompetences/update?where[Id_Competence]=" + Competence.id_Competence, jSCompetence);
                }
                else
                {
                    await DisplayAlert("Erreur", "Vous n'avez pas les droits sur ce Profil", "Ok");
                    ((Switch)sender).IsToggled = !((Switch)sender).IsToggled;
                }
            }
        }
    }
}
