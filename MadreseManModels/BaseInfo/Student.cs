using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.BaseInfo
{
    [Table("student")]
    public class Student
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string lastname { get; set; }

        [Required]
        public string national_id { get; set; }

        [Required]
        public DateTime birth_date { get; set; }

        [ForeignKey("grade")]
        public int grade_id { get; set; }

        [ForeignKey("class")]
        public int class_id { get; set; }
    }
}
