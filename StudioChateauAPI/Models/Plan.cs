using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Models
{
    public class Plan
    {
        public Plan()
        {
            plan_desc = "";
            plan_business_rules = "";
            plan_has_catalog = "N";
            plan_external_GUID = null;
            plan_sqft = 0;
            plan_image = "default.jpg";
            msrepl_tran_version = new Guid();
        }
        [Key]
        public int plan_id { get; set; }
        public int plan_seq { get; set; }
        [StringLength(40, ErrorMessage="Plan name cannot be more than 40 characters")]
        [Required]
        public string plan_name { get; set; }
        [StringLength(255, ErrorMessage = "Plan image URL  cannot be more than 255 characters")]
        public string plan_image { get; set; }
        public string plan_desc { get; set; }
        [Required]
        public int community_id { get; set; }
        public string plan_business_rules { get; set; }
        [StringLength(1, ErrorMessage = "Plan has catalog cannot be more than 1 characters")]
        public string plan_has_catalog { get; set; }
        [StringLength(64, ErrorMessage = "Plan external GUID cannot be more than 64 characters")]
        public string plan_external_GUID { get; set; }
        public System.Guid msrepl_tran_version { get; set; }
        public int plan_sqft { get; set; }
        [JsonIgnore]
        public Plan[] plans { get; set; }
    }
}