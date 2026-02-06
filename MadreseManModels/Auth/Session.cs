using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadreseManModels.Auth
{
    [Table("session")]
    public class Session
    {

        [Key]
        public int id { get; set; }

        [Required]
        [ForeignKey("users")]
        public int UserId { get; set; }

        public string SessionId { get; set; }
    }
}
