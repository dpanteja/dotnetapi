using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Mail;
using System.IO;
using Newtonsoft.Json.Linq;
using StudioChateauProcess.Models;
using StudioChateauProcess.Helpers;
using StudioChateauAPI.Models;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Runtime.Caching;
using System.Threading;

namespace StudioChateauProcess
{
    class Program
    {
        private static string token = "";
        

        static void Main(string[] args)
        {
            List<StudioChateauAPIErrorLog> errorLogs = new List<StudioChateauAPIErrorLog>();

            token = AuthenticationHelper.Authenticate();

            Thread.SetData(Thread.GetNamedDataSlot("Token"), token);

            HandleMessages.SendMessages(null);

            string externalServiceURL = System.Configuration.ConfigurationManager.AppSettings["externalServicePath"];

            List<ExternalServices> services = GenericRESTCall<ExternalServices>.GenericGetCall(externalServiceURL);

            int index = 0;

            foreach (var externalService in services)
            {
                string folderPath = externalService.FolderLocation;
                XMLDump xmlDump = null;
                int builderId = externalService.BuilderId;

                string[] fileEntries = Directory.GetFiles(@folderPath);

                foreach (string fileName in fileEntries)
                {
                    Lots lots = LotDataReaderHelper.ReadLots(fileName);

                    xmlDump = LotDataReaderHelper.StoreXML(fileName);

                    foreach(StudioChateauProcess.Models.Lot lotObject in lots){
                        DataDistributionHelper dataDistributionHelper = new DataDistributionHelper(lots.ElementAt<StudioChateauProcess.Models.Lot>(index), builderId);

                        List<StudioChateauAPIErrorLog> errors = dataDistributionHelper.MakeAPICalls(externalService.ExternalServiceId,
                            xmlDump.ID, index);

                        if (errors.Count > 0)
                        {
                            errorLogs.AddRange(errors);
                        }

                        ++index;
                    }

                    HandleMessages.SendMessages(errorLogs);

                }
            } 
        }

    }

    class HandleMessages
    {
        public static void SendMessages(List<StudioChateauAPIErrorLog> errors)
        {
            StringBuilder sb = new StringBuilder();

            if (errors != null)
            {
                foreach (StudioChateauAPIErrorLog errorEntry in errors)
                {
                    StringBuilder currentEntry = new StringBuilder();

                    currentEntry.Append(errorEntry.XML_Dump_Id.ToString());
                    currentEntry.Append(' ');
                    currentEntry.Append(errorEntry.XML_Element_Sequence.ToString());
                    currentEntry.Append(' ');
                    currentEntry.Append(errorEntry.Error_Message);
                    currentEntry.Append(' ');
                    currentEntry.Append(errorEntry.External_Service_Id.ToString());

                    sb.AppendLine(currentEntry.ToString());
                }
            }
            string emailGroups = System.Configuration.ConfigurationManager.AppSettings["emailList"];
            string fromEmail = System.Configuration.ConfigurationManager.AppSettings["emailList"];
            string fromEmailPassword = System.Configuration.ConfigurationManager.AppSettings["emailPassword"];
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(
                  fromEmail, fromEmailPassword);
                MailMessage msg = new MailMessage();
                msg.To.Add(emailGroups);
                msg.From = new MailAddress(fromEmail);
                msg.Subject = "Error Log";
                msg.Body = sb.ToString();
                client.Send(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }

    class GenericRESTCall<REAL>
    {
        public static List<REAL> GenericGetCall(string path)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(path);
            req.Method = "GET";
            req.Accept = "appliation/json";
            req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Thread.GetData(Thread.GetNamedDataSlot("Token")));

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            var responseString = new StreamReader(resp.GetResponseStream()).ReadToEnd();

            JToken arrayOfType = JToken.Parse(responseString);

            JArray listOfItems = (JArray)arrayOfType["value"];

            if (listOfItems == null)
                listOfItems = (JArray)arrayOfType;

            List<REAL> resultList = new List<REAL>();

            foreach (var curElement in listOfItems)
            {
                REAL curObject = JsonConvert.DeserializeObject<REAL>(curElement.ToString());
                resultList.Add(curObject);
            }

            return resultList; ;
        }


        public static string GenericPostCall(string path, string postData)
        {
            postData = postData.Replace(@"\", string.Empty);

            byte[] buffer = Encoding.ASCII.GetBytes(postData);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(path);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Accept = "application/json";
            req.ContentLength = buffer.Length;
            req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Thread.GetData(Thread.GetNamedDataSlot("Token")));

            Stream strm = req.GetRequestStream();
            strm.Write(buffer, 0, buffer.Length);
            strm.Close();

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            var responseString = new StreamReader(resp.GetResponseStream()).ReadToEnd();

            return responseString;
        }

        public static REAL GetObject(string responseString)
        {
            JToken arrayOfType = JToken.Parse(responseString);

            JObject listOfItems = (JObject)arrayOfType["value"];

            if (listOfItems == null)
                listOfItems = (JObject)arrayOfType;

            REAL curObject = JsonConvert.DeserializeObject<REAL>(listOfItems.ToString());

            return curObject;
        }

        public static int GetObjectID(string responseString, string fieldtToCheck)
        {
            JObject root = JObject.Parse(responseString);

            JToken jtoken;

            jtoken = root.First;
            string returnValue = "";

            while (jtoken != null)
            {
                String name = ((JProperty)jtoken).Name.ToString();
                String value = ((JProperty)jtoken).Value.ToString();

                jtoken = jtoken.Next;

                if (name == fieldtToCheck)
                {
                    returnValue = value;
                    break;
                }
            }

            return Int32.Parse(returnValue);
        }
        
    }
}
