using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.complications
{
    [Table("report_card_entry")]
    public class ReportCardEntry
    {
        [Key]
        public int id { get; set; }

        [Required]
        [ForeignKey("report_card")]
        public int report_card_id { get; set; }

        [Required]
        [ForeignKey("subject")]
        public int subject_id { get; set; }

        [Required]
        public int score { get; set; }

        [Required]
        [ForeignKey("student")]
        public int student_id { get; set; }
    }
}
