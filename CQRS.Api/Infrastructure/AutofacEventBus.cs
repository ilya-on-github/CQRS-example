using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CQRS.CommandStack.Infrastructure;

namespace CQRS.Api.Infrastructure
{
    public class AutofacEventBus : IEventBus
    {
        private readonly ILifetimeScope _scope;

        public AutofacEventBus(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void Publish<TEvent>(TEvent e)
        {
            var handlers = _scope.Resolve<IEnumerable<IHandle<TEvent>>>();
            handlers.AsParallel()
                .ForAll(x => Task.Factory.StartNew(() => x.Handle(e)));
        }
    }
}