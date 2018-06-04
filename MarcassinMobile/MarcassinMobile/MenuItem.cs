using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MarcassinMobile
{
    class MenuItem
    {
        public String Name
        {
            get;
            set;
        }

        public String Icon
        {
            get;
            set;
        }

        public String JSIcon
        {
            get;
            set;
        }

        public Func<ContentPage> PageFn
        {
            get;
            set;
        }

        public MenuItem(String name, Func<ContentPage> pageFn, String icon = null, String jsicon = null)
        {
            Icon = icon;
            JSIcon = jsicon;
            Name = name;
            PageFn = pageFn;
        }
    }
}
