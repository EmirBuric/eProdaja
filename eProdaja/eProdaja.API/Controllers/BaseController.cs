﻿using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace eProdaja.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController<TModel,TSearch>:ControllerBase 
        where TSearch:BaseSearchObject

    {
        protected IService<TModel,TSearch> _service;
        public BaseController(IService<TModel, TSearch> service)
        {
            _service = service;
        }
        [HttpGet]
        public virtual PagedResults<TModel> GetList([FromQuery] TSearch searchObject)
        {
            return _service.GetPaged(searchObject);
        }
        [HttpGet("{id}")]
        public virtual TModel GetById(int id)
        {
            return _service.GetById(id);
        }
        
    }
}
