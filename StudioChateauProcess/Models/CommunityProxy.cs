using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudioChateauProcess.Models
{
    [DataContract(Name="CommunityProxy", Namespace="")]
    class CommunityProxy
    {
        [DataMember]
        public int Builder_Id { get; set; }

        [DataMember]
        public string Community_Name { get; set; }

        [DataMember]
        public string Community_External_GUID { get; set; }
    }
}
