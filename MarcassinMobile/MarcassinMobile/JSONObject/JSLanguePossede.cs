using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MarcassinMobile.JSONObject
{
    public class JSLanguePossede
    {
        [JsonProperty(PropertyName = "Id_Langue")]
        private int _id_Langue;

        public int id_Langue
        {
            get { return _id_Langue; }
            set { _id_Langue = value; }
        }

        [JsonProperty(PropertyName = "Id_Employe")]
        private int _id_Employe;

        public int id_Employe
        {
            get { return _id_Employe; }
            set { _id_Employe = value; }
        }

        [JsonProperty(PropertyName = "Default")]
        private bool _default;

        public bool _Default
        {
            get { return _default; }
            set { _default = value; }
        }
       [JsonProperty(NullValueHandling = NullValueHandling.Include, PropertyName = "langue")]
        private JSLangue _langue;
        public JSLangue Langue
        {
            get { return _langue; }
            set { _langue = value; }
        }
    }
}
