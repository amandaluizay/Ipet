using EnterpriseStore.Domain.Intefaces;
using Ipet.Domain.Models;

namespace EnterpriseStore.Service.Services
{
    public class produtoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public produtoService(IProdutoRepository produtoRepository, 
                                 INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
        {


            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
          

            await _produtoRepository.Atualizar(produto);
        }

        public async Task Remover(Guid id)
        {

            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}