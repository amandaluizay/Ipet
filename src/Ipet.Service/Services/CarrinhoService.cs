﻿using Ipet.Data.Repository;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Ipet.Interfaces.Services;

namespace Ipet.Service.Services
{
    public class CarrinhoService : BaseService, ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository, IProdutoRepository produtoRepository, INotificador notificador)
            : base(notificador)
        {
            _carrinhoRepository = carrinhoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> AdicionarProduto(Guid userId, Guid produtoId, int quantidade)
        {
            var usuario = await _carrinhoRepository.ObterUsuarioPorId(userId);

            if (usuario == null)
            {
                var carrinho = new Carrinho
                {
                    UsuarioId = userId
                };

                await _carrinhoRepository.Adicionar(carrinho);
                return false;
            }
            else
            {
                var produto = await _produtoRepository.ObterPorId(produtoId);
                var carrinho = await _carrinhoRepository.ObterCarrinhoPorUsuario(userId);

                var carrinhoProduto = carrinho.CarrinhoProdutos.FirstOrDefault(cp => cp.ProdutoId == produtoId);

                if (carrinhoProduto == null)
                {
                    carrinho.CarrinhoProdutos.Add(new CarrinhoProduto
                    {
                        CarrinhoId = carrinho.Id,
                        ProdutoId = produtoId,
                        Quantidade = quantidade
                    });
                }
                else
                {
                    carrinhoProduto.Quantidade += quantidade;
                }

                await _carrinhoRepository.Atualizar(carrinho);
                return true;
            }
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
            var carrinho = await _carrinhoRepository.ObterCarrinhoPorUsuario(usuarioId);

            if (carrinho == null)
            {
                carrinho = await CriarCarrinho(usuarioId);
            }

            return carrinho;
        }

        public async Task<Carrinho> CriarCarrinho(Guid usuarioId)
        {
            var usuario = await _carrinhoRepository.ObterUsuarioPorId(usuarioId);

            if (usuario == false)
            {
                var carrinho = new Carrinho
                {
                    UsuarioId = usuarioId
                };

                await _carrinhoRepository.Adicionar(carrinho);
                return carrinho;
            }
            else
            {
                Notificar("Carrinho já existe.");
                return null;
            }
        }

        public async Task AtualizarQuantidadeProduto(Guid carrinhoId, Guid produtoId, int novaQuantidade)
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
                Notificar("Produto não encontrado no carrinho.");
                return;
            }

            carrinhoProduto.Quantidade = novaQuantidade;

            await _carrinhoRepository.Atualizar(carrinho);
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
