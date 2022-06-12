using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudioChateauAPI.Models
{

    public class Phase
    {

        public Phase()
        {
            phase_desc = "";
            phase_admin_email = "";
            phase_retail_price_override = "N";
            phase_external_GUID = null;
            external_exports = false;
            msrepl_tran_version = new Guid();
        }
        [Key]
        public int phase_id { get; set; }
        [StringLength(40, ErrorMessage = "Phase no cannot be more than 40 characters")]
        [Required]
        public string phase_no { get; set; }
        [Required]
        public int community_id { get; set; }
        public string phase_desc { get; set; }
        [StringLength(1, ErrorMessage = "Phase hold orders cannot be more than 1 characters")]
        public string phase_hold_orders { get; set; }
        [StringLength(64, ErrorMessage = "Phase admin email cannot be more than 64 characters")]
        public string phase_admin_email { get; set; }
        [StringLength(10, ErrorMessage = "Phase retail price override cannot be more than 10 characters")]
        public string phase_retail_price_override { get; set; }
        [StringLength(64, ErrorMessage = "Phase external GUID cannot be more than 64 characters")]
        public string phase_external_GUID { get; set; }
        public System.Guid msrepl_tran_version { get; set; }
        public bool external_exports { get; set; }
        [JsonIgnore]
        public Phase[] phases { get; set; }
    }
}