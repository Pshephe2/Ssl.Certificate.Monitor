using Ssl.Certificate.Data.Models;
using Ssl.Certificate.Monitor.Interfaces;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Ssl.Certificate.Monitor.Services
{
    internal class CertificateService: ICertificateService
    {
        private readonly IControlRepository _controlRepository;
        private readonly ISslActivityLogRepository _logRepository;
        private readonly ITcpClientWrapper _tcpClientWrapper;

        public CertificateService(
            IControlRepository controlRepository,
            ISslActivityLogRepository logRepository,
            ITcpClientWrapper tcpClientWrapper
            ) 
        { 
            _controlRepository = controlRepository;
            _logRepository = logRepository;
            _tcpClientWrapper = tcpClientWrapper;
        }

        private IEnumerable<SslControlTable> GetSites()
        {
            var items = _controlRepository.GetAll();
            return items;
        }

        private X509Certificate GetCertificate(string url, int port = 443)
        {
            RemoteCertificateValidationCallback certCallback = (_, _, _, _) => true;
            _tcpClientWrapper.Connect(url, port);
            var cert = _tcpClientWrapper.SendData(url);
            _tcpClientWrapper.Close();
            return cert;
        }

        public void GetSslCertificateDetails()
        {
            foreach ( var item in GetSites())
            {
                var cert = GetCertificate(item.url);
                _logRepository.SaveCertificate(item.id, cert);
                _logRepository.Save();
            }
        }
    }
}
