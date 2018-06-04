using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarcassinMobile.JSONObject
{
    public class JSNote
    {
        [JsonProperty(PropertyName = "Id_Tutore")]
        private int _id_Tutore;
        [JsonIgnore]
        public int id_Tutore
        {
            set { _id_Tutore = value; }
            get { return _id_Tutore; }
        }
        [JsonProperty(PropertyName = "Id_Tuteur")]
        private int _id_Tuteur;
        [JsonIgnore]
        public int id_Tuteur
        {
            set { _id_Tuteur = value; }
            get { return _id_Tuteur; }
        }
        [JsonProperty(PropertyName = "Id_Competence")]
        private int _id_Competence;
        [JsonIgnore]
        public int id_Competence
        {
            set { _id_Competence = value; }
            get { return _id_Competence; }
        }
        [JsonProperty(PropertyName = "Note")]
        private int _note;
        [JsonIgnore]
        public int note
        {
            set {
                if (value > 10)
                {
                    _note = 10;
                }
                else if (value < 0)
                {
                    _note = 0;
                }
                else
                {
                    _note = value;
                }
            }
            get { return _note; }
        }

        [JsonProperty(PropertyName = "NoteCommunication")]
        private int _noteCommunication;
        [JsonIgnore]
        public int noteCommunication
        {
            set
            {
                if (value > 10)
                {
                    _noteCommunication = 10;
                }
                else if (value < 0)
                {
                    _noteCommunication = 0;
                }
                else
                {
                    _noteCommunication = value;
                }
            }
            get { return _noteCommunication; }
        }
        [JsonProperty(PropertyName = "NoteConnaissance")]
        private int _noteConnaissance;
        [JsonIgnore]
        public int noteConnaissance
        {
            set
            {
                if (value > 10)
                {
                    _noteConnaissance = 10;
                }
                else if (value < 0)
                {
                    _noteConnaissance = 0;
                }
                else
                {
                    _noteConnaissance = value;
                }
            }
            get { return _noteConnaissance; }
        }
        [JsonProperty(PropertyName = "NotePedagogie")]
        private int _notePedagogie;
        [JsonIgnore]
        public int notePedagogie
        {
            set
            {
                if (value > 10)
                {
                    _notePedagogie = 10;
                }
                else if (value < 0)
                {
                    _notePedagogie = 0;
                }
                else
                {
                    _notePedagogie = value;
                }
            }
            get { return _notePedagogie; }
        }
        [JsonProperty(PropertyName = "NoteRelationnel")]
        private int _noteRelationnel;
        [JsonIgnore]
        public int noteRelationnel
        {
            set
            {
                if (value > 10)
                {
                    _noteRelationnel = 10;
                }
                else if (value < 0)
                {
                    _noteRelationnel = 0;
                }
                else
                {
                    _noteRelationnel = value;
                }
            }
            get { return _noteRelationnel; }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore,PropertyName = "Tutore")]
        private JSEmploye _tutore;
        [JsonIgnore]
        public JSEmploye tutore
        {
            get { return _tutore; }
            set { _tutore = value; }
        }
    }
}
