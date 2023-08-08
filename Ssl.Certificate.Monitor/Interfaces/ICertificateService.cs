using System.Security.Cryptography.X509Certificates;

namespace Ssl.Certificate.Monitor.Interfaces
{
    internal interface ICertificateService
    {
        void GetSslCertificateDetails();
    }
}
