using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Service.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository, 
                                 INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }


        public async Task<List<Produto>> GetProdutosByTags(string[] tags)
        {

            var todosProdutos = await _produtoRepository.ObterTodos();



            var produtos = await _produtoRepository.GetProdutosByTag(tags,todosProdutos);

            return produtos;
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