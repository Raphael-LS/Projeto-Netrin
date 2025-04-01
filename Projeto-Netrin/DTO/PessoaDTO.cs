using System.ComponentModel.DataAnnotations;
using Projeto_Netrin.Models.Validators;

namespace Projeto_Netrin.DTO
{
    public class PessoaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome deve ser preenchido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF deve ser preenchido")]
        [CpfValidator(ErrorMessage = "CPF Inválido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A idade deve ser preenchido")]
        public int Idade { get; set; }
    }
}
