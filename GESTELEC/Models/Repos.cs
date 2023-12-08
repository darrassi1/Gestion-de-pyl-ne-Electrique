using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GESTELEC.Models
{
    public class Repos
    {
        [Key]
        public int ReposId { get; set; }
        [Required]
        public DateTime DateRepos { get; set; }
        [Required]
        public string MotifRepos { get; set; }
        [ForeignKey("Ouvrier")]
        public string CIN { get; set; }
        public virtual Ouvrier Ouvrier { get; set; }
    }

}