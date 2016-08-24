using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisApplication.Models
{
    public class fileUpload
    {
        public int ID { get; set; }

        [Display(Name = "User Name")]
        public string userName { get; set; }

        [Display(Name = "Case Name")]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$",
        ErrorMessage = "Special characters are not allowed in the case name.")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string caseName { get; set; }

        [Display(Name = "Input File")]
        public string inputFilename { get; set; }

        [Display(Name = "Upload Date")]
        [DataType(DataType.DateTime)]
        public DateTime uploadDate { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }

        [Display(Name = "Units of model")]
        [Required]
        public string unitModel { get; set; }


  
        /*                                                                  TODO
        [Required(ErrorMessage = "Please Upload a Valid Image File")]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload Product Image")]
        [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
        public IFormFile ProductImage { get; set; }
        */
    }
}




