using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarcassinMobile.JSONObject
{
    public class JSMembre
    {
        [JsonProperty(PropertyName = "Id_Employe")]
        private int _id_Employe;
        [JsonIgnore]
        public int id_Employe
        {
            get { return _id_Employe; }
            set { _id_Employe = value; }
        }

        [JsonProperty(PropertyName = "Id_Groupe")]
        private int _id_Groupe;
        [JsonIgnore]
        public int id_Groupe
        {
            get { return _id_Groupe; }
            set { _id_Groupe = value; }
        }

        [JsonProperty(PropertyName = "EstTutorant")]
        private bool _estTutorant;
        [JsonIgnore]
        public bool estTutorant
        {
            get { return _estTutorant; }
            set { _estTutorant = value; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "employe")]
        private JSEmploye _employe;
        [JsonIgnore]
        public JSEmploye Employe
        {
            get { return _employe; }
            set { _employe = value; }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "groupe")]
        private JSGroupe _groupe;
        [JsonIgnore]
        public JSGroupe Groupe
        {
            get { return _groupe; }
            set { _groupe = value; }
        }
    }
}
