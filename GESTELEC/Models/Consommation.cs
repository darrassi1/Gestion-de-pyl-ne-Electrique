using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GESTELEC.Models
{
    public class Consommation
    {
        [Key]
        public int ConsommationId { get; set; }
        [Required]
        public double VolumeGasoil { get; set; }
        [Required]
        public double PrixBon { get; set; }
        [Required]
        public DateTime DateRemplissage { get; set; }
        [Required]
        public double Kilometrage { get; set; }
        [ForeignKey("Vehicule")]
        public string Immatricule { get; set; }
        public virtual Vehicule Vehicule { get; set; }
    }

}