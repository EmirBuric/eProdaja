using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public interface ICRUDService<TModel,TSearch,TInsert,TUpdate> :IService<TModel,TSearch>
        where TModel : class
        where TSearch : BaseSearchObject
    {
        public TModel Insert(TInsert insert);
        public TModel Update(int id, TUpdate update);
    }
}
