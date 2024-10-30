﻿using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProizvodiController : ControllerBase
    {
        private IProizvodiService _service;
        public ProizvodiController(IProizvodiService service) 
        {
            _service = service;
        }
        [HttpGet]
        public List<Proizvodi> GetList() 
        {
            return _service.GetList();  
        }
        [HttpPost]
        public Proizvodi Insert(ProizvodiInsertRequest request) 
        {
            return _service.Insert(request);
        }
        [HttpPut]
        public Proizvodi Update(int id,ProizvodiUpdateRequest request) 
        {
            return _service.Update(id,request);
        }
    }
}
