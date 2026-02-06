using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.complications
{
    [Table("budget_transaction")]
    public class BudgetTransaction
    {
        [Key]
        public int id { get; set; }

        [Required]
        public bool is_deposit { get; set; }

        public DateOnly date {  get; set; }

        [Required]
        public int budget_id { get; set; }

        [Required]
        public int transaction_amount { get; set; }

    }
}
