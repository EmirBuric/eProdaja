using Azure.Core;
using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class BaseService<TModel,TSearch,TDbEntity,TInsert,TUpdate>:IService<TModel, TSearch,TInsert,TUpdate> 
        where TSearch:BaseSearchObject 
        where TDbEntity:class, new() 
        where TModel : class
        where TInsert:BaseInsertRequest
        where TUpdate :BaseUpdateRequest
    {
        public EProdajaContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public BaseService(EProdajaContext context, IMapper mapper) 
        {
            Context = context;
            Mapper = mapper;
        }

        public PagedResults<TModel> GetPaged(TSearch search)
        {
            List<TModel> result = new List<TModel>();
            var query = Context.Set<TDbEntity>().AsQueryable();

            query=AddFilter(search, query);

            int count= query.Count();
            
            if (search?.Page.HasValue == true && search?.PageSize.HasValue == true)
            {
                query = query.Skip(search.Page.Value * search.PageSize.Value).Take(search.PageSize.Value);
            }

            var list = query.ToList();
            result = Mapper.Map(list, result);
            PagedResults<TModel> pagedResults = new PagedResults<TModel>();
            pagedResults.ResultsList=result;
            pagedResults.count=count;
            return pagedResults;
        }
        public virtual IQueryable<TDbEntity> AddFilter(TSearch search,IQueryable<TDbEntity> query)
        {
            return query;
        }

        public TModel GetById(int id)
        {
            var entity=Context.Set<TDbEntity>().Find(id);
            if(entity!=null)
            {
                return Mapper.Map<TModel>(entity);
            }
            else 
            {
                return null;
            }
            
        }

        public virtual TModel Insert(TInsert insert)
        {
            TDbEntity entity = new TDbEntity();

            Mapper.Map(insert, entity);

            Context.Add(entity);
            Context.SaveChanges();

            return Mapper.Map<TModel>(entity);
        }

        public virtual TModel Update(int id, TUpdate update)
        {
            var entity = Context.Set<TDbEntity>().Find(id);
            if (entity != null)
            {
                Mapper.Map(update, entity);
                Context.SaveChanges();
                return Mapper.Map<TModel>(entity);
            }
            else
            {
                return null;
            }
        }
    }
}
