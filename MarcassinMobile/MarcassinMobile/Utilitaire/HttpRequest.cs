using MarcassinMobile.JSONObject;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MarcassinMobile.Utilitaire
{
    class HttpRequest
    {
        
       

        //Fonction pour les requêtes HTTP en PUT
        //url : url du ws à appeler
        //values : Dictionnaires <key, value> contenant les paramètres à envoyer
        //return : String réponse du ws
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="values"></param>
        /// <returns></returns>
       public static async Task<string> postRequest(String url, object JSONObj)
        {
            String responseString = String.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = new System.TimeSpan(0, 0, 0, 30, 0);
                   
                    var JSONObjse = Newtonsoft.Json.JsonConvert.SerializeObject(JSONObj);
                    System.Diagnostics.Debug.WriteLine(JSONObjse);
                    var post = await client.PostAsync(url,new StringContent(JSONObjse,encoding: Encoding.UTF8,mediaType: "application/json")).ConfigureAwait(false);
                    System.Diagnostics.Debug.WriteLine(JSONObjse);
                    return post.StatusCode.ToString();
                }
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                System.Diagnostics.Debug.WriteLine(e.Message);
                return " {\"code\":0, \"error\":\"" + e + "\"}";
            }
        }
        public static async Task<string> deleteRequest(String url)
        {
            String responseString = String.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = new System.TimeSpan(0, 0, 0, 30, 0);
                    var delete = await client.DeleteAsync(url);
                    return delete.StatusCode.ToString();
                }
            }
            catch(System.Exception e)
            {
                return " {\"code\":0, \"error\":\"" + e + "\"}";
            }
        }
        //Fonction pour les requêtes HTTP en GET
        //url : url du ws à appeler
        //return : String réponse du ws 
        public static async Task<String> getRequest(String url)
        {
            var responseString = String.Empty;
            System.Diagnostics.Debug.WriteLine(url);
            using (var client = new HttpClient())
            {
                responseString = await client.GetStringAsync(url);

                return responseString;
            }
        }
        public static async Task<String> getRequest(Uri url)
        {
            var responseString = String.Empty;
            using (var client = new HttpClient())
            {
                responseString = await client.GetStringAsync(url);
                return responseString;
            }
        }
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
