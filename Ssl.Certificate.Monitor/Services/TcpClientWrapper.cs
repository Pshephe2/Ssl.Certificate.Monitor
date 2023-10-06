using Ssl.Certificate.Monitor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ssl.Certificate.Monitor.Services
{
    public class TcpClientWrapper : ITcpClientWrapper
    {
        private TcpClient tcpClient;

        public void Connect(string hostname, int port)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(hostname, port);
        }

        public X509Certificate SendData(string url)
        {
            if (tcpClient == null || !tcpClient.Connected)
            {
                throw new InvalidOperationException("TcpClient is not connected.");
            }

            NetworkStream stream = tcpClient.GetStream();
            var sslStream = new SslStream(stream, false);
            sslStream.AuthenticateAsClient(url);
            var serverCertificate = sslStream.RemoteCertificate;
            return serverCertificate!;
        }

        public void Close()
        {
            tcpClient?.Close();
        }
    }
}
