

using EnterpriseStore.Domain.Models;
using EnterpriseStore.Service.Models;
using Ipet.Domain.Models;

namespace EnterpriseStore.Domain.Intefaces
{
    public interface IServicoService : IDisposable
    {
        Task Adicionar(Servico servico);
        Task Atualizar(Servico servico);
        Task Remover(Guid id);
    }
}