using Ssl.Certificate.Data;
using Ssl.Certificate.Data.Models;
using Ssl.Certificate.Monitor.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Ssl.Certificate.Monitor.Repository
{
    internal class SslActivityLogRepository : Repository<SslActivityLog>, ISslActivityLogRepository
    {
        public SslActivityLogRepository(MonitorDbContext dbContext)
            :base(dbContext) { }

        public void SaveCertificate(int control_id, X509Certificate cert)
        {
            var logEntry = new SslActivityLog()
            {
                control_id = control_id,
                run_time = DateTime.UtcNow,
                expires_on = DateTime.Parse(cert.GetExpirationDateString()).ToUniversalTime(),
                created_on = DateTime.Parse(cert.GetEffectiveDateString()).ToUniversalTime()
            };
            Insert(logEntry);
        }
    }
}
