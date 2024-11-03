﻿using eProdaja.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services.ProizvodiStateMachine
{
    public class ActiveProizvodiState : BaseProizvodiState
    {
        public ActiveProizvodiState(EProdajaContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
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
                nameof(Hide)
            };
        }
    }
}
