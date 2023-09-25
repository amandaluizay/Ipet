﻿using Ipet.Domain.Models;

namespace EnterpriseStore.Interfaces.Services
{
    public interface ICarrinhoService
    {
        Task AdicionarProduto(Guid carrinhoId, Guid produtoId, int quantidade);
        Task RemoverProduto(Guid carrinhoId, Guid produtoId, int quantidade);
        Task FinalizarCompra(Guid carrinhoId);
        Task<Carrinho> ObterCarrinhoPorUsuario(Guid usuarioId);
        Task<Carrinho> CriarCarrinho(Guid usuarioId);
        Task RemoverCarrinho(Guid carrinhoId);
    }
}