using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.complications
{
    [Table("attachment")]
    public class Attachment
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string data { get; set; }
    }
}
