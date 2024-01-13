using Ipet.Data.Context;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class ProdutoHashtagRepository : Repository<ProdutoHashtag>, IProdutoHashtagRepository
    {
        public ProdutoHashtagRepository(MeuDbContext context) : base(context) { }

        public async Task<List<ProdutoHashtag>> ObterPorProdutoId(Guid produtoId)
        {
            return await DbSet.Where(h => h.IdProduto == produtoId).ToListAsync();
        }
        public async Task ExcluirTagsDoProduto(Guid idProduto)
        {
            var tagsDoProduto = await DbSet.Where(tag => tag.IdProduto == idProduto).ToListAsync();

            if (tagsDoProduto.Any())
            {
                DbSet.RemoveRange(tagsDoProduto);
            }
        }

    }
}