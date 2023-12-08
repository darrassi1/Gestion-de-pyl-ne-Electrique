using System;
using System.ComponentModel.DataAnnotations;

namespace GESTELEC.Models
{
    public class Work
    {
        [Key]
        public int WorkId { get; set; }

        [Required]
        public string OuvrierCIN { get; set; }

        [Required]
        public string PyloneNumero { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        // Other properties as needed

        // Navigation properties
        public virtual Ouvrier Ouvrier { get; set; }
        public virtual Pylone Pylone { get; set; }
    }
}
