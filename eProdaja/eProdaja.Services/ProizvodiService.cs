using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services.Database;
using eProdaja.Services.ProizvodiStateMachine;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eProdaja.Services
{
    public class ProizvodiService : BaseCRUDServis<Modeli.Proizvodi, ProizvodiSearchObject, Database.Proizvodi,ProizvodiInsertRequest,ProizvodiUpdateRequest>,IProizvodiService
    {
        public BaseProizvodiState BaseProizvodiState { get; set; }
        public ProizvodiService(EProdajaContext context, IMapper mapper,BaseProizvodiState baseProizvodiState) 
            :base(context,mapper) 
        {
            BaseProizvodiState = baseProizvodiState;
        }

        public override IQueryable<Database.Proizvodi> AddFilter(ProizvodiSearchObject search, IQueryable<Database.Proizvodi> query)
        {
            var filterdQuery=base.AddFilter(search, query);
            if(!string.IsNullOrWhiteSpace(search?.FTS))
            {
                filterdQuery = filterdQuery.Where(x => x.Naziv.Contains(search.FTS));
            }
            return filterdQuery;
        }
        public override Modeli.Proizvodi Insert(ProizvodiInsertRequest insert)
        {
            var state = BaseProizvodiState.CreateState("initial");
            return state.Insert(insert);
        }

        public override Modeli.Proizvodi Update(int id, ProizvodiUpdateRequest update)
        {
            var entity=GetById(id);
            var state = BaseProizvodiState.CreateState(entity.StateMachine);
            return state.Update(id, update);
        }

        public Modeli.Proizvodi Activate(int id)
        {
            var entity = GetById(id);
            var state = BaseProizvodiState.CreateState(entity.StateMachine);
            return state.Activate(id);
        }
    }
}
