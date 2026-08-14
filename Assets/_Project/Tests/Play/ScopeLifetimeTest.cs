using VContainer;
using VContainer.Unity;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Echo.Application.Run;
using Echo.Application.Session;

namespace Echo.Tests.Play._Project.Tests.Play
{
    public class ScopeLifetimeTest
    {
        private LifetimeScope _app;

        [SetUp]
        public void Setup()
        {
            _app = LifetimeScope.Create(b => b.Register<SessionContext>(Lifetime.Singleton));
        }

        [TearDown]
        public void TearDown()
        {
            if (_app != null)
            {
                Object.DestroyImmediate(_app.gameObject);
            }
        }

        [Test]
        public void Child_Resolves_ParentRegistration()
        {
            var gameplay = _app.CreateChild(b =>
                b.Register<RunContext>(Lifetime.Singleton));
            
            Assert.AreSame(_app.Container.Resolve<SessionContext>(),
                gameplay.Container.Resolve<SessionContext>());
        }

        [Test]
        public void Parent_DoesNotSee_ChildRegistration()
        {
            _app.CreateChild(b => b.Register<RunContext>(Lifetime.Singleton));
            
            Assert.Throws<VContainerException>(() => _app.Container.Resolve<RunContext>());
        }

        [UnityTest]
        public IEnumerator DestroyingGameplay_KeepsSession_DisposesRun()
        {
            var session = _app.Container.Resolve<SessionContext>();
            var gameplay = _app.CreateChild(b =>
                b.Register<RunContext>(Lifetime.Singleton));
            var run = gameplay.Container.Resolve<RunContext>();
            
            Object.Destroy(gameplay.gameObject);
            yield return null;
            
            Assert.IsTrue(run.IsDisposed, "RunContext пережил свою область");
            Assert.AreSame(session, _app.Container.Resolve<SessionContext>(), "SessionContext не должен пересоздаваться");
        }
    }
}
