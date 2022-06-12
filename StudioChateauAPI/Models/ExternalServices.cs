using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Models
{
    public class ExternalServices
    {
        [Key]
        public int ExternalServiceId { get; set; }
        public int BuilderId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceUrl { get; set; }
        public string Params { get; set; }
        public bool Status { get; set; }
        public string FolderLocation { get; set; }
        public bool InviteFlag { get; set; }
    }
}