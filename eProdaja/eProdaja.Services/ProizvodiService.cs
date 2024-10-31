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
    public class ProizvodiService : BaseService<Modeli.Proizvodi,ProizvodiSearchObject,Database.Proizvodi,ProizvodiInsertRequest,ProizvodiUpdateRequest>,IProizvodiService
    {

        public ProizvodiService(EProdajaContext context, IMapper mapper) :base(context,mapper) {}

        public override IQueryable<Database.Proizvodi> AddFilter(ProizvodiSearchObject search, IQueryable<Database.Proizvodi> query)
        {
            var filterdQuery=base.AddFilter(search, query);
            if(!string.IsNullOrWhiteSpace(search?.FTS))
            {
                filterdQuery = filterdQuery.Where(x => x.Naziv.Contains(search.FTS));
            }
            return filterdQuery;
        }
        
    }
}
