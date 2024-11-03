using eProdaja.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services.ProizvodiStateMachine
{
    public class HiddenProizvodiState : BaseProizvodiState
    {
        public HiddenProizvodiState(EProdajaContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
        public override Modeli.Proizvodi Edit(int id)
        {
            var set = Context.Set<Database.Proizvodi>();
            var entity = set.Find(id);
            entity.StateMachine = "draft";
            Context.SaveChanges();
            return Mapper.Map<Modeli.Proizvodi>(entity);
        }
        public override List<string> AllowedActions(Database.Proizvodi entity)
        {
            return new List<string>()
            {
                nameof(Edit) 
            };
        }
    }
}
