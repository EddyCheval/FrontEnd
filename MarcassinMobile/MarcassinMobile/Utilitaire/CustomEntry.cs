using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace MarcassinMobile.Utilitaire
{
    class CustomEntry : Entry
    {
        public static readonly BindableProperty CustomBorderColorProperty =
        BindableProperty.Create(nameof(CustomBorderColor),
            typeof(Color), typeof(CustomEntry), Color.Gray);
        // Gets or sets BorderColor value  
        public Color CustomBorderColor
        {
            get => (Color)GetValue(CustomBorderColorProperty);
            set => SetValue(CustomBorderColorProperty, value);
        }

        public static readonly BindableProperty CustomBorderWidthProperty =
        BindableProperty.Create(nameof(CustomBorderWidth), typeof(int),
            typeof(CustomEntry), 2);
        // Gets or sets BorderWidth value  
        public int CustomBorderWidth
        {
            get { return (int)GetValue(CustomBorderWidthProperty); }
            set { SetValue(CustomBorderWidthProperty, value); }
        }
        public static readonly BindableProperty CustomCornerRadiusProperty =
        BindableProperty.Create(nameof(CustomCornerRadius),
            typeof(double), typeof(CustomEntry),7.0);
        // Gets or sets CornerRadius value  
        public double CustomCornerRadius
        {
            get { return (double)GetValue(CustomCornerRadiusProperty); }
            set { SetValue(CustomCornerRadiusProperty, value);}
        }
        public static readonly BindableProperty IsCurvedCornersEnabledProperty =
        BindableProperty.Create(nameof(IsCurvedCornersEnabled),
            typeof(bool), typeof(CustomEntry), true);
        // Gets or sets IsCurvedCornersEnabled value  
        public bool IsCurvedCornersEnabled
        {
            get { return (bool)GetValue(IsCurvedCornersEnabledProperty); }
            set { SetValue(IsCurvedCornersEnabledProperty, value); }
        }
    }
}
