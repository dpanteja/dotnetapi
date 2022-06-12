using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace StudioChateauProcess
{
    class AuthenticationHelper
    {
        private static string token = "";

        public static string Authenticate()
        {
            string tokenPath = System.Configuration.ConfigurationManager.AppSettings["tokenPath"];
            string processUser = System.Configuration.ConfigurationManager.AppSettings["processUser"];
            string processUserPassword = System.Configuration.ConfigurationManager.AppSettings["processUserPassword"];
            string grantType = System.Configuration.ConfigurationManager.AppSettings["grantType"];

            string postData = "username=" + processUser +
                              "&password=" + processUserPassword +
                              "&grant_type=" + grantType;

            byte[] buffer = Encoding.ASCII.GetBytes(postData);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(tokenPath);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = buffer.Length;

            Stream strm = req.GetRequestStream();
            strm.Write(buffer, 0, buffer.Length);
            strm.Close();

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            var responseString = new StreamReader(resp.GetResponseStream()).ReadToEnd();

            return ParseResponse(responseString);

        }

        private static string ParseResponse(string responseString)
        {
            JObject root = JObject.Parse(responseString);

            JToken jtoken;

            jtoken = root.First;

            while (jtoken != null)
            {
                String name = ((JProperty)jtoken).Name.ToString();
                String value = ((JProperty)jtoken).Value.ToString();

                jtoken = jtoken.Next;

                if (name == "access_token")
                {
                    token = value;
                    break;
                }
            }

            return token;
        }
 
    }
}
