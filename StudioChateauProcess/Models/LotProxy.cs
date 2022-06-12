using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudioChateauProcess.Models
{
    [DataContract(Name="LotProxy", Namespace="")]
    class LotProxy
    {
        [DataMember]
        public string lot_no { get; set; }

        [DataMember]
        public string lot_tract { get; set; }

        [DataMember]
        public string lot_address1 { get; set; }

        [DataMember]
        public string lot_address2 { get; set; }

        [DataMember]
        public string lot_city { get; set; }

        [DataMember]
        public string lot_state { get; set; }

        [DataMember]
        public string lot_zip_code { get; set; }

        [DataMember]
        public string lot_home_buyer { get; set;}

        [DataMember]
        public string lot_current_phone { get; set;  }

        [DataMember]
        public string lot_email { get; set; }

        [DataMember]
        public string lot_work_phone {get; set;}

        [DataMember]
        public string lot_cell_phone { get; set; }

        [DataMember]
        public string lot_elevation { get; set; }

        [DataMember]
        public int lot_sequence { get; set; }

        [DataMember]
        public int phase_id { get; set; }

        [DataMember]
        public int plan_id { get; set; }
    }
}
