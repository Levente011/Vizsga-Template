using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vizsga.Models
{
    [Table("orders")]
    public class OrderModel
    {
        public int Id { get; set; }
        [Required]
        public CategoryModel Category { get; set; }

        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public bool Bool { get; set; }
    }
}
