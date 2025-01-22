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
    public class JediniceMjereController : BaseController<JediniceMjere, JedinicaMjereSearchObject>
    {
        public JediniceMjereController(IJediniceMjereService service) : base(service)
        {
        }
    }
}
