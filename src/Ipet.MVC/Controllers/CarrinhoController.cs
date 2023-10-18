using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ipet.MVC.Models;
using Ipet.Interfaces.Services;
using Ipet.Domain.Models;
using Ipet.ViewModels;
using AutoMapper;
using System.Globalization;

namespace Ipet.MVC.Controllers
{
    [Authorize]
    public class CarrinhoController : Controller
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CarrinhoController(ICarrinhoService carrinhoService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _carrinhoService = carrinhoService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);

            var carrinhoViewModel = _mapper.Map<CarrinhoViewModel>(carrinho);

            //if (carrinhoViewModel.Produtos == null)
            //{
            //    carrinhoViewModel.Produtos = new List<CarrinhoProdutoViewModel>
            //    {
            //        new CarrinhoProdutoViewModel
            //        {
            //            Produto = new ProdutoViewModel
            //            {
            //                Nome = "Produto Padrão",
            //                Valor = 0.0m
            //            },
            //            Quantidade = 0
            //        }
            //    };
            //}

            ViewData["Title"] = "Carrinho";


            return View(carrinhoViewModel);
        }

        [Route("carrinho/conta")]
        public async Task<IActionResult> Conta()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);

            var carrinhoViewModel = _mapper.Map<CarrinhoViewModel>(carrinho);
            decimal valorDecimal = 0;

            if (carrinhoViewModel.Produtos != null)
            {
                valorDecimal = carrinhoViewModel.Produtos.Sum(item => item.Produto.Valor);
            }

            return View("_SomaCarrinhoPartial", valorDecimal);
        }

        [HttpPost("carrinho/adicionar")]
        public async Task<IActionResult> AdicionarProduto(Guid produtoId, int quantidade)
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            await _carrinhoService.AdicionarProduto(usuarioId, produtoId, quantidade);
            return RedirectToAction("Index");
        }

        [HttpPost("carrinho/alterar-quantidade")]
        public async Task<IActionResult> AtualizarQuantidadeProduto(Guid produtoId, int quantidade)
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            await _carrinhoService.AtualizarQuantidadeProduto(usuarioId, produtoId, quantidade);
            return RedirectToAction("Index");
        }

        [HttpPost("carrinho/remover")]
        public async Task<IActionResult> RemoverProduto(Guid produtoId, int quantidade)
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            await _carrinhoService.RemoverProduto(usuarioId, produtoId, quantidade);
            return RedirectToAction("Index");
        }

        [HttpPost("carrinho/finalizar")]
        public async Task<IActionResult> FinalizarCompra()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);

            if (carrinho == null)
            {

                return RedirectToAction("Index");
            }

            await _carrinhoService.FinalizarCompra(carrinho.Id);
            return RedirectToAction("Index");
        }

    }
}
