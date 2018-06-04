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

[assembly: ExportRenderer(typeof(CustomStackLayout), typeof(CustomStackLayoutRenderer))]
namespace MarcassinMobile.Droid.CustomRenderer
{

    class CustomStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {

        private GradientDrawable _gradientBackground;
        public CustomStackLayoutRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);
            var view = (CustomStackLayout)Element;
            if(view is null) { return; }
            Paint(view);
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomStackLayout.CustomBorderColorProperty.PropertyName ||
                    e.PropertyName == CustomStackLayout.CustomBorderRadiusProperty.PropertyName ||
                    e.PropertyName == CustomStackLayout.CustomBorderWidthProperty.PropertyName)
            {
                if (Element != null)
                {
                    Paint((CustomStackLayout)Element);
                }
            }
        }
        private void Paint(CustomStackLayout view)
        {
            _gradientBackground = new GradientDrawable();
            _gradientBackground.SetShape(ShapeType.Rectangle);
            // Thickness of the stroke line  
            _gradientBackground.SetStroke((int)view.CustomBorderWidth, view.CustomBorderColor.ToAndroid());
            // Radius for the curves  
            _gradientBackground.SetCornerRadius(
                DpToPixels(this.Context, Convert.ToSingle(view.CustomBorderRadius)));
            // set the background of the label  
            this.SetBackground(_gradientBackground);
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}
