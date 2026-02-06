using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.BaseInfo
{
    [Table("class")]
    public class Class
    {
        [Key]
        public int id {  get; set; }

        [Required]
        public string class_name { get; set; }

        [ForeignKey("grade")]
        public int grade_id { get; set; }
    }
}
