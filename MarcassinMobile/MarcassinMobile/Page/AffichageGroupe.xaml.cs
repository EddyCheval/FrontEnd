using MarcassinMobile.JSONObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcassinMobile.Utilitaire;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace MarcassinMobile.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AffichageGroupe : ContentPage
	{
        JSGroupe groupe = new JSGroupe();
        List<JSIntituleCompetence> ListComp = new List<JSIntituleCompetence>();
        List<JSEmploye> ListEmp = new List<JSEmploye>();
        List<JSEmploye> listEmpInitial = new List<JSEmploye>();
        private JSEmploye employe;
        bool ActualStatus = false;
        bool boo = false;
		public AffichageGroupe (JSGroupe jSGroupe, bool modif = false, JSEmploye jSEmploye = null)
		{
            ActualStatus = modif;
			InitializeComponent();
            this.Title = "Paramètre du groupe";
            if (jSEmploye == null)
            {
                employe = Settings.ActualUser;
            }
            else
            {
                employe = jSEmploye;
            }
            groupe = jSGroupe;
            CheckDroit();
            ChargementData(modif);
            
		}
        private async void CheckDroit()
        {
            var req = await HttpRequest.getRequest(App.Url + "api/Membres?filter[where][Id_Groupe]=" + groupe.id_Groupe + "&filter[where][Id_Employe]=" + employe.id_Employe);
            var reqdes = JsonConvert.DeserializeObject<List<JSMembre>>(req);
            if(reqdes.Count() >0)
                boo = reqdes.First().estTutorant;
            else
            {
                boo = false;
            }

            ModifierBtn.IsVisible = boo;
            
        }
        private async void ChargementData(bool modif)
        {
            var reqTuteur = await HttpRequest.getRequest(App.Url + "api/Membres?filter[include][employe]&filter[where][Id_Groupe]=" + groupe.id_Groupe);
            System.Diagnostics.Debug.WriteLine(reqTuteur);
            var res = JsonConvert.DeserializeObject<List<JSMembre>>(reqTuteur);

            var reqComp = await HttpRequest.getRequest(App.Url + "api/IntituleCompetences?filter[include][competence]&filter[where][Id_Langue]=" + Settings.ActualLanguage);
            ListComp = JsonConvert.DeserializeObject<List<JSIntituleCompetence>>(reqComp);

            var reqEmp = await HttpRequest.getRequest(App.Url + "api/Employes");
            ListEmp = JsonConvert.DeserializeObject <List<JSEmploye>>(reqEmp);
            if (res.Where(c => c.estTutorant == true).Count() != 0)
                groupe.tuteur = res.Where(c => c.estTutorant == true).First().Employe;
            var listp = new List<JSEmploye>();
            foreach (JSMembre m in res)
            {
                if (m.Employe != null)
                {
                    listp.Add(m.Employe);
                    listEmpInitial.Add(m.Employe);
                }
                else
                {
                    var reqManquant = await HttpRequest.getRequest(App.Url + "api/Employes/" + m.id_Employe);
                    var r = JsonConvert.DeserializeObject<JSEmploye>(reqManquant);

                    listp.Add(r);
                    listEmpInitial.Add(r);
                }
            }
            groupe.Participant = listp;

            Titre.Text = groupe.titre;
            ModifTitre.Text = groupe.titre;

            if (!(groupe.Competence is null)){
                if (groupe.Competence.IntituleCompetences is null)
                {
                    var req = await HttpRequest.getRequest(App.Url + "api/IntituleCompetences?filter[where][Id_Langue]=" + Settings.ActualLanguage+"&filter[where][Id_Competence]="+groupe.Competence.id_Competence);
                    var reqDes = JsonConvert.DeserializeObject<List<JSIntituleCompetence>>(req);
                    if(reqDes.Count > 0)
                    {
                        groupe.Competence.IntituleCompetences = reqDes;
                    }
                    else
                    {
                        var JSi = new JSIntituleCompetence
                        {
                            description = "description",
                            intitule = "intitule",
                            id_Langue = 2
                        };
                        groupe.Competence.IntituleCompetences = new List<JSIntituleCompetence>();
                        groupe.Competence.IntituleCompetences.Add(JSi);
                    }
                }
                //Competence
                if (groupe.Competence.IntituleCompetences.Where(c => c.id_Langue == Settings.ActualLanguage).ToList().Count > 0)
                {
                    Competence.Text = groupe.Competence.IntituleCompetences.Where(c => c.id_Langue == Settings.ActualLanguage).First().intitule;
                    IntituleCompActuel.Text = groupe.Competence.IntituleCompetences.Where(c => c.id_Langue == Settings.ActualLanguage).First().intitule;
                    if (groupe.Competence.IntituleCompetences.Where(c => c.id_Langue == Settings.ActualLanguage).First().description != "")
                        DescriptionCompActuel.Text = groupe.Competence.IntituleCompetences.Where(c => c.id_Langue == Settings.ActualLanguage).First().description.ToString();
                    else
                    {
                        DescriptionCompActuel.Text = "Description";
                    }
                }
                else if (groupe.Competence.IntituleCompetences.ToList().Count > 0)
                {
                    Competence.Text = groupe.Competence.IntituleCompetences.First().intitule;
                    IntituleCompActuel.Text = groupe.Competence.IntituleCompetences.First().intitule;
                    if (groupe.Competence.IntituleCompetences.First().description != "")
                        DescriptionCompActuel.Text = groupe.Competence.IntituleCompetences.First().description;
                    else
                    {
                        DescriptionCompActuel.Text = "Description";
                    }
                }
                else
                {
                    Competence.Text = "Intitule";
                    IntituleCompActuel.Text = "Intitule";
                    DescriptionCompActuel.Text = "Description";
                }
            }
            ModifCompetence.ItemsSource = ListComp;

            //Membre et Tuteur
            Tuteur.BindingContext = groupe.tuteur;
            ListMembreVue.ItemsSource = ObservableCollectionConvert.ObservableCollectionConvertion(groupe.Participant);
            ModifListParticipant.ItemsSource = ObservableCollectionConvert.ObservableCollectionConvertion(groupe.Participant);
            ListEmploye.ItemsSource = ListEmp; 

            if (modif == false)
            {
                AvecModif.IsVisible = false;
                SansModif.IsVisible = true;
            }
            else
            {
                AvecModif.IsVisible = true;
                SansModif.IsVisible = false;
            }

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Navig
            await Navigation.PushAsync(new Profil(false,((JSEmploye)ListMembreVue.SelectedItem)));
        }
        private async void Tuteur_Tapped(object sender, EventArgs e)
        {
            //Navig
            if (groupe.tuteur != null)
            {
                await Navigation.PushAsync(new Profil(false, groupe.tuteur));
            }
        }
        private async void ConfirmModif_Clicked(object sender, EventArgs e)
        {
            groupe.titre = ModifTitre.Text;
            var req = await HttpRequest.postRequest(App.Url + "api/Groupes/update?where[Id_Groupe]=" + groupe.id_Groupe, groupe);
            foreach(JSEmploye emp in listEmpInitial)
            {
                System.Diagnostics.Debug.WriteLine(groupe.Participant.Count());
                System.Diagnostics.Debug.WriteLine(listEmpInitial.Count());

                if (groupe.Participant.Where(c => c.id_Employe == emp.id_Employe).Count() == 0)
                {
                    System.Diagnostics.Debug.WriteLine("une différence Delete :"+ emp.id_Employe+" :: "+ groupe.Participant.Where(c => c.id_Employe == emp.id_Employe).Count());
                    var reqDelete = await HttpRequest.deleteRequest(App.Url + "api/Membres?where[Id_Groupe]=" + groupe.id_Groupe + "&where[Id_Employe]=" + emp.id_Employe);
                }
            }
            foreach(JSEmploye emp2 in groupe.Participant)
            {
                if (listEmpInitial.Where(c => c.id_Employe == emp2.id_Employe).Count() == 0)
                {
                    System.Diagnostics.Debug.WriteLine("une différence Add :" + emp2.id_Employe + " :: " + groupe.Participant.Where(c => c.id_Employe == emp2.id_Employe).Count());
                    var ObjEmp = new JSMembre
                    {
                        id_Employe = emp2.id_Employe,
                        id_Groupe = groupe.id_Groupe.Value,
                        estTutorant = false
                    };
                    var reqAdd = await HttpRequest.postRequest(App.Url + "api/Membres",ObjEmp);
                }
            }
            System.Diagnostics.Debug.Write(req);
            ActualStatus = false;
            ChargementData(ActualStatus);
        }

        private void ModifListParticipant_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            groupe.Participant.Remove(((JSEmploye)ModifListParticipant.SelectedItem));
            ModifListParticipant.ItemsSource = ObservableCollectionConvert.ObservableCollectionConvertion(groupe.Participant);
        }

        private async void ListEmploye_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (groupe.Participant.Where(c => c.id_Employe == ((JSEmploye)ListEmploye.SelectedItem).id_Employe).Count() == 0)
            {
                groupe.Participant.Add(((JSEmploye)ListEmploye.SelectedItem));
                ModifListParticipant.ItemsSource = ObservableCollectionConvert.ObservableCollectionConvertion(groupe.Participant);
            }
            else
            {
                await DisplayAlert("Erreur", "La cible est déjâ ajoutée", "Ok");
            }

        }

        private void ModifCompetence_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            groupe.Competence = ((JSIntituleCompetence)ModifCompetence.SelectedItem).Competence;
            IntituleCompActuel.Text = ((JSIntituleCompetence)ModifCompetence.SelectedItem).intitule;
            DescriptionCompActuel.Text = ((JSIntituleCompetence)ModifCompetence.SelectedItem).description;   
        }

        private void ModifierBtn_Clicked(object sender, EventArgs e)
        {
            ActualStatus = true;
            ChargementData(ActualStatus);
        }

        private async void BtnMessagerie_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MessagerieGroupe(groupe.id_Groupe.Value));
        }
    }
}