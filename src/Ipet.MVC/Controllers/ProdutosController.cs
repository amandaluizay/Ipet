
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EnterpriseStore.MVC.ViewModels;
using EnterpriseStore.MVC.Extensions;
using EnterpriseStore.Domain.Models;
using EnterpriseStore.Domain.Intefaces;
using Ipet.Domain.Models;
using EnterpriseStore.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Ipet.MVC.Models;
using Ipet.MVC.Areas.Identity.Pages.Account;
using System.Buffers.Text;

namespace EnterpriseStore.MVC.Controllers
{
    [Authorize]
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager; 

        public ProdutosController(IProdutoRepository produtoRepository,
        IMapper mapper, 
                                  IProdutoService produtoService,
                                  UserManager<ApplicationUser> userManager,
                                  INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _produtoService = produtoService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos()));
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
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return View(produtoViewModel);

            produtoViewModel.Imagem = "IMAGEM";
            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
            {
                return View(produtoViewModel);
            }

            produtoViewModel.Imagem = produtoViewModel.ImagemUpload.ContentType + ";base64," + ConvertImagemToBase64(produtoViewModel.ImagemUpload);

            //user 
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                produtoViewModel.EstabelecimentoId = Guid.Parse(user.Id);
                produtoViewModel.Estabelecimento = user.Nome;
            }
            else
            {
                // Trate o caso em que o usuário não está autenticado
                return View(produtoViewModel);
            }
            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Usuario", "2")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Usuario", "2")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizacao = await ObterProduto(id);
            //produtoViewModel.Estabelecimento = produtoAtualizacao.Estabelecimento;
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;
            if (!ModelState.IsValid) return View(produtoViewModel);

            if (produtoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(produtoViewModel);
                }

                produtoAtualizacao.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            if (!OperacaoValida()) return View(produtoViewModel);

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
            var produto = await ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            await _produtoService.Remover(id);

            if (!OperacaoValida()) return View(produto);

            TempData["Sucesso"] = "Produto excluido com sucesso!";

            return RedirectToAction("Index");
        }
        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
            return produto;
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

        public string ConvertImagemToBase64(IFormFile imagemFile)
        {
            if (imagemFile == null || imagemFile.Length == 0)
            {
                // Lida com a imagem ausente ou vazia, se necessário.
                return null;
            }

            using (var ms = new MemoryStream())
            {
                imagemFile.CopyTo(ms);
                byte[] imagemBytes = ms.ToArray();
                string imagemBase64 = Convert.ToBase64String(imagemBytes);
                return imagemBase64;
            }
        }

    }
}
