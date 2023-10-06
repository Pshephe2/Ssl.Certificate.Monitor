using System.Security.Cryptography.X509Certificates;

namespace Ssl.Certificate.Monitor.Interfaces
{
    public interface ITcpClientWrapper
    {
        void Connect(string hostname, int port);
        X509Certificate SendData(string url);
        void Close();
    }
}
