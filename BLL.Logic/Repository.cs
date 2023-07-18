using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AutoMapper;
using BLL.Interfaces;
using BLL.Mappings.MapperFactory;
using Common.Delta;
using Common.Interfaces;
using Common.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLL.Logic
{
    public class Repository<TBllEntity, TDalEntity, TKey> : IRepository<TBllEntity, TKey>
        where TKey : struct
        where TBllEntity : class, IEntity<TKey>
        where TDalEntity : class, IEntity<TKey>
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessService<TDalEntity, TKey> _dataAccessService;

        public Repository(IDataAccessService<TDalEntity, TKey> dataAccessService)
        {
            _mapper = MapperFactory.GetMapper();
            _dataAccessService = dataAccessService;
        }

        public async Task<GetWrapper<IEnumerable<TBllEntity>>> GetAsync(
            int? limit = null,
            int? offset = null,
            Expression<Func<TBllEntity, bool>>? filter = null,
          //  Func<IQueryable<TBllEntity>, IOrderedQueryable<TBllEntity>>? orderBy = null,
            IEnumerable<string>? includeProperties = null
        )
        {
            IQueryable<TDalEntity> query = _dataAccessService.Get();
      
            if (includeProperties != null)
            {
                foreach (var includeName in includeProperties)
                {
                    query = query.Include(includeName);
                }
                
            }

            query = query.OrderByDescending(r => r.CreatedAt);

            
            
            if (filter != null)
            {
                var dalFilterExpression = _mapper.Map<Expression<Func<TDalEntity, bool>>>(filter);
                query = query
                    .Where(dalFilterExpression);
            }
            
     
            
            var mappedQuery = query.Select(r=>_mapper.Map<TBllEntity>(r));
            //query.ProjectTo<TBllEntity>(_mapper.ConfigurationProvider);
            
            
            var count = await mappedQuery.CountAsync();
            
            // if(orderBy!= null)
            // {
            //     mappedQuery = orderBy(mappedQuery); //.ToList();
            // }

            if (limit != null)
            {
                mappedQuery = mappedQuery.Take(limit.Value);
            }

            if (offset != null)
            {
                mappedQuery = mappedQuery.Skip(offset.Value);
            }

            var items = await mappedQuery.ToListAsync();
            
            

            return new GetWrapper<IEnumerable<TBllEntity>>(items, count);
        }


        private IEnumerable<string> GetIncludeNames(Expression<Func<TBllEntity, object>>[] includeProperties)
        {
            var memberExpressions = new List<MemberExpression>();
            foreach (var property in includeProperties)
            {
                if (property.Body is MemberExpression memberExpression)
                {
                    memberExpressions.Add(memberExpression);
                }
                else if (property.Body is NewExpression newExpression)
                {
                    memberExpressions.AddRange(newExpression.Arguments.OfType<MemberExpression>());
                }
            }

            return memberExpressions.Select(x => x.Member.Name);
        }

        public async Task<TBllEntity?> GetByIdAsync(TKey id, IEnumerable<string>? includeProperties = null)
        {
            return (await this.GetAsync(
                filter: e => e.Id.Equals(id),
                includeProperties: includeProperties)
            ).Items.FirstOrDefault();
        }

        public async Task<TBllEntity> AddAsync(TBllEntity entity)
        {
            var dalEntity = _mapper.Map<TDalEntity>(entity);
            var addedEntity = await _dataAccessService.AddAsync(dalEntity);
            var addedDto = _mapper.Map<TBllEntity>(addedEntity);
            return addedDto;
        }

        public async Task UpdateRangeAsync(IEnumerable<TBllEntity> items)
        {
            var dals = _mapper.Map<IEnumerable<TDalEntity>>(items);

            await _dataAccessService.UpdateRangeAsync(dals);
        }

        public async Task<TBllEntity> UpdateAsync(TBllEntity entity)
        {
            var dalEntity = _mapper.Map<TDalEntity>(entity);
            dalEntity = await _dataAccessService.UpdateAsync(dalEntity);

            return _mapper.Map<TBllEntity>(dalEntity);
        }

        public async Task<TBllEntity> PatchAsync(TKey id, Delta<TBllEntity> delta)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"Entity with ID {id} not found.");
            }

            delta.Patch(entity);

            var dalEntity = _mapper.Map<TDalEntity>(entity);

            dalEntity = await _dataAccessService.UpdateAsync(dalEntity);

            return _mapper.Map<TBllEntity>(dalEntity);
        }

        public async Task DeleteAsync(TKey id, bool soft = false)
        {
            if (soft)
            {
                var entity = await GetByIdAsync(id);
                
                if (entity is null) throw new Exception($"Entity with id '{id}' not found");
                
                entity.IsDeleted = true;
                
                await UpdateAsync(entity);
            }
            else
            {
                await _dataAccessService.DeleteAsync(id);
            }
        }
    }
}


