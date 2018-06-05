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
	public partial class PageNote : ContentPage
	{
        JSGroupe groupe = new JSGroupe();
        List<JSMembre> Tuteur;
        bool Modif = false;
        public PageNote(JSGroupe groupe)
        {
            InitializeComponent();
            Title = "PageNote";
            this.groupe = groupe;
            RecupTuteur();
        }
        public async void VerifNote()
        {
            if (!(groupe.Competence is null))
            {
                var req = await HttpRequest.getRequest(App.Url + "api/Notes?filter[where][Id_Tutore]=" + Settings.ActualUser.id_Employe + "&filter[where][Id_Tuteur]=" + Tuteur.First().id_Employe + "&filter[where][Id_Competence]=" + groupe.Competence.id_Competence);
                var res = JsonConvert.DeserializeObject<List<JSNote>>(req);
                if (res.Count() > 0)
                {
                    await DisplayAlert("Attention", "Une note a deja été donnée. Un second enregistrement modifira la note précédente", "OK");
                    Modif = true;
                }
            }
        }
        public async void RecupTuteur()
        {
            var req = await HttpRequest.getRequest(App.Url + "api/Membres?filter[include][employe]&filter[where][EstTutorant]=true&filter[where][Id_Groupe]=" + groupe.id_Groupe);
            var res = JsonConvert.DeserializeObject<List<JSMembre>>(req);
            Tuteur = res;
            System.Diagnostics.Debug.WriteLine(res);
            System.Diagnostics.Debug.WriteLine(App.Url + "api/Membres?filter[include][employe]&filter[where][EstTutorant]=true&filter[where][Id_Groupe]=" + groupe.id_Groupe);
            if (Tuteur.Count() > 0)
            {
                if (Tuteur.FirstOrDefault().id_Employe != Settings.ActualUser.id_Employe)
                {
                    if (!(groupe.Competence is null))
                    {
                        if (groupe.Competence.IntituleCompetences.Where(c => c.id_Langue == Settings.ActualLanguage).Count() >= 1)
                        {
                            Competence.Text = groupe.Competence.IntituleCompetences.Where(c => c.id_Langue == Settings.ActualLanguage).First().intitule;
                        }
                    }
                    else
                    {
                        Competence.Text = "Compétence";
                    }
                    Employe.Text = Tuteur.First().Employe.nom + " " + Tuteur.First().Employe.prenom;

                    VerifNote();
                }
                else
                {
                    await DisplayAlert("Vous êtes Tuteur de ce groupe", "Retour à la selection", "ok");
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await DisplayAlert("Absence de Tuteur", "Retour à la selection", "ok");
                await Navigation.PopAsync();

            }
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(Settings.ActualUser.id_Employe);
            if (Tuteur.Count() > 0)
            {
                JSNote note = new JSNote
                {
                    id_Tuteur = Tuteur.First().id_Employe,
                    id_Competence = groupe.Competence.id_Competence.Value,
                    id_Tutore = Settings.ActualUser.id_Employe,
                    note = NoteGenerale.SelectedIndex,
                    noteRelationnel = NoteRelationnel.SelectedIndex,
                    noteCommunication = NoteCommunication.SelectedIndex,
                    noteConnaissance = NoteConnaissance.SelectedIndex,
                    notePedagogie = NotePedagogie.SelectedIndex
                };

                if (Modif)
                {
                    var request = await HttpRequest.postRequest(App.Url + "api/Notes/replaceOrCreate", note);
                }
                else
                {
                    var request = await HttpRequest.postRequest(App.Url + "api/Notes", note);
                }
                await DisplayAlert("Merci pour votre note", "Note enregistrée", "ok");
                await Navigation.PushAsync(new SelectionGroupe());
            }
            else
            {
                await DisplayAlert("Error", "le groupe n'a pas de tuteur", "ok");
            };

        }
    }
}