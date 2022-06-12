using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Exceptions
{
    public class PlanNotFoundException : Exception
    {
         private static string message;

         public PlanNotFoundException(int? planNumber)
             : base(ModifyMessage(planNumber))
        {
        }

         private static string ModifyMessage(int? planNumber)
        {
            message = "Plan number " + planNumber + " is not existing - lot cannot be created";
            return message;
        }
    }
}