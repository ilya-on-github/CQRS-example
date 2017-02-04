using System.Reflection;
using System.Web.Http;
using AutoMapper;
using CQRS.CommandStack.Infrastructure;

namespace CQRS.Api.Infrastructure
{
    public class ApiControllerBase : ApiController
    {
        protected readonly ICommandDispatcher CommandDispatcher;
        protected readonly ApiMapper Mapper;

        public ApiControllerBase(ICommandDispatcher commandDispatcher, ApiMapper mapper)
        {
            CommandDispatcher = commandDispatcher;
            Mapper = mapper;
        }
    }

    public class ApiMapper
    {
        private readonly IMapper _mapper;

        public ApiMapper()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(Assembly.GetAssembly(GetType()));

            }).CreateMapper();
        }

        public TOut Map<TIn, TOut>(TIn source)
        {
            return _mapper.Map<TIn, TOut>(source);
        }
    }
}