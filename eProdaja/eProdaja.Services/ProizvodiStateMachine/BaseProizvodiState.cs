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
            throw new Exception("Method not allowed");
        }
        public virtual Modeli.Proizvodi Update(int id,ProizvodiUpdateRequest request)
        {
            throw new Exception("Method not allowed");
        }
        public virtual Modeli.Proizvodi Activate(int id)
        {
            throw new Exception("Method not allowed");
        }
        public virtual Modeli.Proizvodi Hide(int id)
        {
            throw new Exception("Method not allowed");
        }
        public BaseProizvodiState CreateState(string stateName) 
        {
            switch (stateName)
            {
                case "initial":
                    return ServiceProvider.GetService<InitialProizvodiState>();
                case "draft":
                    return ServiceProvider.GetService<DraftProizvodiState>();
                default:
                    throw new Exception("State not recognized");
            }
        }
    }
}
