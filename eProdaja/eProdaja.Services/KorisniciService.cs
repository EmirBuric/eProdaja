using eProdaja.Modeli;
using eProdaja.Modeli.Requests;
using eProdaja.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class KorisniciService : IKorisniciService
    {
        public EProdajaContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public KorisniciService(EProdajaContext context,IMapper mapper) 
        {
            Context = context;
            Mapper = mapper;
        }
        
        public virtual List<Modeli.Korisnici> GetList()
        {
            var list= Context.Korisnicis.ToList();
            List<Modeli.Korisnici> result = new List<Modeli.Korisnici>();
            /*list.ForEach(x =>result.Add(new Modeli.Korisnici() { 
                KorisnikId = x.KorisnikId,
                KorisnickoIme = x.KorisnickoIme,
                Email = x.Email,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Telefon = x.Telefon,
                Status = x.Status
            }));*/
            result = Mapper.Map(list,result);
            return result;
        }

        public Modeli.Korisnici Insert(KorisniciInsertRequest request)
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

        public Modeli.Korisnici Update(int id, KorisniciUpdateRequest request)
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
