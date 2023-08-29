
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EnterpriseStore.MVC.ViewModels;
using EnterpriseStore.MVC.Extensions;
using EnterpriseStore.Domain.Intefaces;
using EnterpriseStore.Domain.Models;
using Ipet.Domain.Models;
using EnterpriseStore.Data.Repository;

namespace EnterpriseStore.MVC.Controllers
{
    //[Authorize]
    public class EstabelecimentoController : BaseController
    {
        private readonly IEstabelecimentoRepository _estabelecimentoRepository;
        private readonly IEstabelecimentoService _estabelecimentoService;
        private readonly IMapper _mapper;

        public EstabelecimentoController(IEstabelecimentoRepository estabelecimentoRepository, 
                                      IMapper mapper,
                                      IEstabelecimentoService estabelecimentoService,
                                      INotificador notificador) : base(notificador)
        {
            _estabelecimentoRepository = estabelecimentoRepository;
            _mapper = mapper;
            _estabelecimentoService = estabelecimentoService;
        }


        [AllowAnonymous]
        [Route("lista-de-estabelecimentos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<EstabelecimentoViewModel>>(await _estabelecimentoRepository.ObterTodos()));
        }


        [AllowAnonymous]
        [Route("dados-do-estabelecimento/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var fornecedorViewModel = await ObterEstabelecimento(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }


        [Route("novo-estabelecimento")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("novo-estabelecimento")]
        [HttpPost]
        public async Task<IActionResult> Create(EstabelecimentoViewModel estabelecimentoViewModel)
        {
            if (!ModelState.IsValid) return View(estabelecimentoViewModel);

            Guid guidValue = new Guid("18a00b3a-73a3-4045-9fc9-8a697eddb18c");
            estabelecimentoViewModel.Conta = guidValue;
            estabelecimentoViewModel.Imagem = "IMAGEM";

            var fornecedor = _mapper.Map<Estabelecimento>(estabelecimentoViewModel);
            await _estabelecimentoService.Adicionar(fornecedor);

            if (!OperacaoValida()) return View(estabelecimentoViewModel);

            return RedirectToAction("Index");
        }

        [Route("novo-estabelecimento_teste")]
        [HttpPost]
        public async Task<IActionResult> Create_2(EstabelecimentoViewModel estabelecimentoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(estabelecimentoViewModel);
            }

            estabelecimentoViewModel.Imagem = "IMAGEM";

            var estabelecimento = _mapper.Map<Estabelecimento>(estabelecimentoViewModel);
            await _estabelecimentoService.Adicionar(estabelecimento);

            if (!OperacaoValida())
            {
                return View(estabelecimentoViewModel);
            }

            return Ok(); // Você esqueceu dos parênteses para chamar o método Ok()
        }



        [Route("editar-estabelecimento/{id:guid}")]



        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterEstabelecimento(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [Route("editar-estabelecimento/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EstabelecimentoViewModel estabelecimentoViewModel)
        {
            if (id != estabelecimentoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(estabelecimentoViewModel);

            var fornecedor = _mapper.Map<Estabelecimento>(estabelecimentoViewModel);
            await _estabelecimentoService.Atualizar(fornecedor);

            return RedirectToAction("Index");
        }


        [Route("excluir-estabelecimento/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterEstabelecimento(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        
        [Route("excluir-estabelecimento/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedor = await ObterEstabelecimento(id);

            if (fornecedor == null) return NotFound();

            await _estabelecimentoService.Remover(id);

            if (!OperacaoValida()) return View(fornecedor);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("obter-estabelecimento/{id:guid}")]
        private async Task<EstabelecimentoViewModel> ObterEstabelecimento(Guid id)
        {
            var estabelecimento = _mapper.Map<EstabelecimentoViewModel>(await _estabelecimentoRepository.ObterPorId(id));
            return estabelecimento;

        }

    }
}
