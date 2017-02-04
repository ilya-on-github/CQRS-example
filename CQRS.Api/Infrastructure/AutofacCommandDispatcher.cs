using Autofac;
using CQRS.CommandStack.Infrastructure;

namespace CQRS.Api.Infrastructure
{
    public class AutofacCommandDispatcher : ICommandDispatcher
    {
        private readonly ILifetimeScope _scope;

        public AutofacCommandDispatcher(ILifetimeScope scope)
        {
            _scope = scope;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Выгоды:
        /// - единая точка входа для обработки команд;
        /// - IUnitOfWork 'PerLifetimeContext' гарантирует отсутствие интерференции при параллельной обработке нескольких команд;
        /// - код обработчика выполняет обработку, не выполняя самостоятельно открытии и фиксацию транзакции.
        /// </remarks>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="command"></param>
        public void Handle<TCommand>(TCommand command)
        {
            using (var scope = _scope.BeginLifetimeScope())
            {
                _scope.Resolve<IHandle<TCommand>>()
                    .Handle(command);

                scope.Resolve<IUnitOfWork>().Commit();
            }
        }

        public TResult Handle<TCommand, TResult>(TCommand command)
        {
            using (var scope = _scope.BeginLifetimeScope())
            {
                var result = scope.Resolve<IHandle<TCommand, TResult>>()
                    .Handle(command);

                scope.Resolve<IUnitOfWork>().Commit();

                return result;
            }
        }
    }
}