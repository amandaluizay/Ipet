using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context) { }


        public async Task<List<Produto>> GetProdutosByTag(string tag)
        {
            return await DbSet
                .Include(p => p.Hashtags)
                .Where(p => p.Hashtags.Any(h => h.Tag.Contains(tag)))
                .ToListAsync();
        }
    }
}