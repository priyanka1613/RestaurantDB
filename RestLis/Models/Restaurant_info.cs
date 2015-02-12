using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace RestLis.Models
{
    public class Restaurant_info
    {
        public int ID{get;set;}

        [Required]
        [DataType(DataType.Text)]
        [Display(Name="Restaurant Name:")]
        public string res_name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Address:")]
        public string res_address { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Area:")]
        public string res_area { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number:")]
        public string res_phone { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Catagory:")]
        public string res_catagory { get; set; }
        
        [DataType(DataType.Url)]
        [Display(Name = "Website:")]
        public string res_website { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Picture_url:")]
        public string res_picurl { get; set; }

    }
}