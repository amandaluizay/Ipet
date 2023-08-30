// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseStore.MVC.Controllers;
using EnterpriseStore.MVC.ViewModels;
using Ipet.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ipet.MVC.Areas.Identity.Pages.Account
{
    public class RegisterEstabelecimentoModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterEstabelecimentoModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IWebHostEnvironment hostingEnvironment, 
            IHttpContextAccessor httpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [DisplayName("Estabelecimento")]
            public string Nome { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [DisplayName("Endereço")]
            public string Endereco { get; set; }

            [DisplayName("Estampa do Estabelecimento")]
            public IFormFile ImagemUpload { get; set; }
            public string Imagem { get; set; }

            [Required]
            [StringLength(19, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 11)]
            [DataType(DataType.Password)]
            [Display(Name = "CNPJ")]
            public string Cnpj { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }
        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var estabelecimento = new EstabelecimentoViewModel
            {
                //Imagem = "",
                Nome = Input.Nome,
                //Documento = Input.Cnpj,
                //Endereco = Input.Endereco,
                //ImagemUpload = Input.ImagemUpload,
                //Ativo = true, // Ou false, dependendo do que deseja
                //DataCadastro = DateTime.Now // Definir a data de cadastro
                
            };

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Adicionar a claim personalizada ao usuário
                    var claim = new Claim("Usuario", "2");
                    await _userManager.AddClaimAsync(user, claim);
                    // Cria Estabelecimento

                    Guid.TryParse(user.Id, out Guid guid);
                    estabelecimento.Conta = guid;

                    var baseUrl = GetBaseUrl();
                    string create2Url = baseUrl + "/novo-estabelecimento_teste";

                    // Preparar os dados do estabelecimento como JSON
                    var estabelecimentoJson = JsonConvert.SerializeObject(estabelecimento);
                    var postData = Encoding.UTF8.GetBytes(estabelecimentoJson);

                    // Criar a solicitação HTTP
                    var request = (HttpWebRequest)WebRequest.Create(create2Url);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = postData.Length;

                    // Enviar os dados
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(postData, 0, postData.Length);
                    }

                    // Receber a resposta
                    using (var response = (HttpWebResponse)request.GetResponse())
                    using (var responseStream = response.GetResponseStream())
                    using (var reader = new StreamReader(responseStream))
                    {
                        var responseText = reader.ReadToEnd();
                        var teste = 0;
                    }

                    //produtoViewModel = await PopularFornecedores(produtoViewModel);
                    //if (!ModelState.IsValid) return View(produtoViewModel);

                    //var imgPrefixo = Guid.NewGuid() + "_";
                    //if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                    //{
                    //    return View(produtoViewModel);
                    //}

                    //produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
                    //await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));






                    // Gerar o token de confirmação
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
