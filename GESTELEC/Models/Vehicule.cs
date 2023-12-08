using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GESTELEC.Models
{
    public class Vehicule
    {
        [Key]
        public string Immatricule { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string TypeCarburant { get; set; }
        [Required]
        public double KilometrageInitial { get; set; }
    }

}