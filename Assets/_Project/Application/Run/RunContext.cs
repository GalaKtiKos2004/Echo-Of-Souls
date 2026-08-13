using System;

namespace Echo.Application.Run
{
    public enum Alignment
    {
        Light,
        Dark
    }

    public class RunContext : IDisposable
    {
        public string Id { get; }
        public Alignment Alignment { get; private set; }
        public bool IsDisposed { get; private set; }

        public RunContext()
        {
            Id = Guid.NewGuid().ToString("N").Substring(0, 8);
            Alignment = Alignment.Light;
        }

        public void SwitchToDark() => Alignment = Alignment.Dark;

        public void Dispose() => IsDisposed = true;
    }
}
