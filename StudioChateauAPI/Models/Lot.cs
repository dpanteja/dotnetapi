using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Models
{
    public partial class Lot
    {
        public Lot()
        {
            lot_tract = "";
            lot_address1 = "";
            lot_address2 = "";
            lot_city = "";
            lot_state = "";
            lot_zip_code = "";
            lot_home_buyer = "";
            lot_current_address1 = "";
            lot_current_address2 = "";
            lot_current_city = "";
            lot_current_state = "";
            lot_current_zip = "";
            lot_current_phone = "";
            lot_contact = "";
            lot_add_to_loan = "";
            lot_email = "";
            lot_last_seq = 0;
            lot_work_phone = "";
            lot_cell_phone = "";
            lot_custom1 = null;
            lot_custom2 = null;
            lot_custom3 = null;
            lot_custom4 = null;
            lot_custom5 = null;
            lot_custom6 = null;
            lot_custom7 = null;
            lot_custom8 = null;
            lot_custom9 = null;
            lot_custom10 = null;
            lot_loan_rate = 0;
            lot_loan_price = 0;
            lot_loan_periods = 360;
            lot_loan_down_payment = 0;
            lot_home_buyer2 = "";
            lot_contact2 = "";
            lot_current_address12 = "";
            lot_current_address22 = "";
            lot_current_city2 = "";
            lot_current_state2 = "";
            lot_current_zip2 = "";
            lot_current_phone2 = "";
            lot_work_phone2 = "";
            lot_cell_phone2 = "";
            lot_email2 = "";
            lot_elevation = "";
            lot_external_GUID = "";
            lot_notes = "";
            lot_designer_id = 0;
            lot_has_wished = false;
            lot_is_billed = 0;
            build_sequence = "";
            build_sequence_reference = "";
            lot_identifier = new Guid();
            lot_base_price = 0;
            lot_premium = 0;
            lot_boa_notes = "";
            lot_diary = "";
            disable_dcs_email_flag = true;
            

        }
        [Key]
        public int lot_id { get; set; }
        [Required]
        public string lot_no { get; set; }
        public string lot_tract { get; set; }
        public string lot_address1 { get; set; }
        public string lot_address2 { get; set; }
        public string lot_city { get; set; }
        public string lot_state { get; set; }
        public string lot_zip_code { get; set; }
        public string lot_home_buyer { get; set; }
        public string lot_current_address1 { get; set; }
        public string lot_current_address2 { get; set; }
        public string lot_current_city { get; set; }
        public string lot_current_state { get; set; }
        public string lot_current_zip { get; set; }
        public string lot_current_phone { get; set; }
        public string lot_contact { get; set; }
        public string lot_add_to_loan { get; set; }
        public string lot_email { get; set; }
        [Required]
        public int phase_id { get; set; }
        [Required]
        public Nullable<int> plan_id { get; set; }
        public Nullable<int> lot_last_seq { get; set; }
        public string lot_work_phone { get; set; }
        public string lot_cell_phone { get; set; }
        public string lot_custom1 { get; set; }
        public string lot_custom2 { get; set; }
        public string lot_custom3 { get; set; }
        public string lot_custom4 { get; set; }
        public string lot_custom5 { get; set; }
        public string lot_custom6 { get; set; }
        public string lot_custom7 { get; set; }
        public string lot_custom8 { get; set; }
        public string lot_custom9 { get; set; }
        public string lot_custom10 { get; set; }
        public Nullable<double> lot_loan_rate { get; set; }
        public Nullable<decimal> lot_loan_price { get; set; }
        public Nullable<int> lot_loan_periods { get; set; }
        public Nullable<decimal> lot_loan_down_payment { get; set; }
        public string lot_home_buyer2 { get; set; }
        public string lot_contact2 { get; set; }
        public string lot_current_address12 { get; set; }
        public string lot_current_address22 { get; set; }
        public string lot_current_city2 { get; set; }
        public string lot_current_state2 { get; set; }
        public string lot_current_zip2 { get; set; }
        public string lot_current_phone2 { get; set; }
        public string lot_work_phone2 { get; set; }
        public string lot_cell_phone2 { get; set; }
        public string lot_email2 { get; set; }
        public string lot_elevation { get; set; }
        public string lot_external_GUID { get; set; }
        public string lot_notes { get; set; }
        public int lot_designer_id { get; set; }
        public System.Guid msrepl_tran_version { get; set; }
        public bool lot_has_wished { get; set; }
        public int lot_is_billed { get; set; }
        public string build_sequence { get; set; }
        public string build_sequence_reference { get; set; }
        public Nullable<int> lot_sequence { get; set; }
        public System.Guid lot_identifier { get; set; }
        public Nullable<decimal> lot_base_price { get; set; }
        public Nullable<decimal> lot_premium { get; set; }
        public string lot_boa_notes { get; set; }
        public string lot_diary { get; set; }
        public bool disable_dcs_email_flag { get; set; }
        [JsonIgnore]
        public Lot[] lots { get; set; }
    }
}