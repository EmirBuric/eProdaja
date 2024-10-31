using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace eProdaja.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TModel,TSearch,TInsert,TUpdate>:ControllerBase 
        where TSearch:BaseSearchObject
        where TInsert : BaseInsertRequest
        where TUpdate : BaseUpdateRequest
    {
        protected IService<TModel,TSearch,TInsert,TUpdate> _service;
        public BaseController(IService<TModel, TSearch,TInsert,TUpdate> service)
        {
            _service = service;
        }
        [HttpGet]
        public PagedResults<TModel> GetList([FromQuery] TSearch searchObject)
        {
            return _service.GetPaged(searchObject);
        }
        [HttpGet("{id}")]
        public TModel GetById(int id)
        {
            return _service.GetById(id);
        }
        [HttpPost]
        public TModel Insert(TInsert insert)
        {
            return _service.Insert(insert);
        }
        [HttpPut("{id}")]
        public TModel Update(int id,TUpdate update) 
        {
            return _service.Update(id,update);
        }
    }
}
