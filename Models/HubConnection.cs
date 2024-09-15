using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myapp.Models
{
    [Table(nameof(HubConnection))]
    public class HubConnection 
    {
        [Key]
        public int Id { get; set; }
        public string ConnectionId { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
