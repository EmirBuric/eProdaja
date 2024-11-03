using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProizvodiController : BaseCRUDController<Proizvodi,ProizvodiSearchObject,ProizvodiInsertRequest,ProizvodiUpdateRequest>
    {
        protected new IProizvodiService _service;
        public ProizvodiController(IProizvodiService service) : base(service)
        {
            _service = service;
        }
        [HttpPut("{id}/activate")]
        public Proizvodi Activate(int id)
        {
            return _service.Activate(id);  
        }
        [HttpPut("{id}/edit")]
        public Proizvodi Edit(int id)
        {
            return _service.Edit(id);
        }
        [HttpPut("{id}/hide")]
        public Proizvodi Hide(int id)
        {
            return _service.Hide(id);
        }
        [HttpGet("{id}/allowedActions")]
        public List<string> AllowedActions(int id)
        {
            return _service.AllowedActions(id);
        }
    }
}
