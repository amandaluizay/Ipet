
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ipet.Domain.Models;
using Ipet.ViewModels;
using Ipet.Domain.Intefaces;
using Ipet.MVC.Extensions;
using Microsoft.AspNetCore.Identity;
using Ipet.MVC.Models;
using Ipet.Data.Repository;
using Ipet.Service.Services;
using Microsoft.AspNetCore.Routing;

namespace Ipet.MVC.Controllers
{
    [Authorize]
    public class ServicosController : BaseController
    {
        private readonly IServiçoHashtagRepository _servicoHashtagRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IPerfilPetRepository _perfilPetRepository;
        private readonly IPerfilPetService _perfilPetService;
        private readonly IServicoService _servicoService;

        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServicosController(IServicoRepository servicoRepository, 
                                  IMapper mapper,
                                  IServiçoHashtagRepository servicoHashtagRepository,
                                  IPerfilPetRepository perfilPetRepository,
                                  IPerfilPetService perfilPetService,
                                  UserManager<ApplicationUser> userManager,
                                  IServicoService servicoService,
                                  INotificador notificador) : base(notificador)
        {
            _servicoHashtagRepository = servicoHashtagRepository;
            _servicoRepository = servicoRepository;
            _mapper = mapper;
            _servicoService = servicoService;
            _perfilPetRepository = perfilPetRepository;
            _perfilPetService = perfilPetService;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [Route("lista-de-servicos")]
        public async Task<IActionResult> Index(string tags, string tipoAnimal, string raca, string porte)
        {
            var selectedTags = new List<string>();
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var perfilPet = await _perfilPetRepository.ObterPerfilUsuario(Guid.Parse(user.Id));

                if (perfilPet != null)
                {
                    ViewBag.NomePet = perfilPet.Nome;
                    ViewBag.TipoAnimal = perfilPet.TipoAnimal;
                    ViewBag.Raca = perfilPet.Raca;
                    ViewBag.Porte = perfilPet.Porte;
                }

            }
            if (tipoAnimal == "on")
            {
                selectedTags.Add(ViewBag.TipoAnimal);
            }
            if (raca == "on")
            {
                selectedTags.Add(ViewBag.Raca);
            }
            if (porte == "on")
            {
                selectedTags.Add(ViewBag.Porte);
            }
            if (!string.IsNullOrEmpty(tags))
            {
                string cleanedTags = tags.Trim().ToUpper();
                string[] tagArray = cleanedTags.Split(',');
                selectedTags.AddRange(tagArray);
            }
            if (selectedTags.Count > 0)
            {
                var servicos = await _servicoService.GetServicosByTags(selectedTags.ToArray());
                return View(_mapper.Map<IEnumerable<ServicoViewModel>>(servicos));
            }
            if (user != null)
            {
                var perfilPet = await _perfilPetRepository.ObterPerfilUsuario(Guid.Parse(user.Id));

                if (perfilPet != null)
                {
                    ViewBag.NomePet = perfilPet.Nome;
                    ViewBag.TipoAnimal = perfilPet.TipoAnimal;
                    ViewBag.Raca = perfilPet.Raca;
                    ViewBag.Porte = perfilPet.Porte;
                }
            }
            return View(_mapper.Map<IEnumerable<ServicoViewModel>>(await _servicoRepository.ObterTodos()));
        }
        [AllowAnonymous]
        [Route("dados-do-servico/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var servicoViewModel = await ObterServico(id);

            if (servicoViewModel == null)
            {
                return NotFound();
            }

            var servicoHashtags = await _servicoHashtagRepository.ObterPorServicoId(servicoViewModel.Id);
            servicoViewModel.Hashtags = _mapper.Map<List<ServiçoHashtagViewModel>>(servicoHashtags);
            servicoViewModel.HashtagsInput = string.Join(", ", servicoViewModel.Hashtags);

            var user = _userManager.FindByIdAsync(servicoViewModel.EstabelecimentoId.ToString());
            ViewBag.NomeDoUsuario = user;
            return View(servicoViewModel);
        }
        [ClaimsAuthorize("Usuario", "2")]
        [Route("novo-servico")]
        public async Task<IActionResult> Create()
        {

            return View();

        }
        [ClaimsAuthorize("Usuario", "2")]
        [Route("novo-servico")]
        [HttpPost]
        public async Task<IActionResult> Create(ServicoViewModel servicoViewModel)
        {
            var hashtagStrings = servicoViewModel.HashtagsInput.Split(',').Select(tag => tag.Trim());
            servicoViewModel.Hashtags = hashtagStrings.Select(tag => new ServiçoHashtagViewModel { Tag = tag }).ToList();

            if (!ModelState.IsValid) return View(servicoViewModel);

            servicoViewModel.Imagem = "IMAGEM";
            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(servicoViewModel.ImagemUpload, imgPrefixo))
            {
                return View(servicoViewModel);
            }

            servicoViewModel.Imagem = servicoViewModel.ImagemUpload.ContentType + ";base64," + ConvertImagemToBase64(servicoViewModel.ImagemUpload);

            //user 
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                servicoViewModel.EstabelecimentoId = Guid.Parse(user.Id);
                servicoViewModel.Estabelecimento = user.Nome;
            }
            else
            {
                return View(servicoViewModel);
            }

