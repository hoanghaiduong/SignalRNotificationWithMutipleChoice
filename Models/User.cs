using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myapp.Models
{
    [Table(nameof(User))]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string UserName { get; set; } 
        public required string Password { get; set; }  
        public string Dept { get; set; } = string.Empty;
    }
}
