using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public interface IVrsteProizvodaService : ICRUDService<Modeli.VrsteProizvodum, VrsteProizvodaSearchObject,VrsteProivodumUpsertRequest,VrsteProivodumUpsertRequest>
    {

    }
}
