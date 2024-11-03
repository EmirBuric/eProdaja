using eProdaja.Modeli.Requests;
using eProdaja.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using eProdaja.Modeli;

namespace eProdaja.Services.ProizvodiStateMachine
{
    public class BaseProizvodiState
    {
        public EProdajaContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        public BaseProizvodiState(EProdajaContext context, IMapper mapper,IServiceProvider serviceProvider)
        {
            Context = context;
            Mapper = mapper;
            ServiceProvider = serviceProvider;
        }
        public virtual Modeli.Proizvodi Insert(ProizvodiInsertRequest request)
        {
            throw new UserException("Method not allowed");
        }
        public virtual Modeli.Proizvodi Update(int id,ProizvodiUpdateRequest request)
        {
            throw new UserException("Method not allowed");
        }
        public virtual Modeli.Proizvodi Activate(int id)
        {
            throw new UserException("Method not allowed");
        }
        public virtual Modeli.Proizvodi Hide(int id)
        {
            throw new UserException("Method not allowed");
        }
        public virtual Modeli.Proizvodi Edit(int id)
        {
            throw new UserException("Method not allowed");
        }
        public virtual List<string> AllowedActions(Database.Proizvodi entity) 
        {
            throw new UserException("Method not allowed");
        }
        public BaseProizvodiState CreateState(string stateName) 
        {
            switch (stateName)
            {
                case "initial":
                    return ServiceProvider.GetService<InitialProizvodiState>();
                case "draft":
                    return ServiceProvider.GetService<DraftProizvodiState>();
                case "active":
                    return ServiceProvider.GetService<ActiveProizvodiState>();
                case "hidden":
                    return ServiceProvider.GetService<HiddenProizvodiState>();
                default:
                    throw new Exception("State not recognized");
            }
        }
    }
}
