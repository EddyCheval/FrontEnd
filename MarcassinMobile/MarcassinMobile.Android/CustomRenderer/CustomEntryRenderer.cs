using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MarcassinMobile.Droid.CustomRenderer;
using MarcassinMobile.Utilitaire;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), target: typeof(CustomEntryRenderer))]
namespace MarcassinMobile.Droid.CustomRenderer
{
    class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context): base(context){}

        private GradientDrawable _gradientBackground;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);
            var view = (CustomEntry)Element;
            if (view == null) return;
            Paint(view);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == BoutonPerso.CustomBorderColorProperty.PropertyName ||
                    e.PropertyName == BoutonPerso.CustomBorderRadiusProperty.PropertyName ||
                    e.PropertyName == BoutonPerso.CustomBorderWidthProperty.PropertyName)
            {
                if (Element != null)
                {
                    Paint((CustomEntry)Element);
                }
            }
        }
        private void Paint(CustomEntry view)
        {
            _gradientBackground = new GradientDrawable();
            _gradientBackground.SetShape(ShapeType.Rectangle);
            // Thickness of the stroke line  
            _gradientBackground.SetStroke((int)view.CustomBorderWidth, view.CustomBorderColor.ToAndroid());
            // Radius for the curves  
            _gradientBackground.SetCornerRadius(
                DpToPixels(this.Context, Convert.ToSingle(view.CustomCornerRadius)));
            // set the background of the label  
            Control.SetBackground(_gradientBackground);
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}