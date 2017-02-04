using Autofac;
using CQRS.Api;
using CQRS.CommandStack.Infrastructure;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CompositionRootTests
    {
        private IContainer _container;

        [SetUp]
        public void Setup()
        {
            _container = Startup.CreateContainer();
        }

        [TearDown]
        public void TearDown()
        {
            _container.Dispose();
        }

        [Test]
        public void UnitOfWork_InsideOneScope_ShouldBeSame()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var instance1 = scope.Resolve<IUnitOfWork>();
                var instance2 = scope.Resolve<IUnitOfWork>();

                Assert.IsTrue(instance1 == instance2);
            }
        }

        [Test]
        public void UnitOfWork_ForDifferentScopes_ShouldBeDifferent()
        {
            IUnitOfWork instance1;
            IUnitOfWork instance2;

            using (var scope1 = _container.BeginLifetimeScope())
            {
                instance1 = scope1.Resolve<IUnitOfWork>();

            }

            using (var scope2 = _container.BeginLifetimeScope())
            {
                instance2 = scope2.Resolve<IUnitOfWork>();
            }

            Assert.IsTrue(instance1 != instance2);
        }

        [Test]
        public void UnitOfWork_ForNestedScopes_ShouldBeDifferent()
        {
            IUnitOfWork instance1;
            IUnitOfWork instance2;

            using (var scope1 = _container.BeginLifetimeScope())
            {
                instance1 = scope1.Resolve<IUnitOfWork>();

                using (var scope2 = _container.BeginLifetimeScope())
                {
                    instance2 = scope2.Resolve<IUnitOfWork>();
                }
            }

            Assert.IsTrue(instance1 != instance2);
        }

        [Test]
        public void EventPublisher_InsideOneScope_ShouldBeSame()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var instance1 = scope.Resolve<IEventPublisher>();
                var instance2 = scope.Resolve<IEventPublisher>();

                Assert.IsTrue(instance1 == instance2);
            }
        }

        [Test]
        public void EventPublisher_ForDifferentScopes_ShouldBeDifferent()
        {
            IEventPublisher instance1;
            IEventPublisher instance2;

            using (var scope1 = _container.BeginLifetimeScope())
            {
                instance1 = scope1.Resolve<IEventPublisher>();
            }

            using (var scope2 = _container.BeginLifetimeScope())
            {
                instance2 = scope2.Resolve<IEventPublisher>();
            }

            Assert.IsTrue(instance1 != instance2);
        }

        [Test]
        public void EventPublisher_ForNestedScopes_ShouldBeDifferent()
        {
            IEventPublisher instance1;
            IEventPublisher instance2;

            using (var scope1 = _container.BeginLifetimeScope())
            {
                instance1 = scope1.Resolve<IEventPublisher>();

                using (var scope2 = _container.BeginLifetimeScope())
                {
                    instance2 = scope2.Resolve<IEventPublisher>();
                }
            }

            Assert.IsTrue(instance1 != instance2);
        }
    }
}
