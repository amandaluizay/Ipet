
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Data.Repository
{
    public interface IProdutoHashtagRepository : IRepository<ProdutoHashtag>
    {
        Task ExcluirTagsDoProduto(Guid idProduto);
        Task<List<ProdutoHashtag>> ObterPorProdutoId(Guid produtoId);
    }
}