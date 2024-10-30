﻿using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public interface IProizvodiService
    {
        List<Proizvodi> GetList();
        Proizvodi Insert(ProizvodiInsertRequest request);
        Proizvodi Update(int id,ProizvodiUpdateRequest request);
    }
}
