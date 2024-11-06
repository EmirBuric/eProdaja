using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eProdaja.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisniciController : BaseCRUDController<Korisnici, KorisniciSearchObject,KorisniciInsertRequest,KorisniciUpdateRequest>
    {
        public KorisniciController(IKorisniciService service) :base(service) {}
        [HttpPost("login")]
        [AllowAnonymous]
        public Korisnici Login(string username, string password)
        {
            return (_service as IKorisniciService).Login(username, password);
        }
    }
}
