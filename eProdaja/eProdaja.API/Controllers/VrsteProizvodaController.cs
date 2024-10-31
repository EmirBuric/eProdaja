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
    public class VrsteProizvodaController : BaseController<VrsteProizvodum,VrsteProizvodaSearchObject,BaseInsertRequest,BaseUpdateRequest>
    {
        public VrsteProizvodaController(IVrsteProizvodaService service) :base(service) {}        
    }
}
