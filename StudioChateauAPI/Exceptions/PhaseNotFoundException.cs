using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Exceptions
{
    public class PhaseNotFoundException : Exception
    {
        private static string message;

        public PhaseNotFoundException(int phaseNumber)
            : base(ModifyMessage(phaseNumber))
        {
        }

        private static string ModifyMessage(int phaseNumber)
        {
            message = "Phase number " + phaseNumber + " is not existing lot cannot be created";
            return message;
        }
    }
}