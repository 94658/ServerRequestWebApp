using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerRequestWebApp.Models
{
    public class Servers
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int ServerID { get; set; }
        public string Server_Name { get; set; }
    }
}