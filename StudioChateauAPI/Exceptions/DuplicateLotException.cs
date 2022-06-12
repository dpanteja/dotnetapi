using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Exceptions
{
    public class DuplicateLotException : Exception
    {
         private static string message;

        public DuplicateLotException(int lotNumber)
             : base(ModifyMessage(lotNumber))
        {
        }

        private static string ModifyMessage(int lotNumber)
        {
            message = "Lot " + lotNumber + " is already existing - lot cannot be created";
            return message;
        }
    }
}