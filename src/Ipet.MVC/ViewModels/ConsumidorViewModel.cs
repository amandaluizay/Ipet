using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseStore.MVC.ViewModels
{
    public class ConsumidorViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Consumidor")]
        public Guid Conta { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Consumidor")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("CNPJ")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Ativo?")]

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
    }
}
