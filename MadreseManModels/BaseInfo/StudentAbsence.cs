using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.BaseInfo
{
    [Table("student_absence")]
    public class StudentAbsence
    {
        [Key]
        public int id { get; set; }

        [Required]
        [ForeignKey("student")]
        public int student_id { get; set; }

        [Required]
        public DateTime absence_date { get; set; }

    }
}
