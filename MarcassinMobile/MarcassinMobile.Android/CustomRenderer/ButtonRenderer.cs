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

[assembly: ExportRenderer(typeof(BoutonPerso), target: typeof(CurvedCornersButtonRenderer))]
namespace MarcassinMobile.Droid.CustomRenderer
{ 
    public class CurvedCornersButtonRenderer : ButtonRenderer
    {
        public CurvedCornersButtonRenderer(Context context) : base(context){}

        private GradientDrawable _gradientBackground;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            var view = (BoutonPerso)Element;
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
                    Paint((BoutonPerso)Element);
                }
            }
        }
        private void Paint(BoutonPerso view)
        {
            _gradientBackground = new GradientDrawable();
            _gradientBackground.SetShape(ShapeType.Rectangle);
            _gradientBackground.SetColor(view.CustomBackgroundColor.ToAndroid());
            // Thickness of the stroke line  
            _gradientBackground.SetStroke((int)view.CustomBorderWidth, view.CustomBorderColor.ToAndroid());
            // Radius for the curves  
            _gradientBackground.SetCornerRadius(
                DpToPixels(this.Context, Convert.ToSingle(view.CustomBorderRadius)));
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
