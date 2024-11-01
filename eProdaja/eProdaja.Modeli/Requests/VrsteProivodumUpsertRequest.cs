using System;
using System.Collections.Generic;
using System.Text;

namespace eProdaja.Modeli.Requests
{
    public class VrsteProivodumUpsertRequest:BaseUpdateRequest
    {
        public string Naziv { get; set; } = null!;
    }
}
