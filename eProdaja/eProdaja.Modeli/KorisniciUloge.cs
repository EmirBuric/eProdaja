﻿using System;
using System.Collections.Generic;

namespace eProdaja.Modeli 
{
    public partial class KorisniciUloge
    {
        public int KorisnikUlogaId { get; set; }

        public int KorisnikId { get; set; }

        public int UlogaId { get; set; }

        public DateTime DatumIzmjene { get; set; }

        public virtual Uloge Uloga { get; set; } = null!;
    }
}

