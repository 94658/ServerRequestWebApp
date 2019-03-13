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

        [Required]
        public string target_server { get; set; }

        [Required]
        public string notes { get; set; }

        //[DataType(DataType.DateTime)]
        //public DateTime access_start { get; set; }

        //[DataType(DataType.DateTime)]
        //public DateTime access_end { get; set; }

        //public int period { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime access_start { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime access_end { get; set; }


        //Time and who sent the request
        public string created_by { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime created_on { get; set; }

        //Approvers and time approved
        public string supervised_by { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime ? supervised_on { get; set; }

        public string approved_by { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime ? approved_on { get; set; }
         
        public bool isApproved { get; set; }
        public bool isPending { get; set; }
        public bool isSupervised { get; set; }
        
        //isSupervised
        //isApproved
    }
}