using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.complications
{
    [Table("budget")]
    public class Budget
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string budget_name { get; set; }

        [Required]
        public string budget_description { get; set; }

        [Required]
        public int budget_amount { get; set; }

        public string color_code { get; set; }
    }
}
