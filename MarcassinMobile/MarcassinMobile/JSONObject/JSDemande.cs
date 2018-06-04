using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarcassinMobile.JSONObject
{
    public class JSDemande
    {

        [JsonProperty(PropertyName = "Id_Employe")]
        private int _id_Employe;
        [JsonIgnore]
        public int id_Employe
        {
            get { return _id_Employe; }
            set { _id_Employe = value; }
        }


        [JsonProperty(PropertyName = "Id_Competence")]
        private int _id_Competence;
        [JsonIgnore]
        public int id_Competence
        {
            get { return _id_Competence; }
            set { _id_Competence = value; }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Competence")]
        private JSCompetence _competence;
        [JsonIgnore]
        public JSCompetence Competence
        {
            set { _competence = value; }
            get { return _competence; }
        }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Id_DuTuteur")]
        private Nullable<int> _id_DuTuteur;
        [JsonIgnore]
        public Nullable<int> id_DuTuteur
        {
            get { return _id_DuTuteur; }
            set { _id_DuTuteur = value; }
        }
        //Partie ajouté
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "employe")]
        private JSEmploye _employe;
        [JsonIgnore]
        public JSEmploye Employe
        {
            get { return _employe; }
            set { _employe = value; }
        }

        //Partie ajouté
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tuteur")]
        private JSEmploye _tuteur;
        [JsonIgnore]
        public JSEmploye Tuteur
        {
            get { return _tuteur; }
            set { _tuteur = value; }
        }

    }
}
