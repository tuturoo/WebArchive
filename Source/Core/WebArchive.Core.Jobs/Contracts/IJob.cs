using System;
using WebArchive.Core.Jobs.Models;

namespace WebArchive.Core.Jobs.Contracts
{
    public interface IJob
    {
        public Guid Identifier { get; }

        public Type Handler { get; }

        public DateTime CreatedAt { get; }

        public DateTime LastUpdatedAt { get; }

        public JobStatus Status { get; }
    }
}
