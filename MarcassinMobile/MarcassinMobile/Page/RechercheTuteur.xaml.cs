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
            foreach (var liaisonComp in jS)
            {
                try
                {
                    var reqNote = await HttpRequest.getRequest(App.Url + "api/Notes?filter[where][Id_Tuteur]=" + liaisonComp.id_Employe + "&filter[where][Id_Competence]=" + liaisonComp.id_Competence);
                    List<JSNote> jSNote = JsonConvert.DeserializeObject<List<JSNote>>(reqNote);
                    var moyenne = 0;
                    var compteur = 0;
                    if (jSNote.Count() > 0)
                    {
                        foreach (var Note in jSNote)
                        {
                            var noteMoyenne = (Note.note + Note.noteCommunication + Note.noteConnaissance + Note.notePedagogie + Note.noteRelationnel) / 5;
                            moyenne += noteMoyenne;
                            compteur++;
                        }
                        moyenne = moyenne / compteur;
                    }
                    else
                    {
                        moyenne = 0;
                    }
                    liaisonComp.note = moyenne;

                }
                catch
                {
                    liaisonComp.note = 0;
                }
                /* ListLiaisonCompetenceSelected.Remove(liaisonComp.Where(c => c.id_DuTuteur == Settings.ActualUser.id_Employe));*/

            }
            ListLiaisonCompetenceSelected = ObservableCollectionConvert.ObservableCollectionConvertion(jS);
            ListTuteurXAML.ItemsSource = ListLiaisonCompetenceSelected;
        }

        async private void Valider_Clicked(object sender, EventArgs e)
        {
            var temoin = 0;
            JSLiaisonCompetence LiaisonComp = ((JSLiaisonCompetence)ListTuteurXAML.SelectedItem);

            JSDemande demande = new JSDemande
            {

                id_Competence = LiaisonComp.id_Competence,
                id_DuTuteur = LiaisonComp.id_Employe,
                id_Employe = Settings.ActualUser.id_Employe
            };

            var reqDemande = await HttpRequest.getRequest(App.Url + "api/Demandes");
            List<JSDemande> jS = JsonConvert.DeserializeObject<List<JSDemande>>(reqDemande);
            var ListDemandeCheck = ObservableCollectionConvert.ObservableCollectionConvertion(jS);

            foreach (var demandeCheck in ListDemandeCheck)
            {
                if (demande.id_Employe == demandeCheck.id_Employe && demande.id_Competence == demandeCheck.id_Competence)
                {
                    temoin = 1;
                }
            }
            System.Diagnostics.Debug.WriteLine(temoin);
            if (temoin == 0)
            {
                var req = await HttpRequest.postRequest(App.Url + "api/Demandes/", demande);
                await Navigation.PushAsync(new PageDemande());
            }
            else
            {
                var req = await HttpRequest.postRequest(App.Url + "api/Demandes/update?where[Id_Employe]=" + demande.id_Employe + "&[Id_Competence]=" + demande.id_Competence, demande);
                await Navigation.PushAsync(new PageDemande());
            }


        }
    }
}