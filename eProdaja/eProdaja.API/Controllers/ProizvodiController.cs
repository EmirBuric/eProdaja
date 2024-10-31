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
    public class ProizvodiController : BaseController<Proizvodi,ProizvodiSearchObject,ProizvodiInsertRequest,ProizvodiUpdateRequest>
    {
        public ProizvodiController(IProizvodiService service) : base(service)
        {}
    }
}
