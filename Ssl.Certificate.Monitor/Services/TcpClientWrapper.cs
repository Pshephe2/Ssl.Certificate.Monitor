using Ssl.Certificate.Monitor.Interfaces;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Ssl.Certificate.Monitor.Services
{
    public class TcpClientWrapper : ITcpClientWrapper
    {
        private TcpClient? _tcpClient;
        public void Connect(string hostname, int port)
        {
            _tcpClient = new TcpClient();
            _tcpClient.Connect(hostname, port);
        }

        public X509Certificate SendData(string url)
        {
            if (_tcpClient is not { Connected: true })
            {
                throw new InvalidOperationException("TcpClient is not connected.");
            }

            var stream = _tcpClient.GetStream();
            var sslStream = new SslStream(stream, false);
            sslStream.AuthenticateAsClient(url);
            var serverCertificate = sslStream.RemoteCertificate;
            return serverCertificate!;
        }

        public void Close()
        {
            _tcpClient?.Close();
        }
    }
}
