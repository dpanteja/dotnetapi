using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Models
{
    public class UserRoleModel
    {
        public string userId { get; set; }
        public int[] builderId { get; set; }
    }
}