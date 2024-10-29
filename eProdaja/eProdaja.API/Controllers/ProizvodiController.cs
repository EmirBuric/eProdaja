using eProdaja.Modeli;
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
        public ProizvodiController(ProizvodiService service) 
        {
            _service = service;
        }
        [HttpGet]
        public List<Proizvodi> GetList() 
        {
            return _service.GetList();  
        }
    }
}
