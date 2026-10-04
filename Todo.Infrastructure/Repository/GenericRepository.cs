using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.RepositoryInterfaces;
using Todo.Infrastructure.Persistence.Entities;

namespace Todo.Infrastructure.Repository
{
    public class GenericRepository<TDomain, TEntity> :
        IGenericRepository<TDomain>
        where TDomain : class
        where TEntity : class
    {
        private readonly TodoAppDbContext todoAppDbContext;
        private readonly IMapper mapper;

        public GenericRepository(TodoAppDbContext todoAppDbContext,
            IMapper mapper)
        {
            this.todoAppDbContext = todoAppDbContext;
            this.mapper = mapper;
        }

        public TodoAppDbContext TodoAppDbContext { get; }

        public async Task AddAsync(TDomain domain)
        {
            var entity = mapper.Map<TEntity>(domain);
            await todoAppDbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<int> CommitAsync()
        {
            return await todoAppDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            return await todoAppDbContext.Set<TEntity>().ProjectTo<TDomain>(mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TDomain> GetByIdAsync(object id)
        {
            var entity = await todoAppDbContext.Set<TEntity>().FindAsync(id); // ID should by PK of the entity

            return entity == null ? null : mapper.Map<TDomain>(entity);
        }
    }
}
