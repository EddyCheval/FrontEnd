using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MarcassinMobile.Utilitaire
{
    public class MessageViewCell : ViewCell
    {
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create("Name", typeof(string), typeof(MessageViewCell), "");
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public static readonly BindableProperty ContenuProperty =
            BindableProperty.Create("Contenu", typeof(string), typeof(MessageViewCell), "");
        public string Contenu
        {
            get { return (string)GetValue(ContenuProperty); }
            set { SetValue(ContenuProperty, value); }
        }
        public static readonly BindableProperty DateProperty =
            BindableProperty.Create("Date", typeof(string), typeof(MessageViewCell),"");
    
        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }
    }
}
