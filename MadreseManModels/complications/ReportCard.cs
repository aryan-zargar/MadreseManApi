using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.complications
{
    [Table("report_card")]
    public class ReportCard
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        [ForeignKey("class")]
        public int class_id { get; set;}

        [ForeignKey("academic_year")]
        [Required]
        public int academic_year_id { get; set; }

    }
}
