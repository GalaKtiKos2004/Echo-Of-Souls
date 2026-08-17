using System;

namespace Echo.Application.Session
{
    public sealed class SessionContext : IDisposable
    {
        public string Id { get; }
        public DateTime StartedUtc { get; }
        public bool IsDisposed { get; private set; }
        
        public SessionContext()
        {
            Id = Guid.NewGuid().ToString("N").Substring(0, 8);
            StartedUtc = DateTime.UtcNow;
        }
        
        public void Dispose() => IsDisposed = true;
     }
}
}

