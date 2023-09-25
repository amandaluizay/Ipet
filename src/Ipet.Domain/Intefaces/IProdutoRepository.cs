using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //Task<IEnumerable<Produto>> ObterProdutosPorEstabelecimento(Guid estabelecimentoid);
        //Task<IEnumerable<Produto>> ObterProdutosEstabelecimento();
        //Task<Produto> ObterProdutoEstabelecimento(Guid id);
    }
}