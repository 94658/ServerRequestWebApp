using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServerRequestWebApp.Models
{
    public class ServerAccessModel
    {
        [Key]
        public int ID { get; set; }
        public string target_server { get; set; }
        public int period { get; set; }
        public string notes { get; set; }

        //Time and who sent the request
        public string created_by { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime created_on { get; set; }

        //Approvers and time approved
        public string supervised_by { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime supervised_on { get; set; }
        public string approved_by { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime approved_on { get; set; }
    }
}