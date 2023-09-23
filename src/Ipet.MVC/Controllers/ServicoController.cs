
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EnterpriseStore.MVC.ViewModels;
using EnterpriseStore.MVC.Extensions;
using EnterpriseStore.Domain.Models;
using EnterpriseStore.Domain.Intefaces;
using Ipet.Domain.Models;
using EnterpriseStore.Service.Services;

namespace EnterpriseStore.MVC.Controllers
{
    [Authorize]
    public class ServicosController : BaseController
    {
        private readonly IServicoRepository _servicoRepository;
        private readonly IServicoService _servicoService;
        private readonly IMapper _mapper;

        public ServicosController(IServicoRepository servicoRepository, IMapper mapper,
                                  IServicoService servicoService,
                                  INotificador notificador) : base(notificador)
        {
            _servicoRepository = servicoRepository;
            _mapper = mapper;
            _servicoService = servicoService;
        }

        [AllowAnonymous]
        [Route("lista-de-servicos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ServicoViewModel>>(await _servicoRepository.ObterTodos()));
        }

        [AllowAnonymous]
        [Route("dados-do-servico/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterServico(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }


        [Route("novo-servico")]
        public async Task<IActionResult> Create()
        {

            return View();

        }


        [Route("novo-servico")]
        [HttpPost]
        public async Task<IActionResult> Create(ServicoViewModel servicoViewModel)
        {
            if (!ModelState.IsValid) return View(servicoViewModel);

            servicoViewModel.Imagem = "IMAGEM";
            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(servicoViewModel.ImagemUpload, imgPrefixo))
            {
                return View(servicoViewModel);
            }

            servicoViewModel.Imagem = imgPrefixo + servicoViewModel.ImagemUpload.FileName;
            await _servicoService.Adicionar(_mapper.Map<Servico>(servicoViewModel));

            if (!OperacaoValida()) return View(servicoViewModel);

            return RedirectToAction("Index");
        }


        [Route("editar-servico/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterServico(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }


        [Route("editar-servico/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ServicoViewModel servicoViewModel)
        {
            if (id != servicoViewModel.Id) return NotFound();

            var servicoAtualizacao = await ObterServico(id);
            servicoViewModel.Imagem = servicoAtualizacao.Imagem;
            if (!ModelState.IsValid) return View(servicoViewModel);

            if (servicoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(servicoViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(servicoViewModel);
                }

                servicoAtualizacao.Imagem = imgPrefixo + servicoViewModel.ImagemUpload.FileName;
            }

            servicoAtualizacao.Nome = servicoViewModel.Nome;
            servicoAtualizacao.Descricao = servicoViewModel.Descricao;
            servicoAtualizacao.Valor = servicoViewModel.Valor;
            servicoAtualizacao.Ativo = servicoViewModel.Ativo;

            await _servicoService.Atualizar(_mapper.Map<Servico>(servicoAtualizacao));

            if (!OperacaoValida()) return View(servicoViewModel);

            return RedirectToAction("Index");
        }

        [Route("excluir-servico/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterServico(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [Route("excluir-servico/{id:guid}")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var servico = await ObterServico(id);

            if (servico == null)
            {
                return NotFound();
            }

            await _servicoService.Remover(id);

            if (!OperacaoValida()) return View(servico);

            TempData["Sucesso"] = "Produto excluido com sucesso!";

            return RedirectToAction("Index");
        }

        private async Task<ServicoViewModel> ObterServico(Guid id)
        {
            var servico = _mapper.Map<ServicoViewModel>(await _servicoRepository.ObterPorId(id));
            return servico;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
