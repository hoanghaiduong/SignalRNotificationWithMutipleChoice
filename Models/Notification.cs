using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace myapp.Models
{
    [Table(nameof(Notification))]
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string MessageType { get; set; } = null!;

        public DateTime? NotificationDateTime { get; set; } = DateTime.Now;
    }
}
