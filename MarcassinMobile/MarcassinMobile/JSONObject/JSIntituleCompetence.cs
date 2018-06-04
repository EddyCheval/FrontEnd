using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarcassinMobile.JSONObject
{
    public class JSIntituleCompetence
    {
        [JsonProperty(PropertyName = "Id_Competence")]
        private int _id_Competence;
        public int id_Competence
        {
            get { return _id_Competence; }
            set { _id_Competence = value; }
        }
        [JsonProperty(PropertyName = "Id_Langue")]
        private int _id_Langue;
        public int id_Langue
        {
            get { return _id_Langue; }
            set { _id_Langue = value; }
        }
        [JsonProperty(PropertyName = "Description")]
        private string _description;
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        [JsonProperty(PropertyName = "Intitule")]
        private string _intitule;
        public string intitule
        {
            get { return _intitule; }
            set { _intitule = value; }
        }
        [JsonProperty(PropertyName = "competence")]
        private JSCompetence _competence;
        public JSCompetence Competence
        {
            get { return _competence; }
            set { _competence = value; }
        }
    }
}
