using UnityEngine;
using VContainer;
using VContainer.Unity;
using Echo.Application.Run;

namespace Echo.Composition
{
    public sealed class GameplayScope : LifetimeScope
    {
        protected override void Awake()
        {
            if (Find<AppScope>() != null)
            {
                parentReference = ParentReference.Create<AppScope>();
            }
            else
            {
                Debug.LogError("[Gameplay] AppScope не найден. " +
                               "Запускайтесь через BootScene — " +
                               "иначе родительские сервисы будут недоступны.");
            }

            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<RunContext>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameplayScopeProbe>();
        }
    }
}
