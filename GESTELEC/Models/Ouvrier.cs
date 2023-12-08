using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GESTELEC.Models
{
    public class Ouvrier
    {
        [Key]
        public string CIN { get; set; }
        [Required]
        public string NomComplet { get; set; }
        [Required]
        public string Ville { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public DateTime DateNaissance { get; set; }
        [Required]
        public DateTime DateDebutActivite { get; set; }
        [Required]
        public string Poste { get; set; }
    }

}