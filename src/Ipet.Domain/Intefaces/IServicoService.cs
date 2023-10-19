

using Ipet.Domain.Models;

namespace Ipet.Domain.Intefaces
{
    public interface IServicoService : IDisposable
    {
        Task Adicionar(Servico servico);
        Task Atualizar(Servico servico);
        Task Remover(Guid id);
    }
}