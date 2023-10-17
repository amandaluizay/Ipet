using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Service.Services
{
    public class PerfilPetService : BaseService, IPerfilPetService
    {
        private readonly IPerfilPetRepository _perfilPetRepository;

        public PerfilPetService(IPerfilPetRepository perfilPetRepository, 
                                 INotificador notificador) : base(notificador)
        {
            _perfilPetRepository = perfilPetRepository;
        }

        public async Task Adicionar(PerfilPet perfilPet)
        {


            await _perfilPetRepository.Adicionar(perfilPet);
        }

        public async Task Atualizar(PerfilPet perfilPet)
        {
          

            await _perfilPetRepository.Atualizar(perfilPet);
        }

        public async Task Remover(Guid id)
        {

            await _perfilPetRepository.Remover(id);
        }

        public void Dispose()
        {
            _perfilPetRepository?.Dispose();
        }

    }
}