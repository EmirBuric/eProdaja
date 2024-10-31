using System;
using System.Collections.Generic;
using System.Text;

namespace eProdaja.Modeli.Requests
{
    public class ProizvodiInsertRequest:BaseInsertRequest
    {
        public string Naziv { get; set; } = null!;

        public string Sifra { get; set; } = null!;

        public decimal Cijena { get; set; }

        public int VrstaId { get; set; }

        public int JedinicaMjereId { get; set; }

        public bool Status { get; set; }
    }
}
