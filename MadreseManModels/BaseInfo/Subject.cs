using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.BaseInfo
{
    [Table("subject")]
    public class Subject
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public int grade_id { get; set; }

        [Required]
        public string teacher_name { get; set; }

    }
}
