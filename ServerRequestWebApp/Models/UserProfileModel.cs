using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerRequestWebApp.Models
{
    public class UserProfileModel
    {
       
        [Key]
        public int UserProfileId { get; set; }
        public string UserName { get; set; }

        [Required, AllowHtml]
        [EmailAddress]
        [RegularExpression("[a-zA-Z0-9._%+-]+@craftsilicon.com", ErrorMessage ="Enter your craft silicon email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required, AllowHtml ]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }
        //public char Gender { get; set; }
       
        public string Department { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime CreatedOn { get; set; } 
        public bool  isAdmin { get; set; }
        public string Gender { get; set; }
        public virtual UserLogonModel UserLogons { get; set; }

    }
}