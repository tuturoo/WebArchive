using System;

namespace WebArchive.Core.Jobs.Services
{
    public interface IJobHandler
    {
        public Task StartAsync();

        public Task StopAsync();
    }
}
