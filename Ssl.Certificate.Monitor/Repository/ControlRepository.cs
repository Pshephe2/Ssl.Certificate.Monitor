using Ssl.Certificate.Data;
using Ssl.Certificate.Data.Models;
using Ssl.Certificate.Monitor.Interfaces;

namespace Ssl.Certificate.Monitor.Repository
{
    internal class ControlRepository: Repository<SslControlTable>, IControlRepository
    {
        public ControlRepository(MonitorDbContext dbContext)
            : base(dbContext) { }
    }
}
