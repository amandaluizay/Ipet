using EnterpriseStore.Domain.Intefaces;
using Ipet.Domain.Models;

namespace EnterpriseStore.Service.Services
{
    public class ConsumidorService : BaseService, IConsumidorService
    {
        private readonly IConsumidorRepository _consumidorRepository;

        public ConsumidorService(IConsumidorRepository consumidorRepository, 
                                 INotificador notificador) : base(notificador)
        {
            _consumidorRepository = consumidorRepository;
        }

        public async Task Adicionar(Consumidor consumidor)
        {

            await _consumidorRepository.Adicionar(consumidor);
        }

        public async Task Atualizar(Consumidor consumidor)
        {

            await _consumidorRepository.Atualizar(consumidor);
        }

        public async Task Remover(Guid id)
        {

            await _consumidorRepository.Remover(id);
        }

        public void Dispose()
        {
            _consumidorRepository?.Dispose();
        }
    }
}