using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcassinMobile.Utilitaire;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarcassinMobile.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menu : ContentPage
	{
        public Action<ContentPage> OnMenuSelect
        {
            get;
            set;
        }
		public Menu ()
		{
			InitializeComponent ();
            Title = "Menu";
            Nom.Text = Settings.ActualUser.nom;
            Prenom.Text = Settings.ActualUser.prenom;
            AdresseMail.Text = Settings.ActualUser.adresseMail;
            LienLinkedIn.Text = Settings.ActualUser.linkedIn;
            Entreprise.Text = Settings.ActualUser.entreprise;
            Service.Text = Settings.ActualUser.service;
            Padding = new Thickness(0, 0);
            var categories = new List<MenuItem>()
            {
                new MenuItem("MainPage",() => new MainPage()),
                new MenuItem("Noter un Tuteur", () => new SelectionGroupe()),
                new MenuItem("Profil",() => new Profil()),
                new MenuItem("MesDemandes",() => new LesDemandes()),
                new MenuItem("Demande",() => new PageDemande()),
                new MenuItem("Recherche",() => new PageRecherche()),
                new MenuItem("Créer un Groupe", () => new CreationGroupe()),
                new MenuItem("Deconnexion",() => new PageConnexion())
            };
            listView.ItemsSource = categories;
            listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
                if (((ListView)sender).SelectedItem == null)
                    return;

                if (OnMenuSelect != null)
                {
                    var category = (MenuItem)e.SelectedItem;
                    var categoryPage = category.PageFn();
                    OnMenuSelect(categoryPage);
                    ((ListView)sender).SelectedItem = null;
                }
            };
        }
        
    }
}