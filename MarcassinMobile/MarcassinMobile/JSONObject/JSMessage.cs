using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarcassinMobile.JSONObject
{
    public class JSMessage
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore,PropertyName = "Id_Message")]
        private Nullable<int> _id_Message;

        [JsonIgnore]
        public Nullable<int> id_Message
        {
            get { return _id_Message; }
            set { _id_Message = value; }
        }
        [JsonProperty(PropertyName = "Id_Groupe")]
        private int _id_Groupe;
        [JsonIgnore]
        public int id_Groupe
        {
            get { return _id_Groupe; }
            set { _id_Groupe = value; }
        }
        [JsonProperty(PropertyName = "Id_Expediteur")]
        private int _id_Expediteur;

        [JsonIgnore]
        public int id_Expediteur
        {
            get { return _id_Expediteur; }
            set { _id_Expediteur = value; }
        }
        [JsonProperty(PropertyName = "Contenu")]
        private string _contenu;

        [JsonIgnore]
        public string contenu
        {
            get { return _contenu; }
            set { _contenu = value; }
        }
        [JsonProperty(PropertyName = "Date")]
        private DateTime _date;

        [JsonIgnore]
        public DateTime date
        {
            get { return _date; }
            set { _date = value; }
        }
        [JsonProperty(PropertyName = "employe")]
        private JSEmploye _employe;

        [JsonIgnore]
        public JSEmploye Employe
        {
            get { return _employe; }
            set { _employe = value; }
        }
    }
}
