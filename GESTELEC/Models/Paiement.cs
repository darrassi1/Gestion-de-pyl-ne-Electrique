using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GESTELEC.Models
{
    public class Paiement
    {
        [Key]
        public int PaiementId { get; set; }
        [Required]
        public double Montant { get; set; }
        [Required]
        public DateTime DatePaiement { get; set; }
        [ForeignKey("Ouvrier")]
        public string CIN { get; set; }
        public virtual Ouvrier Ouvrier { get; set; }
        [ForeignKey("Pylone")]
        public int PyloneId { get; set; }
        public virtual Pylone Pylone { get; set; }
    }

}