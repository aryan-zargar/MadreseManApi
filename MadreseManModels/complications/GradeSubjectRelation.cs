using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.complications
{
    [Table("grade_subject_relation")]
    public class GradeSubjectRelation
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("grade")]
        [Required]
        public int grade_id { get; set; }

        [ForeignKey("subject")]
        [Required]
        public int subject_id { get; set; }
    }
}
