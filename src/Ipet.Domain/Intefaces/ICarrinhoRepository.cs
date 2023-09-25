using EnterpriseStore.Domain.Intefaces;
using Ipet.Domain.Models;

namespace EnterpriseStore.Data.Repository
{
    public interface ICarrinhoRepository : IRepository<Carrinho>
    {
        Task<Carrinho> ObterCarrinhoPorUsuario(Guid usuarioId);
        Task<bool> ObterUsuarioPorId(Guid usuarioId);
    }
}