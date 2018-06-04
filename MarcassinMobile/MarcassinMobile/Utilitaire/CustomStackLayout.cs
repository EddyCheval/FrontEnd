using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MarcassinMobile.Utilitaire
{
    class CustomStackLayout : StackLayout
    {
        public static readonly BindableProperty CustomBorderWidthProperty = 
            BindableProperty.Create(
                nameof(CustomBorderWidth), 
                typeof(int), 
                typeof(CustomStackLayout),
                4);
        public int CustomBorderWidth
        {
            get { return (int)GetValue(CustomBorderWidthProperty); }
            set { SetValue(CustomBorderWidthProperty, value); }
        }
        public static readonly BindableProperty CustomBorderColorProperty =
            BindableProperty.Create(
                nameof(CustomBorderColor),
                typeof(Color),
                typeof(CustomStackLayout)
                , Color.Default);
        public Color CustomBorderColor

        {
            get { return (Color)GetValue(CustomBorderColorProperty); }
            set { SetValue(CustomBorderColorProperty, value); }
        }
        public static readonly BindableProperty CustomBorderRadiusProperty =
            BindableProperty.Create(
                nameof(CustomBorderRadius),
                typeof(int),
                typeof(CustomStackLayout)
                , 4);
        public int CustomBorderRadius

        {
            get { return (int)GetValue(CustomBorderRadiusProperty); }
            set { SetValue(CustomBorderRadiusProperty, value); }
        }
    }
}
