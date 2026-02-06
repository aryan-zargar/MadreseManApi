using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.BaseInfo
{
    [Table("grade")]
    public class Grade
    {
        [Key]
        public int id { get; set; }

        public string grade_name { get; set; }
    }
}
