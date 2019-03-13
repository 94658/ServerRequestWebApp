using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServerRequestWebApp.Models
{
    public class AdminModel
    {
        [Key]
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public bool Approver { get; set; }
    }
}