﻿using eProdaja.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class ProizvodiService : IProizvodiService
    {
        public List<Proizvodi> List = new List<Proizvodi>()
        {
            new Proizvodi()
            {
                ProizvodId = 1,
                Naziv="Laptop",
                Cijena=999
            },
            new Proizvodi()
            {
                ProizvodId = 2,
                Naziv="Monitor",
                Cijena=499
            }
        };
        public virtual List<Proizvodi> GetList()
        {
            return List;
        }
    }
}
