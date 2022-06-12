using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Models
{
    public class Builders
    {
        [Key]
        public int builder_id { get; set; }
        public string builder_name { get; set; }
        public string builder_image { get; set; }
        public string builder_desc { get; set; }
        public string builder_email { get; set; }
        public string builder_agreement_title { get; set; }
        public string community_default_disclaimer { get; set; }
        public bool use_oms_externals { get; set; }
        public Guid msrepl_tran_version { get; set; }
        public string builder_email_export { get; set; }
        public string builder_info { get; set; }
        public string community_disclaimer1 { get; set; }
        public string community_disclaimer2 { get; set; }
        public string community_disclaimer3 { get; set; }
        public string community_price_request_disclaimer { get; set; }
        public string community_vendor_notice_disclaimer { get; set; }
        public string community_email_disclaimer { get; set; }
        public string option_order_disclaimer { get; set; }
        public bool alternate_salestax_flag { get; set; }
        public string vendor_review_disclaimer { get; set; }
        public string DCSPublicKey { get; set; }
        public string DCSPrivateKey { get; set; }
        public string finalization_form_info { get; set; }
        public string finalization_form_footer { get; set; }
    }
}