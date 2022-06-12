using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudioChateauProcess.Models
{
    [DataContract(Name="PlanProxy", Namespace="")]
    class PlanProxy
    {
        [DataMember]
        public string plan_name { get; set; }

        [DataMember]
        public int community_id { get; set; }

        [DataMember]
        public int plan_sqft { get; set; }

        [DataMember]
        public int plan_seq { get; set; }
    }
}
