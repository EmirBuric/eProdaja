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
using Azure.Core;
using Microsoft.Extensions.Logging;

namespace eProdaja.Services
{
    public class KorisniciService : BaseCRUDServis<Modeli.Korisnici, KorisniciSearchObject, Database.Korisnici,KorisniciInsertRequest,KorisniciUpdateRequest>,IKorisniciService
    {
        ILogger<KorisniciService> _logger;
        public KorisniciService(EProdajaContext context,IMapper mapper,ILogger<KorisniciService> logger) 
        :base(context,mapper)
        {
            _logger = logger;
        }

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
     
        public override void BeforeInsert(KorisniciInsertRequest insert, Database.Korisnici entity)
        {
            _logger.LogInformation($"Adding user: {entity.KorisnickoIme}");
            if (insert.Lozinka != insert.LozinkaPotvrda)
            {
                throw new Exception("Lozinka i LozinkaPotvrda moraju biti iste");
            }
            entity.LozinkaSalt = GenerateSalt();
            entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, insert.Lozinka);
            base.BeforeInsert(insert, entity);
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

        public override void BeforeUpdate(KorisniciUpdateRequest update, Database.Korisnici entity)
        {
            if (update.Lozinka != null)
            {
                if (update.Lozinka != update.LozinkaPotvrda)
                {
                    throw new Exception("Lozinka i LozinkaPotvrda moraju biti iste");
                }
                entity.LozinkaSalt = GenerateSalt();
                entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, update.Lozinka);
            }
            base.BeforeUpdate(update, entity);
        }

        public Modeli.Korisnici Login(string username, string password)
        {
            var entity = Context.Korisnicis.FirstOrDefault(x => x.KorisnickoIme == username);

            if (entity == null)
            {
                return null;
            }

            var hash = GenerateHash(entity.LozinkaSalt, password);
            if(hash!=entity.LozinkaHash)
            {
                return null;
            }
            return Mapper.Map<Modeli.Korisnici>(entity);
        }
    }
}
