using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.complications
{
    [Table("tution_payment")]
    public class TuitionPayment
    {
        [Key]
        public int id { get; set; }

        [Required]
        [ForeignKey("student")]
        public int student_id   { get; set; }

        [Required]
        public int amount { get; set; }

        [Required]
        [ForeignKey("academic_year")]
        public int academic_year_id { get; set; }

        [Required]
        public int month { get; set; }

        [Required]
        public int discount { get; set; }

        [Required]
        public int fine { get; set; }

        [Required]
        public int net_amount { get; set; }

        [Required]
        public char status {  get; set; }

        [Required]
        public DateOnly due { get; set; }

        public long receipt_number  { get; set; }

        [Required]
        public DateOnly date {  get; set; }
        public string description { get; set; }

        [ForeignKey("attachment")]
        public int attachment_id { get; set; }


    }
}
