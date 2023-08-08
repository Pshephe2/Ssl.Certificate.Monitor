using System.ComponentModel.DataAnnotations.Schema;

namespace Ssl.Certificate.Data.Models
{
    [Table(name: "ssl_activity_log")]
    public class SslActivityLog
    {
        public int id { get; set; }
        public int control_id { get; set; }
        public DateTime run_time { get; set; }
        public DateTime expires_on { get; set; }
        public DateTime created_on { get; set; }
        public virtual SslControlTable SslControlTable { get; set; }
    }
}
