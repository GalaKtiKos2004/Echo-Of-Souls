using UnityEngine;
using VContainer;
using VContainer.Unity;
using Echo.Application.Session;
using Echo.Domain._Project.Domain.Events;

namespace Echo.Composition
{
    public sealed class AppScope : LifetimeScope
    {
        private static AppScope _instance;

        protected override void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.Log("[App] Дубликат AppScope уничтожен");
                Destroy(gameObject);
                return;                    // base.Awake() не вызываем: контейнер не строим
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            base.Awake();                  // здесь VContainer строит контейнер
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SessionContext>(Lifetime.Singleton);
            builder.Register<IEventBus, EventBus>(Lifetime.Singleton);
            builder.RegisterEntryPoint<AppScopeProbe>();
            builder.RegisterEntryPoint<BootEntryPoint>();
        }

        protected override void OnDestroy()
        {
            if (_instance == this) _instance = null;
            base.OnDestroy();
        }
    }
}
