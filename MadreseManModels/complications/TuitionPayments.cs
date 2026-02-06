using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.complications
{
    [Table("tution_payments")]
    public class TuitionPayments
    {
        [Key]
        public int payment_id { get; set; }

        [Required]
        [ForeignKey("student")]
        public int student_id   { get; set; }

        [Required]
        public int amount_paid { get; set; }

        [Required]
        public DateTime payment_date {  get; set; }

        [Required] 
        public string description { get; set; }

        [Required]
        public int attachment_id { get; set; }


    }
}
