using Ipet.Data.Context;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class ServiçoHashtagRepository : Repository<ServiçoHashtag>, IServiçoHashtagRepository
    {
        public ServiçoHashtagRepository(MeuDbContext context) : base(context) { }

        public async Task<List<ServiçoHashtag>> ObterPorServicoId(Guid servicoId)
        {
            return await DbSet.Where(h => h.IdServico == servicoId).ToListAsync();
        }
        public async Task ExcluirTagsDoServico(Guid idServico)
        {
            var tagsDoServico = await DbSet.Where(tag => tag.IdServico == idServico).ToListAsync();

            if (tagsDoServico.Any())
            {
                DbSet.RemoveRange(tagsDoServico);
            }
        }
    }
}