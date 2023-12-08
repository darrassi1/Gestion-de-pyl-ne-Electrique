using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GESTELEC.Models
{
    public class Pylone
    {
        [Key]
        public int PyloneId { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string LigneElectrique { get; set; }
        [Required]
        public string Ville { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public string EtatDegradation { get; set; }
    }

}