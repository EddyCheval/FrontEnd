using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MarcassinMobile.Utilitaire;
using Xamarin.Forms;

namespace MarcassinMobile.Droid
{
    internal class AndroidMessageViewCell : LinearLayout, INativeElementView
    {
        public TextView NameTextView { get; set; }
        public TextView ContenuTextView { get; set; }
        public TextView DateTextView { get; set; }
        public MessageViewCell MessageViewCell { get; private set; }
        public Element Element => MessageViewCell;
        public AndroidMessageViewCell(Context context, MessageViewCell cell) : base(context)
        {
            MessageViewCell = cell;
            var view = (context as Activity).LayoutInflater.Inflate(Resource.Layout.AndroidMessageViewCell, null);
            NameTextView = view.FindViewById<TextView>(Resource.Id.NameText);
            ContenuTextView = view.FindViewById<TextView>(Resource.Id.ContenuText);
            DateTextView = view.FindViewById<TextView>(Resource.Id.DateText);
            AddView(view);
        }
        public void UpdateCell(MessageViewCell cell)
        {
            NameTextView.Text = cell.Name;
            ContenuTextView.Text = cell.Contenu;
            DateTextView.Text = cell.Date;
        }
    }
}