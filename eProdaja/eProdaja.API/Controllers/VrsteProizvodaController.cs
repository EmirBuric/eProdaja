using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace eProdaja.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VrsteProizvodaController : BaseCRUDController<VrsteProizvodum, VrsteProizvodaSearchObject, VrsteProivodumUpsertRequest, VrsteProivodumUpsertRequest>
    {
        public VrsteProizvodaController(IVrsteProizvodaService service) :base(service) {}

        [Authorize(Roles = "Administrator")]
        public override VrsteProizvodum Insert(VrsteProivodumUpsertRequest request)
        {
            return base.Insert(request);
        }
        [AllowAnonymous]
        public override PagedResults<VrsteProizvodum> GetList([FromQuery] VrsteProizvodaSearchObject searchObject)
        {
            return base.GetList(searchObject);
        }
    }
}
