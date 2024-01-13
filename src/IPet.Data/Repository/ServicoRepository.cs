using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class ServicoRepository : Repository<Servico>, IServicoRepository
    {
        public ServicoRepository(MeuDbContext context) : base(context) { }
        public async Task<List<Servico>> GetServicosByTag(string[] tags, List<Servico> todosServicos) { 
            if (tags == null || tags.Length == 0)
            {
                return todosServicos;
            }

            var servicos = DbSet.Include(p => p.Hashtags).AsQueryable();

            foreach (var tag in tags)
            {
                servicos = servicos.Where(p => p.Hashtags.Any(h => h.Tag == tag));
            }

            return await servicos.Distinct().ToListAsync();
        }
    }
}