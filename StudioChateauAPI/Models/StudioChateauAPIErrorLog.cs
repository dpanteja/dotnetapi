using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Models
{
    public class StudioChateauAPIErrorLog
    {
        public int ID { get; set; }
        public int External_Service_Id { get; set; }
        public int XML_Dump_Id { get; set; }
        public int XML_Element_Sequence { get; set; }
        public string Error_Message { get; set; }
        public string CreatedBy { get; set; }
    }

}