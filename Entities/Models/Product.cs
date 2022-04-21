using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        [Column("ID")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255,ErrorMessage = "Maximum length 255 characters")]
        public string  Name { get; set; } = string.Empty;

        public string?  Description { get; set; }
    }
}
