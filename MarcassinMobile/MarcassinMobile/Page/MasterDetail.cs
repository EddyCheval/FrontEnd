using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MarcassinMobile.Utilitaire;

namespace MarcassinMobile.Page
{
    public class MasterDetail : MasterDetailPage
    {
        Menu pageMenu;
        public MasterDetail()
        {
            pageMenu = new Menu();
            pageMenu.OnMenuSelect = (categoryPage) =>
            {

                if (categoryPage.GetType() == typeof(PageConnexion))
                {
                    Detail = new NavigationPage(categoryPage)
                    {
                        BarBackgroundColor = Color.FromHex("#B32F3150"),
                        BarTextColor = Color.White
                    };
                    IsGestureEnabled = false;
                    IsPresented = false;
                    Settings.ActualUser = null;
                }
                else
                {
                    Detail = new NavigationPage(categoryPage)
                    {
                        BarBackgroundColor = Color.FromHex("#B32F3150"),
                        BarTextColor = Color.White
                    };
                    IsPresented = false;
                }
            };
            Master = pageMenu;
            var nav = new NavigationPage(new PageRecherche())
            {
                BarBackgroundColor = Color.FromHex("#B32F3150"),
                BarTextColor = Color.White
            };
            Detail = nav;
        }
    }
}
