using System;
using UnityEngine;
using VContainer.Unity;
using Echo.Application.Session;
using Echo.Application.Run;
using Echo.Application.World;

namespace Echo.Composition
{
    public sealed class RegionScopeProbe : IStartable, IDisposable
    {
        private readonly SessionContext _session;   // через две области вверх
        private readonly RunContext _run;           // через одну
        private readonly RegionContext _region;     // своя

        public RegionScopeProbe(SessionContext session, RunContext run, RegionContext region)
        {
            _session = session;
            _run = run;
            _region = region;
        }

        public void Start() =>
            Debug.Log($"[Region] Область поднята: {_region.RegionId}. " +
                      $"Видит прохождение {_run.Id} и сессию {_session.Id}");

        public void Dispose() =>
            Debug.Log($"[Region] Область уничтожена: {_region.RegionId}");
    }
}
