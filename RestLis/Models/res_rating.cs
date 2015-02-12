using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestLis.Models
{
    public class res_rating
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Restaurant Name:")]
        public string res_name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date:")]
        public string date { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Customer Name:")]
        public string cus_name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Ratings:")]
        public double ratings { get; set; }

        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments:")]
        public string comment { get; set; }
    }
}