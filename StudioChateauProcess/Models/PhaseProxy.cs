using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudioChateauProcess.Models
{
    [DataContract(Name="PhaseProxy", Namespace="")]
    class PhaseProxy
    {
        [DataMember]
        public string phase_no { get; set; }

        [DataMember]
        public string phase_hold_orders { get; set; }

        [DataMember]
        public string phase_retail_price_override { get; set; }

        [DataMember]
        public int community_id { get; set; }
    }
}
