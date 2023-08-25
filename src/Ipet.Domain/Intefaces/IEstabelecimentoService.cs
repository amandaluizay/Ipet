
using EnterpriseStore.Domain.Models;
using EnterpriseStore.Service.Models;
using Ipet.Domain.Models;
using System;
using System.Threading.Tasks;

namespace EnterpriseStore.Domain.Intefaces
{
    public interface IEstabelecimentoService : IDisposable
    {
        Task Adicionar(Estabelecimento estabelecimento);
        Task Atualizar(Estabelecimento estabelecimento);
        Task Remover(Guid id);
    }
}