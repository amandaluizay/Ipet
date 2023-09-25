using EnterpriseStore.Data.Repository;
using EnterpriseStore.Domain.Intefaces;
using EnterpriseStore.Interfaces.Services;
using Ipet.Domain.Models;

namespace EnterpriseStore.Service.Services
{
    public class CarrinhoService : BaseService, ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository, INotificador notificador)
            : base(notificador)
        {
            _carrinhoRepository = carrinhoRepository;
        }

        public async Task AdicionarProduto(Guid carrinhoId, Guid produtoId, int quantidade)
        {
            var carrinho = await _carrinhoRepository.ObterPorId(carrinhoId);

            if (carrinho == null)
            {
                Notificar("Carrinho não encontrado.");
                return;
            }

            var carrinhoProduto = carrinho.CarrinhoProdutos.FirstOrDefault(cp => cp.ProdutoId == produtoId);

            if (carrinhoProduto == null)
            {
                carrinho.CarrinhoProdutos.Add(new CarrinhoProduto
                {
                    CarrinhoId = carrinhoId,
                    ProdutoId = produtoId,
                    Quantidade = quantidade
                });
            }
            else
            {
                carrinhoProduto.Quantidade += quantidade;
            }

            await _carrinhoRepository.Atualizar(carrinho);
        }

        public async Task RemoverProduto(Guid carrinhoId, Guid produtoId, int quantidade)
        {
            var carrinho = await _carrinhoRepository.ObterPorId(carrinhoId);

            if (carrinho == null)
            {
                Notificar("Carrinho não encontrado.");
                return;
            }

            var carrinhoProduto = carrinho.CarrinhoProdutos.FirstOrDefault(cp => cp.ProdutoId == produtoId);

            if (carrinhoProduto != null)
            {
                if (quantidade >= carrinhoProduto.Quantidade)
                {
                    carrinho.CarrinhoProdutos.Remove(carrinhoProduto);
                }
                else
                {
                    carrinhoProduto.Quantidade -= quantidade;
                }

                await _carrinhoRepository.Atualizar(carrinho);
            }
        }

        public async Task FinalizarCompra(Guid carrinhoId)
        {
            var carrinho = await _carrinhoRepository.ObterPorId(carrinhoId);

            if (carrinho == null)
            {
                Notificar("Carrinho não encontrado.");
                return;
            }

            carrinho.CarrinhoProdutos.Clear();
            await _carrinhoRepository.Atualizar(carrinho);
        }

        public async Task<Carrinho> ObterCarrinhoPorUsuario(Guid usuarioId)
        {
            return await _carrinhoRepository.ObterCarrinhoPorUsuario(usuarioId);
        }

        public async Task<Carrinho> CriarCarrinho(Guid usuarioId)
        {
            var usuario = await _carrinhoRepository.ObterUsuarioPorId(usuarioId);

            if (usuario == null)
            {
                Notificar("Usuário não encontrado.");
                return null;
            }

            var carrinho = new Carrinho
            {
                UsuarioId = usuarioId
            };

            await _carrinhoRepository.Adicionar(carrinho);
            return carrinho;
        }


        public async Task RemoverCarrinho(Guid carrinhoId)
        {
            await _carrinhoRepository.Remover(carrinhoId);
        }

        public async Task Dispose()
        {
            _carrinhoRepository?.Dispose();
        }
    }
}
