using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ssl.Certificate.Monitor.Interfaces
{
    public interface ITcpClientWrapper
    {
        void Connect(string hostname, int port);
        X509Certificate SendData(string url);
        void Close();
    }
}
