using Ssl.Certificate.Data.Models;
using System.Security.Cryptography.X509Certificates;

namespace Ssl.Certificate.Monitor.Interfaces
{
    internal interface ISslActivityLogRepository: IRepository<SslActivityLog>
    {
        void SaveCertificate(int control_id, X509Certificate cert);
    }
}