            await _servicoService.Adicionar(_mapper.Map<Servico>(servicoViewModel));

            if (!OperacaoValida()) return View(servicoViewModel);

            return RedirectToAction("Index");
        }
        [ClaimsAuthorize("Usuario", "2")]
        [Route("editar-servico/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var servicoViewModel = await ObterServico(id);

            if (servicoViewModel == null)
            {
                return NotFound();
            }

            return View(servicoViewModel);
        }
        [ClaimsAuthorize("Usuario", "2")]
        [Route("editar-servico/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ServicoViewModel servicoViewModel)
        {
            var hashtagStrings = servicoViewModel.HashtagsInput.Split(',').Select(tag => tag.Trim());
            servicoViewModel.Hashtags = hashtagStrings.Select(tag => new ServiçoHashtagViewModel { Tag = tag }).ToList();

            if (id != servicoViewModel.Id) return NotFound();

            var servicoAtualizacao = await ObterServico(id);
            if (!ModelState.IsValid) return View(servicoViewModel);

            if (servicoViewModel.ImagemUpload != null)
            {
                servicoViewModel.Imagem = "IMAGEM";
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(servicoViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(servicoViewModel);
                }


                servicoAtualizacao.Imagem = servicoViewModel.ImagemUpload.ContentType + ";base64," + ConvertImagemToBase64(servicoViewModel.ImagemUpload);
            }

            servicoAtualizacao.Nome = servicoViewModel.Nome;
            servicoAtualizacao.Descricao = servicoViewModel.Descricao;
            servicoAtualizacao.Valor = servicoViewModel.Valor;
            servicoAtualizacao.Ativo = servicoViewModel.Ativo;
            servicoAtualizacao.Hashtags = servicoViewModel.Hashtags;

            await _servicoService.Atualizar(_mapper.Map<Servico>(servicoAtualizacao));

            if (!OperacaoValida()) return View(servicoViewModel);

            return RedirectToAction("Index");
        }
        [ClaimsAuthorize("Usuario", "2")]
        [Route("excluir-servico/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var servico = await ObterServico(id);

            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }
        [ClaimsAuthorize("Usuario", "2")]
        [Route("excluir-servico/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var servico = await ObterServico(id);

            if (servico == null)
            {
                return NotFound();
            }
            await _servicoHashtagRepository.ExcluirTagsDoServico(id);
            await _servicoService.Remover(id);

            if (!OperacaoValida()) return View(servico);

            TempData["Sucesso"] = "Servico excluido com sucesso!";

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

    }
}
