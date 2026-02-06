using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.BaseInfo
{
    [Table("academic_year")]
    public class AcademicYear
    {
        [Key]
        public int id { get; set; }

        public string title { get; set; }

        public bool active { get; set; }

    }
}
