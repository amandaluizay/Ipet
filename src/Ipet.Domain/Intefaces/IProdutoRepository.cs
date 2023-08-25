using Ipet.Domain.Models;

namespace EnterpriseStore.Domain.Intefaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        //Task<IEnumerable<Produto>> ObterProdutosFornecedores();
        //Task<Produto> ObterProdutoFornecedor(Guid id);
    }
}