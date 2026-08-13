using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Echo.Application.Session;
using Echo.Application.Run;

namespace Echo.Composition
{
    public sealed class AppScopeProbe : IStartable, IDisposable
    {
        private readonly SessionContext _session;
        private readonly IObjectResolver _resolver;

        public AppScopeProbe(SessionContext session, IObjectResolver resolver)
        {
            _session = session;
            _resolver = resolver;
        }

        public void Start()
        {
            Debug.Log($"[App] Область поднята. Сессия {_session.Id}");

            // Проверка изоляции: родитель НЕ должен видеть детей.
            if (_resolver.TryResolve<RunContext>(out _))
                Debug.LogError("[App] ОШИБКА: AppScope видит RunContext. " +
                               "Значит RunContext зарегистрирован не в той области.");
            else
                Debug.Log("[App] Изоляция в порядке: RunContext отсюда не виден");
        }

        public void Dispose() => Debug.Log($"[App] Область уничтожена. Сессия {_session.Id}");
    }
}
