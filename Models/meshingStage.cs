using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisApplication.Models
{
    public class meshingStage
    {
        public int ID { get; set; }

        [Display(Name = "User Name")]
        public string userName { get; set; }

        [Display(Name = "Case Name")]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$",
        ErrorMessage = "Special characters are not allowed in the case name.")]
        [StringLength(60, MinimumLength = 3)]
        public string caseName { get; set; }

        [Display(Name = "Input File")]
        public string inputFilename { get; set; }

        [Display(Name = "Upload Date")]
        [DataType(DataType.DateTime)]
        public DateTime uploadDate { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }

        [Display(Name = "Units of model")]
        public string unitModel { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        [Display(Name = "Minimum level of surface refinement")]
        public int refSurfLvlMin { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        [Display(Name = "Maximum level of surface refinement")]
        public int refSurfLvlMax { get; set; }

        [Display(Name = "Level or 1st refinement zone")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        public int refRegLvl1 { get; set; }
        [Display(Name = "Distance of 1st refinement zone")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float refRegDist1 { get; set; }

        [Display(Name = "Level or 2nd refinement zone")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        public int refRegLvl2 { get; set; }
        [Display(Name = "Distance of 2nd refinement zone")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float refRegDist2 { get; set; }

        [Display(Name = "Level or 3rd refinement zone")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        public int refRegLvl3 { get; set; }
        [Display(Name = "Distance of 3rd refinement zone")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float refRegDist3 { get; set; }

        [Display(Name = "Exponential expand ratio")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float expRatio { get; set; }

        [Display(Name = "Number of layers")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        public int numLayers { get; set; }

        [Display(Name = "Final layer thickness")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float finLayerThickness { get; set; }
        [Display(Name = "Min layer thickness")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float minThickness { get; set; }

        [Display(Name = "Date meshed")]
        [DataType(DataType.DateTime)]
        public DateTime meshedDate { get; set; }

    }
}
