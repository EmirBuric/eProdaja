using Azure.Core;
using eProdaja.Modeli.Requests;
using eProdaja.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services.ProizvodiStateMachine
{
    public class DraftProizvodiState : BaseProizvodiState
    {
        public DraftProizvodiState(EProdajaContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
            
        }
        public override Modeli.Proizvodi Update(int id, ProizvodiUpdateRequest request)
        {
            var set = Context.Set<Database.Proizvodi>();
            var entity = set.Find(id);
            Mapper.Map(request, entity);
            Context.SaveChanges();
            return Mapper.Map<Modeli.Proizvodi>(entity);
        }
        public override Modeli.Proizvodi Activate(int id)
        {
            var set = Context.Set<Database.Proizvodi>();
            var entity = set.Find(id);
            entity.StateMachine = "active";
            Context.SaveChanges();
            return Mapper.Map<Modeli.Proizvodi>(entity);
        }
        public override Modeli.Proizvodi Hide(int id)
        {
            var set = Context.Set<Database.Proizvodi>();
            var entity = set.Find(id);
            entity.StateMachine = "hidden";
            Context.SaveChanges();
            return Mapper.Map<Modeli.Proizvodi>(entity);
        }
        public override List<string> AllowedActions(Database.Proizvodi entity)
        {
            return new List<string>()
            {
                nameof(Update),
                nameof(Activate),
                nameof(Hide)
            };
        }
    }
}
