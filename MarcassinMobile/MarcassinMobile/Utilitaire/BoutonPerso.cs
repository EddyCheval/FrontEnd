using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MarcassinMobile.Utilitaire
{
    class BoutonPerso : Button
    {
        public static readonly BindableProperty CustomBorderColorProperty =
           BindableProperty.Create(
               nameof(CustomBorderColor),
               typeof(Color),
               typeof(BoutonPerso),
               Color.Default);

        public Color CustomBorderColor
        {
            get { return (Color)GetValue(CustomBorderColorProperty); }
            set { SetValue(CustomBorderColorProperty, value); }
        }

        public static readonly BindableProperty CustomBorderRadiusProperty =
             BindableProperty.Create(
                 nameof(CustomBorderRadius),
                 typeof(int),
                 typeof(BoutonPerso),
                 4);

        public int CustomBorderRadius
        {
            get { return (int)GetValue(CustomBorderRadiusProperty); }
            set { SetValue(CustomBorderRadiusProperty, value); }
        }

        public static readonly BindableProperty CustomBorderWidthProperty =
             BindableProperty.Create(
                 nameof(CustomBorderWidth),
                 typeof(double),
                 typeof(BoutonPerso),
                 4.0);

        public double CustomBorderWidth
        {
            get { return (double)GetValue(CustomBorderWidthProperty); }
            set { SetValue(CustomBorderWidthProperty, value); }
        }

        public static readonly BindableProperty CustomBackgroundColorProperty =
           BindableProperty.Create(
               nameof(CustomBackgroundColor),
               typeof(Color),
               typeof(BoutonPerso),
               Color.Default
              );

        public Color CustomBackgroundColor
        {
            get { System.Diagnostics.Debug.WriteLine((Color)GetValue(CustomBackgroundColorProperty));
                return (Color)GetValue(CustomBackgroundColorProperty);
            }
            set
            {
                System.Diagnostics.Debug.WriteLine(value);
                SetValue(CustomBackgroundColorProperty, value);
            }
        }
    }
}
