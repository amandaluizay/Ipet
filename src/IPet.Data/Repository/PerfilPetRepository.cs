using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class PerfilPetRepository : Repository<PerfilPet>, IPerfilPetRepository
    {
        public PerfilPetRepository(MeuDbContext context) : base(context) { }

        public async Task<PerfilPet> ObterPerfilUsuario(Guid id)
        {
            return await Db.PerfilPet.AsNoTracking().Include(f => f.IdUsuario)
                .FirstOrDefaultAsync(p => p.IdUsuario == id);
        }
    }

}