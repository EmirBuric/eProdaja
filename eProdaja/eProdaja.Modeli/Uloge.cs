using System;
using System.Collections.Generic;

namespace eProdaja.Modeli 
{
    public partial class Uloge
    {
        public int UlogaId { get; set; }

        public string Naziv { get; set; } = null!;

        public string? Opis { get; set; }
    }
}


