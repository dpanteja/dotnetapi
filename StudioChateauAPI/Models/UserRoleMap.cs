using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudioChateauAPI.Models
{
    public class AspNetUserRole
    {
        [Column (Order=0), Key]
        public string UserId { get; set; }
        [Column(Order = 1), Key]
        public string RoleId { get; set; }
    }
}