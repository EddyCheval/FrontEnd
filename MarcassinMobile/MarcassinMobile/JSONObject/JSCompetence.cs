using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarcassinMobile.JSONObject
{
    public class JSCompetence
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore,PropertyName ="Id_Competence")]
        private Nullable<int> _id_Competence;

        [JsonIgnore]
        public Nullable<int> id_Competence
        {
            get { return _id_Competence; }
            set { _id_Competence = value; }
        }
        [JsonProperty(PropertyName ="Actuel")]
        private bool _actuel;

        [JsonIgnore]
        public bool actuel
        {
            get { return _actuel; }
            set { _actuel = value; }
        }
        [JsonProperty(PropertyName ="Actif")]
        private bool _actif;

        [JsonIgnore]
        public bool actif
        {
            get { return _actif; }
            set { _actif = value; }
        }
        [JsonProperty(PropertyName = "Annee")]
        private string _annee;

        [JsonIgnore]
        public string annee
        {
            get { return _annee; }
            set { _annee = value; }
        }

        [JsonProperty(PropertyName ="Id_CompetenceActuel")]
        private Nullable<int> _id_CompetenceActuel;

        [JsonIgnore]
        public Nullable<int> id_CompetenceActuel
        {
            get { return _id_CompetenceActuel; }
            set { _id_CompetenceActuel = value; }
        }

        [JsonProperty(NullValueHandling =NullValueHandling.Ignore,PropertyName ="intituleCompetences")]
        private List<JSIntituleCompetence> _intituleCompetences;

        [JsonIgnore]
        public List<JSIntituleCompetence> IntituleCompetences
        {
            get { return _intituleCompetences; }
            set { _intituleCompetences = value; }
        }
    }
}
