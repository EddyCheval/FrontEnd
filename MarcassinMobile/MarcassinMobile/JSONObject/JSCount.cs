using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarcassinMobile.JSONObject
{
    public class JSCount
    {
        [JsonProperty(PropertyName = "count")]
        private int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
    }
}
