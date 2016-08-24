using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisApplication.Models
{
    public class userCaseParam
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string modelName { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")]
        public int metricConversion { get; set; }
        public string caseDirectory { get; set; }

        [Display(Name = "Case Name")]
        public string caseName { get; set; }

        [Display(Name = "Upload Date")]
        [DataType(DataType.Date)]
        public DateTime uploadDate { get; set; }

        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}
