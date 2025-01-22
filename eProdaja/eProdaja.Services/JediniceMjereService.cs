using eProdaja.Modeli;
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
    public class JediniceMjereService :BaseService<Modeli.JediniceMjere, JedinicaMjereSearchObject,Database.JediniceMjere>, IJediniceMjereService
    {
        public JediniceMjereService(EProdajaContext context, IMapper mapper) 
            : base(context, mapper) {}
        
    }
}
