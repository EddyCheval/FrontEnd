using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MarcassinMobile.JSONObject
{
    public class JSLangue
    {
        [JsonProperty(PropertyName ="Id_Langue")]
        private int _id_Langue;
        public int id_Langue
        {
            get { return _id_Langue; }
            set { _id_Langue = value; }
        }
        [JsonProperty(PropertyName ="Nom")]
        private string _nom;
        public string nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        [JsonProperty(PropertyName = "Default")]
        private bool _default;
        public bool _Default
        {
            get { return _default; }
            set { _default = value; }
        }
    }
}
