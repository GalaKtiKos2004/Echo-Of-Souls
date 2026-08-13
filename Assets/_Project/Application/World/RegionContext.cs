using System;

namespace Echo.Application.World
{
    /// Живёт один регион. Уничтожается при выходе через портал.
    public sealed class RegionContext : IDisposable
    {
        public string RegionId { get; }
        public bool IsDisposed { get; private set; }

        public RegionContext(string regionId) => RegionId = regionId;

        public void Dispose() => IsDisposed = true;
    }
}
