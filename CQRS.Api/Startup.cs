using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using CQRS.Api.Infrastructure;
using CQRS.CommandStack.Infrastructure;
using CQRS.Infrastructure.Persistence;
using CQRS.Infrastructure.Persistence.Consistency;
using CQRS.Infrastructure.Persistence.Mapping;
using CQRS.Infrastructure.Persistence.Repositories;
using CQRS.QueryStack.Infrastructure;
using Newtonsoft.Json;
using Owin;

namespace CQRS.Api
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            var httpConfig = new HttpConfiguration();
            httpConfig.Formatters.Clear();
            httpConfig.Formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            });
            httpConfig.MapHttpAttributeRoutes();
            httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(CreateContainer());

            app.UseWebApi(httpConfig);
        }

        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            #region CommandStack

            builder.RegisterType<AppDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<StashingEventPublisher>().AsSelf().As<IEventPublisher>().InstancePerLifetimeScope();
            builder.RegisterType<AutofacEventBus>().As<IEventBus>().SingleInstance();

            builder.RegisterType<AutofacCommandDispatcher>().As<ICommandDispatcher>();
            builder.RegisterTypes(Assembly.GetAssembly(typeof(IHandle<>))
                    .GetTypes()
                    .Where(x => x.GetInterfaces()
                        .Any(i => i.IsGenericType &&
                                  new[] { typeof(IHandle<>), typeof(IHandle<,>) }
                                      .Contains(i.GetGenericTypeDefinition())))
                    .ToArray())
                .AsImplementedInterfaces();

            builder.RegisterTypes(Assembly.GetAssembly(typeof(Aggregate))
                    .GetTypes()
                    .Where(x => x.Name.EndsWith("Factory"))
                    .ToArray())
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterTypes(Assembly.GetAssembly(typeof(AggregateRepository))
                    .GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(AggregateRepository)))
                    .ToArray())
                .AsImplementedInterfaces();

            builder.RegisterTypes(Assembly.GetAssembly(typeof(PersistenceMapper))
                    .GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(Profile)))
                    .ToArray())
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<PersistenceMapper>().AsSelf()
                .OnPreparing(args =>
                {
                    args.Parameters = new[]
                    {
                        new TypedParameter(typeof(IEnumerable<Profile>),
                            Assembly.GetAssembly(typeof(PersistenceMapper))
                                .GetTypes()
                                .Where(x => x.IsSubclassOf(typeof(Profile)))
                                .Select(x => args.Context.Resolve(x) as Profile))
                    };
                })
                .SingleInstance();

            #endregion

            #region QueryStack

            builder.RegisterTypes(Assembly.GetAssembly(typeof(AppDbContext))
                    .GetTypes()
                    .Where(x => x.GetInterfaces()
                        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandle<>)))
                    .ToArray())
                .AsImplementedInterfaces();

            builder.RegisterTypes(Assembly.GetAssembly(typeof(AppDbContext))
                    .GetTypes()
                    .Where(x => x
                        .GetInterfaces()
                        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuery<,>)))
                    .ToArray())
                .AsImplementedInterfaces();

            #endregion

            builder.RegisterTypes(Assembly.GetAssembly(typeof(ApiControllerBase))
                    .GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(ApiControllerBase)))
                    .ToArray())
                .AsSelf();

            builder.RegisterType<ApiMapper>().AsSelf()
                .WithParameter(new TypedParameter(typeof(IEnumerable<Profile>),
                    Assembly.GetAssembly(typeof(ApiMapper))
                        .GetTypes()
                        .Where(x => x.IsSubclassOf(typeof(Profile)))))
                .SingleInstance();



            return builder.Build();
        }
    }
}