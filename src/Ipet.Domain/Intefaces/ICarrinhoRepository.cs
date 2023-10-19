
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Data.Repository
{
    public interface ICarrinhoRepository : IRepository<Carrinho>
    {
        Task<Carrinho> ObterCarrinhoPorUsuario(Guid usuarioId);
        Task<bool> ObterUsuarioPorId(Guid usuarioId);
    }
}