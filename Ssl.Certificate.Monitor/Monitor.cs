using Ssl.Certificate.Monitor.Interfaces;

namespace Ssl.Certificate.Monitor
{
    internal class Monitor: IMonitor
    {
        private readonly ICertificateService _certificateService;
        public Monitor(
            ICertificateService certificateService) { 
            _certificateService = certificateService;
        }
        public void Run()
        {
            try
            {
                _certificateService.GetSslCertificateDetails();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
