using System;

namespace WebArchive.Core.Settings.Contracts
{
    public interface IJobSettings
    {
        public int MaxRunningJobs { get; set; }
    }
}
