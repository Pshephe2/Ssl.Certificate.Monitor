using Ssl.Certificate.Data;
using Ssl.Certificate.Monitor.Interfaces;

namespace Ssl.Certificate.Monitor
{
    internal class Monitor: IMonitor
    {
        private readonly MonitorDbContext _dbContext;
        private readonly ICertificateService _certificateService;
        public Monitor(
            MonitorDbContext context,
            ICertificateService certificateService) { 
            _dbContext = context;
            _certificateService = certificateService;
        }
        public void Run()
        {
            _certificateService.GetSslCertificateDetails();
        }
    }
}
