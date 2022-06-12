using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Models
{
    public class BuilderAccessList
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public int BuilderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}