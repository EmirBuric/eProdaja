using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.API.Controllers
{
    public class BaseCRUDController<TModel, TSearch, TInsert, TUpdate> : BaseController<TModel, TSearch> 
        where TModel : class
        where TSearch:BaseSearchObject
    {
        protected new ICRUDService<TModel, TSearch, TInsert, TUpdate> _service;
        public BaseCRUDController(ICRUDService<TModel, TSearch,TInsert,TUpdate> service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual TModel Insert(TInsert insert)
        {
            return _service.Insert(insert);
        }
        [HttpPut("{id}")]
        public virtual TModel Update(int id, TUpdate update)
        {
            return _service.Update(id, update);
        }
    }
}
