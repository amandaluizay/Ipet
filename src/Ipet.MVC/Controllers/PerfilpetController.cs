
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ipet.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Ipet.MVC.Models;
using Ipet.ViewModels;
using Ipet.Domain.Intefaces;
using Ipet.MVC.Extensions;
using Ipet.Data.Repository;
using Ipet.Interfaces.Services;
using Ipet.Service.Services;

namespace Ipet.MVC.Controllers
{
    [Authorize]
    public class PerfilPetController : BaseController
    {
        private readonly IPerfilPetRepository _perfilPetRepository;
        private readonly IPerfilPetService _perfilPetService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager; 

        public PerfilPetController(IPerfilPetRepository perfilPetRepository,
        IMapper mapper, IPerfilPetService perfilPetService,
                                  UserManager<ApplicationUser> userManager,
                                  INotificador notificador) : base(notificador)
        {
            _perfilPetRepository = perfilPetRepository;
            _perfilPetService = perfilPetService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PerfilPet>>(await _perfilPetRepository.ObterTodos()));
        }

        [AllowAnonymous]
        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }
            var user = _userManager.FindByIdAsync(produtoViewModel.EstabelecimentoId.ToString());
            ViewBag.NomeDoUsuario = user;
            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Usuario", "2")]
        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [ClaimsAuthorize("Usuario", "2")]
        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> Create(PerfilPet perfilPetViewModel)
        {
            if (!ModelState.IsValid) return View(perfilPetViewModel);

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                perfilPetViewModel.IdUsuario = Guid.Parse(user.Id);
            }
            else
            {
                return View(perfilPetViewModel);
            }
            await _perfilPetService.Adicionar(_mapper.Map<PerfilPet>(perfilPetViewModel));

            if (!OperacaoValida()) return View(perfilPetViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Usuario", "2")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var perfilPetViewModel = await ObterProduto(id);

           
            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Usuario", "2")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, PerfilPetViewModel perfilPetViewModel)
        {
            if (id != perfilPetViewModel.Id) return NotFound();

            var perfilAtualizacao = await ObterPerfilPet(id);

            if (!ModelState.IsValid) return View(perfilPetViewModel);


            perfilAtualizacao.Nome = perfilPetViewModel.Nome;
            perfilAtualizacao.Porte= perfilPetViewModel.Porte;
            perfilAtualizacao.Idade = perfilPetViewModel.Idade;
            perfilAtualizacao.TipoAnimal = perfilPetViewModel.TipoAnimal;
            perfilAtualizacao.Raca = perfilPetViewModel.Raca;
            perfilAtualizacao.Ativo = perfilPetViewModel.Ativo;
            perfilAtualizacao.Observacao = perfilPetViewModel.Observacao;

            await _perfilPetService.Atualizar(_mapper.Map<PerfilPet>(perfilAtualizacao));

            if (!OperacaoValida()) return View(perfilPetViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Usuario", "2")]
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }


        [ClaimsAuthorize("Usuario", "2")]
        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await ObterPerfilPet(id);

            if (produto == null)
            {
                return NotFound();
            }

            await _perfilPetService.Remover(id);

            if (!OperacaoValida()) return View(produto);

            TempData["Sucesso"] = "Produto excluido com sucesso!";

            return RedirectToAction("Index");
        }
        private async Task<PerfilPetViewModel> ObterPerfilPet(Guid id)
        {
            var perfil = _mapper.Map<PerfilPetViewModel>(await _perfilPetRepository.ObterPorId(id));
            return perfil;
        }

    }
}
