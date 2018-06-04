using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MarcassinMobile.Droid;
using MarcassinMobile.Utilitaire;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MessageViewCell),typeof(AndroidMessageViewCellRenderer))]
namespace MarcassinMobile.Droid
{
    public class AndroidMessageViewCellRenderer : ViewCellRenderer
    {
        AndroidMessageViewCell cell;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            var messageViewCell = (MessageViewCell)item;
            Console.WriteLine("\t\t" + messageViewCell.Name);

            cell = convertView as AndroidMessageViewCell;
            if (cell == null)
            {
                cell = new AndroidMessageViewCell(context, messageViewCell);
            }
            else
            {
                cell.MessageViewCell.PropertyChanged -= OnAndroidMessageViewCellPropertyChanged;
            }

            messageViewCell.PropertyChanged += OnAndroidMessageViewCellPropertyChanged;

            cell.UpdateCell(messageViewCell);
            return cell;
        }
        void OnAndroidMessageViewCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var messageViewCell = (MessageViewCell)sender;
            if(e.PropertyName == MessageViewCell.NameProperty.PropertyName)
            {
                cell.NameTextView.Text = messageViewCell.Name;
            }
            else if (e.PropertyName == MessageViewCell.ContenuProperty.PropertyName)
            {
                cell.ContenuTextView.Text = messageViewCell.Contenu;
            }
            else if (e.PropertyName == MessageViewCell.DateProperty.PropertyName)
            {
                cell.ContenuTextView.Text = messageViewCell.Date;
            }
                    }
    }
}