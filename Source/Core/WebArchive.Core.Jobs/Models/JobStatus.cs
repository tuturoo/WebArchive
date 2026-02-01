using System;

namespace WebArchive.Core.Jobs.Models
{
    public enum JobStatus : byte
    {
        Scheduled,
        InProgress,
        SuccessFinished,
        Failed,
        Cancellation
    }
}
