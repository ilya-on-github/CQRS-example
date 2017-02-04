using System.Collections.Generic;
using AutoMapper;

namespace CQRS.Infrastructure.Persistence.Mapping
{
    public class PersistenceMapper
    {
        private readonly IMapper _autoMapper;

        public PersistenceMapper(IEnumerable<Profile> profiles)
        {
            _autoMapper = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            }).CreateMapper();
        }

        public TTarget Map<TSource, TTarget>(TSource source)
        {
            return _autoMapper.Map<TSource, TTarget>(source);
        }
    }
}