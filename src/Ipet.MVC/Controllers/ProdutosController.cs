
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
using Microsoft.EntityFrameworkCore;

namespace Ipet.MVC.Controllers
{
    [Authorize]
    public class ProdutosController : BaseController
    {
        private readonly IProdutoHashtagRepository _produtoHashtagRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICarrinhoService _carrinhoService;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager; 

        public ProdutosController(IProdutoRepository produtoRepository,
        IProdutoHashtagRepository produtoHashtagRepository,
        IMapper mapper, ICarrinhoService carrinhoService,
        IProdutoService produtoService,
        UserManager<ApplicationUser> userManager,
        INotificador notificador) : base(notificador)
        {
            _produtoHashtagRepository = produtoHashtagRepository;
            _carrinhoService = carrinhoService;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _produtoService = produtoService;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index(string tags)
        {
            if (!string.IsNullOrEmpty(tags))
            {
                string cleanedTags = tags.Trim().ToUpper();
                string[] tagArray = cleanedTags.Split(',');
                var produtos = await _produtoService.GetProdutosByTags(tagArray);
                return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(produtos));
            }

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

            var produtoHashtags = await _produtoHashtagRepository.ObterPorProdutoId(produtoViewModel.Id);
            produtoViewModel.Hashtags = _mapper.Map<List<ProdutoHashtagViewModel>>(produtoHashtags);
            produtoViewModel.HashtagsInput = string.Join(", ", produtoViewModel.Hashtags);



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

            var hashtagStrings = produtoViewModel.HashtagsInput.Split(',').Select(tag => tag.Trim());
            produtoViewModel.Hashtags = hashtagStrings.Select(tag => new ProdutoHashtagViewModel { Tag = tag }).ToList();



            if (!ModelState.IsValid) return View(produtoViewModel);

            produtoViewModel.Imagem = "IMAGEM";
            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
            {
                return View(produtoViewModel);
            }

            produtoViewModel.Imagem = produtoViewModel.ImagemUpload.ContentType + ";base64," + ConvertImagemToBase64(produtoViewModel.ImagemUpload);
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                produtoViewModel.EstabelecimentoId = Guid.Parse(user.Id);
                produtoViewModel.Estabelecimento = user.Nome;
            }
            else
            {
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

            var produtoHashtags = await _produtoHashtagRepository.ObterPorProdutoId(produtoViewModel.Id);
            produtoViewModel.Hashtags = _mapper.Map<List<ProdutoHashtagViewModel>>(produtoHashtags);


            produtoViewModel.HashtagsInput = string.Join(", ", produtoViewModel.Hashtags.Select(h => h.Tag));


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

            var hashtagStrings = produtoViewModel.HashtagsInput.Split(',').Select(tag => tag.Trim());
            produtoViewModel.Hashtags = hashtagStrings.Select(tag => new ProdutoHashtagViewModel { Tag = tag }).ToList();

            if (!ModelState.IsValid) return View(produtoViewModel);

            if (produtoViewModel.ImagemUpload != null)
            {
                produtoAtualizacao.Imagem = "IMAGEM";
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(produtoAtualizacao);
                }

                produtoAtualizacao.Imagem = produtoViewModel.ImagemUpload.ContentType + ";base64," + ConvertImagemToBase64(produtoViewModel.ImagemUpload);
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;
            produtoAtualizacao.Hashtags = produtoViewModel.Hashtags;


            await _produtoHashtagRepository.ExcluirTagsDoProduto(id);
            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction("Index");
        }
        [ClaimsAuthorize("Usuario", "2")]
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);
            var produtoHashtags = await _produtoHashtagRepository.ObterPorProdutoId(produtoViewModel.Id);
            produtoViewModel.Hashtags = _mapper.Map<List<ProdutoHashtagViewModel>>(produtoHashtags);


            produtoViewModel.HashtagsInput = string.Join(", ", produtoViewModel.Hashtags);


            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
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

            await _produtoHashtagRepository.ExcluirTagsDoProduto(id);
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
        [ClaimsAuthorize("Usuario", "1")]
        [Route("carrinho/{id:guid}")]
        [HttpPost, ActionName("Carrinho")]
        public async Task<IActionResult> Carrinho(Guid id)
        {
            var produtoViewModel = await _produtoRepository.ObterPorId(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            Guid U = Guid.Parse("00");
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                U = Guid.Parse(user.Id);
            }
            else
            {
                return View(produtoViewModel);
            }

            int quantidade = 0;

            bool produtoAdicionado = await _carrinhoService.AdicionarProduto(U, produtoViewModel.Id, quantidade);

            if (!produtoAdicionado)
            {

                return View(produtoViewModel);
            }

            return RedirectToAction("Index");
        }
    }
}
