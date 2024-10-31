using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Modeli.SearchObject;
using eProdaja.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;

namespace eProdaja.Services
{
    public class KorisniciService : BaseService<Modeli.Korisnici, KorisniciSearchObject, Database.Korisnici,KorisniciInsertRequest,KorisniciUpdateRequest>,IKorisniciService
    {
        public KorisniciService(EProdajaContext context,IMapper mapper) 
        :base(context,mapper){}

        public override IQueryable<Database.Korisnici> AddFilter(KorisniciSearchObject search, IQueryable<Database.Korisnici> query)
        {
            var filterdQuerry=base.AddFilter(search, query);
            if (!string.IsNullOrWhiteSpace(search?.ImeGTE))
            {
                filterdQuerry = filterdQuerry.Where(x => x.Ime.StartsWith(search.ImeGTE));
            }
            if (!string.IsNullOrWhiteSpace(search?.PrezimeGTE))
            {
                filterdQuerry = filterdQuerry.Where(x => x.Prezime.StartsWith(search.PrezimeGTE));
            }
            if (!string.IsNullOrWhiteSpace(search?.Email))
            {
                filterdQuerry = filterdQuerry.Where(x => x.Email == search.Email);
            }
            if (!string.IsNullOrWhiteSpace(search?.KorisnickoIme))
            {
                filterdQuerry = filterdQuerry.Where(x => x.KorisnickoIme == search.KorisnickoIme);
            }
            if (search.IsKorisniciUlogeIncluded == true)
            {
                filterdQuerry = filterdQuerry.Include(x => x.KorisniciUloges).ThenInclude(x => x.Uloga);
            }
            if (!string.IsNullOrEmpty(search?.OrderBy))
            {
                filterdQuerry = filterdQuerry.OrderBy(search.OrderBy);
            }
            if (search?.Page.HasValue == true && search?.PageSize.HasValue == true)
            {
                filterdQuerry = filterdQuerry.Skip(search.Page.Value * search.PageSize.Value).Take(search.PageSize.Value);
            }
            return filterdQuerry;
        }
        public override Modeli.Korisnici Insert(KorisniciInsertRequest request)
        {
            if (request.Lozinka != request.LozinkaPotvrda) 
            {
                throw new Exception("Lozinka i LozinkaPotvrda moraju biti iste");
            }

            Database.Korisnici entity= new Database.Korisnici();
            Mapper.Map(request, entity);

            entity.LozinkaSalt = GenerateSalt();
            entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Lozinka);

            Context.Add(entity);
            Context.SaveChanges();

            return Mapper.Map<Modeli.Korisnici>(entity);
        }
        public static string GenerateSalt()
        {
            var byteArray = RNGCryptoServiceProvider.GetBytes(16);
            return Convert.ToBase64String(byteArray);
        }
        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        public override Modeli.Korisnici Update(int id, KorisniciUpdateRequest request)
        {
           var entity = Context.Korisnicis.Find(id);
           Mapper.Map(request, entity);

            if (request.Lozinka != null) 
            {
                if (request.Lozinka != request.LozinkaPotvrda)
                {
                    throw new Exception("Lozinka i LozinkaPotvrda moraju biti iste");
                }
                entity.LozinkaSalt = GenerateSalt();
                entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Lozinka);
            }

            Context.SaveChanges();

            return Mapper.Map<Modeli.Korisnici>(entity);
        }
    }
}
