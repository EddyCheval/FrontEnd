using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MarcassinMobile.JSONObject
{
    public class JSEmploye
    {

        [JsonProperty(PropertyName = "Id_Employe")]
        private int _id_Employe;
        public int id_Employe
        {
            get { return _id_Employe; }
            set { _id_Employe = value; }
        }
        [JsonProperty(PropertyName = "Nom")]
        private string _nom;
        public string nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        [JsonProperty(PropertyName = "Prenom")]
        private string _prenom;
        public string prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }
        [JsonProperty(PropertyName = "Service")]
        private string _service;
        public string service
        {
            get { return _service; }
            set { _service = value; }
        }
        [JsonProperty(PropertyName = "AdresseMail")]
        private string _adresseMail;
        public string adresseMail
        {
            get { return _adresseMail; }
            set { _adresseMail = value; }
        }
        [JsonProperty(PropertyName = "Entreprise")]
        private string _entreprise;
        public string entreprise
        {
            get { return _entreprise; }
            set { _entreprise = value; }
        }

        [JsonProperty(PropertyName = "EstInterne")]
        private bool _estInterne;
        public bool estInterne
        {
            get { return _estInterne; }
            set { _estInterne = value; }
        }
        [JsonProperty(PropertyName = "EstAdmin")]
        private bool _estAdmin;
        public bool estAdmin
        {
            get { return _estAdmin; }
            set { _estAdmin = value; }
        }
        [JsonProperty(PropertyName = "EstChefDeService")]
        private bool _estChefDeService;
        public bool estChefDeService
        {
            get { return _estChefDeService; }
            set { _estChefDeService = value; }
        }
        [JsonProperty(PropertyName = "Actif")]
        private bool _actif;
        public bool actif
        {
            get { return _actif; }
            set { _actif = value; }
        }
        [JsonProperty(PropertyName = "DateArrive")]
        private DateTime _dateArrive;
        public DateTime dateArrive
        {
            get { return _dateArrive; }
            set { _dateArrive = value; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore ,PropertyName = "DateDepart")]
        private DateTime _dateDepart;
        public DateTime dateDepart
        {
            get { return _dateDepart; }
            set { _dateDepart = value; }
        }
        [JsonProperty(PropertyName = "Metier")]
        private string _metier;
        public string metier
        {
            get { return _metier; }
            set { _metier = value; }
        }
        [JsonProperty(PropertyName = "LienLinkedIn")]
        private string _linkedIn;
        public string linkedIn
        {
            get { return _linkedIn; }
            set { _linkedIn = value; }
        }

        [JsonProperty(PropertyName = "languePossedes")]
        private List<JSLanguePossede> _languepossede;
        public List<JSLanguePossede> LanguePossedes
        {
            get { return _languepossede; }
            set { _languepossede = value; }
        }

        //Partie ajouté
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore,PropertyName = "Note")]
        private List<JSNote> _note;
        public List<JSNote> note
        {
            get { return _note; }
            set { _note = value; }
        }

        //Partie ajouté
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Demande")]
        private List<JSDemande> _demande;
        public List<JSDemande> demande
        {
            get { return _demande; }
            set { _demande = value; }
        }

    }
}
