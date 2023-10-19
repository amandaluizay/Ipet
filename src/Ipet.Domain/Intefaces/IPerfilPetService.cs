

using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IPerfilPetService : IDisposable
    {
        Task Adicionar(PerfilPet perfilPet);
        Task Atualizar(PerfilPet perfilPet);
        Task Remover(Guid id);
    }
}