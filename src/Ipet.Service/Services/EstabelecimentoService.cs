using EnterpriseStore.Domain.Intefaces;
using Ipet.Domain.Models;

namespace EnterpriseStore.Service.Services
{
    public class EstabelecimentoService : BaseService, IEstabelecimentoService
    {
        private readonly IEstabelecimentoRepository _estabelecimentoRepository;

        public EstabelecimentoService(IEstabelecimentoRepository estabelecimentoRepository, 
                                 INotificador notificador) : base(notificador)
        {
            _estabelecimentoRepository = estabelecimentoRepository;
        }

        public async Task Adicionar(Estabelecimento fornecedor)
        {

            if (_estabelecimentoRepository.Buscar(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            if (_estabelecimentoRepository.Buscar(f => f.Conta == fornecedor.Conta).Result.Any())
            {
                Notificar("Já existe um Estabelecimento com essa conta.");
                return;
            }

            await _estabelecimentoRepository.Adicionar(fornecedor);
        }

        
        public async Task Atualizar(Estabelecimento fornecedor)
        {
          

            if (_estabelecimentoRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _estabelecimentoRepository.Atualizar(fornecedor);
        }

        
        public async Task Remover(Guid id)
        {

            await _estabelecimentoRepository.Remover(id);
        }

        
        public void Dispose()
        {

            _estabelecimentoRepository?.Dispose();
        }
    
    }
}