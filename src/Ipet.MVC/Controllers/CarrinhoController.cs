using EnterpriseStore.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ipet.MVC.Models;

namespace EnterpriseStore.MVC.Controllers
{
    [Authorize]
    public class CarrinhoController : Controller
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CarrinhoController(ICarrinhoService carrinhoService, UserManager<ApplicationUser> userManager)
        {
            _carrinhoService = carrinhoService;
            _userManager = userManager;
        }

        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);
            return View(carrinho);
        }

        [HttpPost("carrinho/adicionar")]
        public async Task<IActionResult> AdicionarProduto(Guid produtoId, int quantidade)
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            await _carrinhoService.AdicionarProduto(usuarioId, produtoId, quantidade);
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
