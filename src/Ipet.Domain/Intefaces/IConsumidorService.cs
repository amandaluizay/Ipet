
using EnterpriseStore.Domain.Models;
using EnterpriseStore.Service.Models;
using Ipet.Domain.Models;
using System;
using System.Threading.Tasks;

namespace EnterpriseStore.Domain.Intefaces
{
    public interface IConsumidorService : IDisposable
    {
        Task Adicionar(Consumidor consumidor);
        Task Atualizar(Consumidor consumidor);
        Task Remover(Guid id);
    }
}