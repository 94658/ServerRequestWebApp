using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServerRequestWebApp.Models
{
    public class UserLogonModel
    {
       
        [Required]
        [Key, ForeignKey("UserProfileModel")]
        public int UserProfileId { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime log_on { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime ? log_out { get; set; }
        public virtual UserProfileModel UserProfileModel { get; set; }
    }
}