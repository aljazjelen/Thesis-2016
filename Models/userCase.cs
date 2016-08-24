using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisApplication.Models
{
    public class userCase
    {
        
        // Constructor for class
        // sets default variables to prevent any mistakes earlier in at stage
        public userCase()
        {
            userName = "newuser";
            caseName = "newcase";
            inputFilename = "newfile";


            mesh_refRegLvl1 = 1;
            mesh_refRegLvl2 = 1;
            mesh_refRegLvl3 = 1;

            mesh_refSurfLvlMax = 1;
            mesh_refSurfLvlMin = 1;
            

        }


        // GENERAL INFO
        // FUTURE WORK: implementation of saving objects or use of multiple tables in database

        // Id given by EF
        public int ID { get; set; }

        // Username of user
        [Display(Name = "User Name")]
        public string userName { get; set; }

        // Given name of case
        [Display(Name = "Case Name")]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$",
        ErrorMessage = "Special characters are not allowed in the case name.")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string caseName { get; set; }

        // Name of uploaded file
        [Display(Name = "Input File")]
        public string inputFilename { get; set; }

        // Date at which file was uploaded
        [Display(Name = "Upload Date")]
        [DataType(DataType.DateTime)]
        public DateTime uploadDate { get; set; }

        // Current status of case
        [Display(Name = "Status")]
        public string status { get; set; }

        // Units at which user have modelled a cad model
        [Display(Name = "Units of model")]
        [Required]
        public string unitModel { get; set; }


        //MESHING STAGE
 
        [Display(Name = "Date meshed")]
        [DataType(DataType.DateTime)]
        public DateTime meshedDate { get; set; }

        public string meshStatus { get; set; }
        /*
        Mesh status variable stores status of meshing stage for particular item.
        Available statuses are:
            - initilized (when case gets created and parameters intialized)
            - submitted (when mesh get submitted to meshing stage)
            - obtained (when meshing software finished and mesh is generated for inspection or simulation)
         */

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        [Display(Name = "Minimum level of surface refinement")]
        public int mesh_refSurfLvlMin { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        [Display(Name = "Maximum level of surface refinement")]
        public int mesh_refSurfLvlMax { get; set; }

        [Display(Name = "Level of 1st refinement zone")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        public int mesh_refRegLvl1 { get; set; }
        [Display(Name = "Distance of 1st refinement zone")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float mesh_refRegDist1 { get; set; }

        [Display(Name = "Level of 2nd refinement zone")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        public int mesh_refRegLvl2 { get; set; }
        [Display(Name = "Distance of 2nd refinement zone")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float mesh_refRegDist2 { get; set; }

        [Display(Name = "Level of 3rd refinement zone")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select a natural number")]
        public int mesh_refRegLvl3 { get; set; }
        [Display(Name = "Distance of 3rd refinement zone")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float mesh_refRegDist3 { get; set; }

        [Display(Name = "Exponential expand ratio")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float mesh_expRatio { get; set; }

        // Number of layers is a full number, choices are 0, 1, 2 or 3
        [Display(Name = "Number of layers")]
        [RegularExpression("([0-3]*)", ErrorMessage = "Please select a natural number")]
        public int mesh_numLayers { get; set; }

        [Display(Name = "Final layer thickness")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float mesh_finLayerThickness { get; set; }
        [Display(Name = "Min layer thickness")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float mesh_minThickness { get; set; }





        //  SIMULATION STAGE

        [Display(Name = "Operating Pressure")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_operatingPressure { get; set; }
        [Display(Name = "Operating Temperature")]
        [RegularExpression("^\\d*(\\.[1-9]+)?$", ErrorMessage = "Please insert floating point number bigger than 0")]
        public float visualization_operatingtemperature { get; set; }

        [Display(Name = "Flow Direction - X")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_flowDirectionX { get; set; }
        [Display(Name = "Flow Direction - Y")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_flowDirectionY { get; set; }
        [Display(Name = "Flow Direction - Z")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_flowDirectionZ { get; set; }
        [Display(Name = "Flow Speed")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_flowSpeed { get; set; }


        //  VISUALIZATION STAGE

        // probes
        // Number of probes is a full number, choices are 0, 1, 2 or 3
        [Display(Name = "Number of probes")]
        [RegularExpression("([0-3]*)", ErrorMessage = "Please select a natural number")]
        public int visualization_numProbes { get; set; }

        [Display(Name = "Probe 1 - X")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_probe1x { get; set; }
        [Display(Name = "Probe 1 - Y")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_probe1y { get; set; }
        [Display(Name = "Probe 1 - Z")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_probe1z { get; set; }

        [Display(Name = "Probe 2 - X")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_probe2x { get; set; }
        [Display(Name = "Probe 2 - Y")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_probe2y { get; set; }
        [Display(Name = "Probe 2 - Z")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_probe2z { get; set; }

        [Display(Name = "Probe 3 - X")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_probe3x { get; set; }
        [Display(Name = "Probe 3 - Y")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_probe3y { get; set; }
        [Display(Name = "Probe 3 - Z")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_probe3z { get; set; }

        // cut sections

        // Number of cutting planes is a full number, choices are 0, 1, 2 or 3
        [Display(Name = "Number of cutting planes")]
        [RegularExpression("([0-3]*)", ErrorMessage = "Please select a natural number")]
        public int visualization_numCuts { get; set; }

        [Display(Name = "Point 1 - X")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_point1x { get; set; }
        [Display(Name = "Point 1 - Y")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_point1y { get; set; }
        [Display(Name = "Point 1 - Z")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_point1z { get; set; }

        [Display(Name = "Normal 1 - X")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_normal1x { get; set; }
        [Display(Name = "Normal 1 - Y")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_normal1y { get; set; }
        [Display(Name = "Normal 1 - Z")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_normal1z { get; set; }


        [Display(Name = "Point 2 - X")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_point2x { get; set; }
        [Display(Name = "Point 2 - Y")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_point2y { get; set; }
        [Display(Name = "Point 2 - Z")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_point2z { get; set; }

        [Display(Name = "Normal 2 - X")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_normal2x { get; set; }
        [Display(Name = "Normal 2 - Y")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_normal2y { get; set; }
        [Display(Name = "Normal 2 - Z")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_normal2z { get; set; }


        [Display(Name = "Point 3 - X")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_point3x { get; set; }
        [Display(Name = "Point 3 - Y")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_point3y { get; set; }
        [Display(Name = "Point 3 - Z")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_point3z { get; set; }

        [Display(Name = "Normal 3 - X")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_normal3x { get; set; }
        [Display(Name = "Normal 3 - Y")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_normal3y { get; set; }
        [Display(Name = "Normal 3 - Z")]
        [RegularExpression("^\\d*(\\.[0-9]+)?$", ErrorMessage = "Please insert floating point number")]
        public float visualization_normal3z { get; set; }


    }
}
