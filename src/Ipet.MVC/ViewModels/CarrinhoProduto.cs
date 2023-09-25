using EnterpriseStore.Domain.Models;
using EnterpriseStore.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ipet.Domain.Models
{
    public class CarrinhoProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CarrinhoId { get; set; }
        public Guid ProdutoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Display(Name = "Produto")]
        public ProdutoViewModel Produto { get; set; }
    }
}
