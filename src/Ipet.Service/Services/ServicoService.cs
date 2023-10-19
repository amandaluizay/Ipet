using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Service.Services
{
    public class ServicoService : BaseService, IServicoService
    {
        private readonly IServicoRepository _servicoRepository;

        public ServicoService(IServicoRepository servicoRepository, 
                                 INotificador notificador) : base(notificador)
        {
            _servicoRepository = servicoRepository;
        }

        public async Task Adicionar(Servico servico)
        {


            await _servicoRepository.Adicionar(servico);
        }

        public async Task Atualizar(Servico servico)
        {
          

            await _servicoRepository.Atualizar(servico);
        }

        public async Task Remover(Guid id)
        {

            await _servicoRepository.Remover(id);
        }

        public void Dispose()
        {
            _servicoRepository?.Dispose();
        }

    }
}