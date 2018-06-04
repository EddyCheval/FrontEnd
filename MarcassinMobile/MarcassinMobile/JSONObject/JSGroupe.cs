using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarcassinMobile.JSONObject
{
    public class JSGroupe
    {

        [JsonProperty(PropertyName ="Titre")]
        private string _titre;
        [JsonIgnore]
        public string titre
        {
            get { return _titre; }
            set { _titre = value; }
        }
        [JsonProperty(PropertyName ="DateReunion")]
        private DateTime _dateReunion;
        [JsonIgnore]
        public DateTime dateReunion
        {
            get { return _dateReunion; }
            set { _dateReunion = value; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "DateCreation")]
        private DateTime _dateCreation;
        [JsonIgnore]
        public DateTime dateCreation
        {
            get { return _dateCreation; }
            set { _dateCreation = value; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName ="Id_Competence")]
        private int _id_Competence;
        [JsonIgnore]
        public int id_Competence
        {
            get { return _id_Competence; }
            set { _id_Competence = value; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "competences")]
        private JSCompetence _competence;
        [JsonIgnore]
        public JSCompetence Competence
        {
            set { _competence = value; }
            get { return  _competence; }
        }

        [JsonProperty( NullValueHandling = NullValueHandling.Ignore,PropertyName ="Tuteur")]
        private JSEmploye _tuteur = new JSEmploye();
        [JsonIgnore]
        public JSEmploye tuteur
        {
            get { return _tuteur; }
            set { _tuteur = value; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName ="Id_Groupe")]
        private Nullable<int> _id_Groupe;
        [JsonIgnore]
        public Nullable<int> id_Groupe
        {
            get { return _id_Groupe; }
            set { _id_Groupe = value; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore,PropertyName ="membres")]
        private List<JSEmploye> _participant;
        [JsonIgnore]
        public List<JSEmploye> Participant
        {
            get { return _participant; }
            set { _participant = value; }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "EstValider")]
        private bool _EstValider;
        [JsonIgnore]
        public bool estValider
        {
            get { return _EstValider; }
            set { _EstValider = value; }
        }
        }
}
