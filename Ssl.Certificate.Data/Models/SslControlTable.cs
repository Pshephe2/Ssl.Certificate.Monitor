using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ssl.Certificate.Data.Models
{
    [Table(name:"ssl_control_table")]
    public class SslControlTable
    {
        public SslControlTable() { }
        [Key]
        public int id { get; set; }
        public string url { get; set; }
        [ForeignKey("control_id")]
        public virtual ICollection<SslActivityLog> SslActivityLog { get; set; }
    }
}
