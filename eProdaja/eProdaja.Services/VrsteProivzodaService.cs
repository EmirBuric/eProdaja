﻿using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class VrsteProizvodaService :BaseService<Modeli.VrsteProizvodum, VrsteProizvodaSearchObject,Database.VrsteProizvodum,BaseInsertRequest,BaseUpdateRequest>, IVrsteProizvodaService
    {
        public VrsteProizvodaService(EProdajaContext context, IMapper mapper) 
            : base(context, mapper) {}
        
    }
}