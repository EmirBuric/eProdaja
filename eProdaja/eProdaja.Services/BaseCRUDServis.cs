using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public abstract class BaseCRUDServis<TModel, TSearch, TDbEntity,TInsert,TUpdate> : 
        BaseService<TModel, TSearch, TDbEntity>,
        ICRUDService<TModel,TSearch,TInsert,TUpdate>
        where TModel : class
        where TSearch : BaseSearchObject  
        where TDbEntity : class, new()
    {
        public BaseCRUDServis(EProdajaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public virtual TModel Insert(TInsert insert)
        {
            TDbEntity entity = new TDbEntity();

            Mapper.Map(insert, entity);

            BeforeInsert(insert, entity);
            Context.Add(entity);
            Context.SaveChanges();

            return Mapper.Map<TModel>(entity);
        }
        public virtual void BeforeInsert(TInsert insert, TDbEntity entity) 
        {

        }

        public virtual TModel Update(int id, TUpdate update)
        {
            var entity = Context.Set<TDbEntity>().Find(id);
            if (entity != null)
            {
                Mapper.Map(update, entity);
                BeforeUpdate(update, entity);
                Context.SaveChanges();
                return Mapper.Map<TModel>(entity);
            }
            else
            {
                return null;
            }
        }
        public virtual void BeforeUpdate(TUpdate update, TDbEntity entity)
        {

        }
    }
}
