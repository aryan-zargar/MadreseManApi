using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreseManModels.Auth
{
    [Table("user")]
    public class User
    {
        [Key]
        public int id { get; set; }

        [NotNull]
        [Required]
        public string username { get; set; }

        [NotNull]
        [Required]
        public string fullName { get; set; }

        [NotNull]
        [Required]
        public string password { get; set; }

        [NotNull]
        [Required]
        public string email { get; set; }

        public int confirmationCode { get; set; }

        public bool isConfirmed { get; set; }

    }
}
