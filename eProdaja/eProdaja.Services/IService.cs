using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public interface IService<TModel,TSearch,TInsert,TUpdate> 
        where TSearch:BaseSearchObject 
        where TInsert:BaseInsertRequest
        where TUpdate:BaseUpdateRequest
    {
        public PagedResults<TModel> GetPaged(TSearch search);
        public TModel GetById(int id);
        public TModel  Insert(TInsert insert);
        public TModel Update(int id,TUpdate update);
    }
}
