using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudioChateauProcess.Models
{
    [CollectionDataContract(Name = "Lots", Namespace = "")]
    class Lots : List<Lot>
    {
    }
}
