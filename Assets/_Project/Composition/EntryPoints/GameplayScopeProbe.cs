using System;
using UnityEngine;
using VContainer.Unity;
using Echo.Application.Session;
using Echo.Application.Run;

namespace Echo.Composition
{
    public sealed class GameplayScopeProbe : IStartable, IDisposable
    {
        private readonly SessionContext _session;   // из родительской области
        private readonly RunContext _run;           // из своей

        public GameplayScopeProbe(SessionContext session, RunContext run)
        {
            _session = session;
            _run = run;
        }

        public void Start()
        {
            Debug.Log($"[Gameplay] Область поднята. Прохождение {_run.Id}, " +
                      $"сторона {_run.Alignment}");
            Debug.Log($"[Gameplay] Родитель виден: сессия {_session.Id}");
        }

        public void Dispose() =>
            Debug.Log($"[Gameplay] Область уничтожена. Прохождение {_run.Id}");
    }
}
