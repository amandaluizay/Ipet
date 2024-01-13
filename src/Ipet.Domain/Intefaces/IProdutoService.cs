

using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task<List<Produto>> GetProdutosByTags(string[] tags);
        Task Remover(Guid id);
    }
}