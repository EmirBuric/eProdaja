using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class ProizvodiService : IProizvodiService
    {
        public EProdajaContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public ProizvodiService(EProdajaContext context, IMapper mapper) 
        {
            Context = context;
            Mapper = mapper;
        }
        
        public virtual List<Modeli.Proizvodi> GetList()
        {
            var list= Context.Proizvodis.ToList();
            var result= new List<Modeli.Proizvodi>();
            list.ForEach(item =>
            {
                result.Add(new Modeli.Proizvodi
                {
                    ProizvodId = item.ProizvodId,
                    Cijena = item.Cijena,
                    Naziv = item.Naziv
                });
            });
            return result;
        }

        public Modeli.Proizvodi Insert(ProizvodiInsertRequest request)
        {
            Database.Proizvodi entity = new Database.Proizvodi();

            Mapper.Map(request, entity);

            Context.Add(entity);
            Context.SaveChanges();

            return Mapper.Map<Modeli.Proizvodi>(entity);
        }

        public Modeli.Proizvodi Update(int id, ProizvodiUpdateRequest request)
        {
            var entity = Context.Proizvodis.Find(id);

            Mapper.Map(request, entity);
            Context.SaveChanges();

            return Mapper.Map<Modeli.Proizvodi>(entity);
        }
    }
}
