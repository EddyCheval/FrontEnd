using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MarcassinMobile.Utilitaire
{
    public class ObservableCollectionConvert
    {
        public static ObservableCollection<T> ObservableCollectionConvertion<T>(List<T> list) 
        {
            var ObList = new ObservableCollection<T>(list);
            return ObList;
        }
    }
}
