using UnityEngine;
using VContainer;
using VContainer.Unity;
using Echo.Application.World;

namespace Echo.Composition
{
    public sealed class RegionScope : LifetimeScope
    {
        [SerializeField] private string regionId = "test_region";

        protected override void Awake()
        {
            if (Find<GameplayScope>() != null)
                parentReference = ParentReference.Create<GameplayScope>();
            else
                Debug.LogError("[Region] GameplayScope не найден. " +
                               "Регион грузится поверх прохождения, а не сам по себе.");

            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register(_ => new RegionContext(regionId), Lifetime.Singleton);
            builder.RegisterEntryPoint<RegionScopeProbe>();
        }
    }
}
