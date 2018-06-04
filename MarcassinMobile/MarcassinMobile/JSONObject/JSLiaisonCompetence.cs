using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarcassinMobile.JSONObject
{
    public class JSLiaisonCompetence
    {
        [JsonProperty(PropertyName = "Id_Employe")]
        private int _id_Employe;
        public int id_Employe
        {
            set { _id_Employe = value; }
            get { return _id_Employe; }
        }

        [JsonProperty(PropertyName = "Id_Competence")]
        private int _id_Competence;
        public int id_Competence
        {
            set { _id_Competence = value; }
            get { return _id_Competence; }
        }
        [JsonProperty(PropertyName = "EstTutorant")]
        private bool _estTutorant;
        public bool estTutorant
        {
            set { _estTutorant = value; }
            get { return _estTutorant; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore,PropertyName = "competence")]
        private JSCompetence _competence;
        public JSCompetence Competence
        {
            set { _competence = value; }
            get { return _competence; }
        }

        //Partie ajouté
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "employe")]
        private JSEmploye _employe;
        public JSEmploye Employe
        {
            set { _employe = value; }
            get { return _employe; }
        }

        //Partie ajouté
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Note")]
        private int _note;
        public int note
        {
            set { _note = value; }
            get { return _note; }
        }

    }
}
