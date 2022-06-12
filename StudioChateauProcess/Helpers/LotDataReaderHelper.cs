using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioChateauProcess.Models;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Json;
using StudioChateauAPI.Models;

namespace StudioChateauProcess.Helpers
{
    class LotDataReaderHelper
    {
        public static Lots ReadLots(string pathToXML)
        {
            string fileName = pathToXML;
            DataContractSerializer dcs = new DataContractSerializer(typeof(Lots));
            FileStream fs = new FileStream(fileName, FileMode.Open);

            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            
            Lots lotInputDatas = (Lots)dcs.ReadObject(reader);


            reader.Close();
            fs.Close();

            return lotInputDatas;
        }

        public static XMLDump StoreXML(string fileName)
        {
            string fileContent = null;
            FileStream stringFS = new FileStream(fileName, FileMode.Open);

            using (StreamReader reader = new StreamReader(stringFS))
            {
                fileContent = reader.ReadToEnd();
            }


            XMLDump xmlDump = new XMLDump();
            xmlDump.XML = fileContent;
            xmlDump.CreatedBy = "Process";

            string jsonDump = GenericJSON<XMLDump>.GetJSON(xmlDump);

            string response = (string) GenericRESTCall<XMLDump>.GenericPostCall("http://localhost:9090/ProjectConfig/XMLDumps", jsonDump);

            XMLDump xmlDummp = GenericRESTCall<XMLDump>.GetObject(response);

            return xmlDump;
        }
    }
}
