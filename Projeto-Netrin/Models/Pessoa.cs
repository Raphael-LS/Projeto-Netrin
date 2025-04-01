using Microsoft.Build.Framework;
using Projeto_Netrin.Models.Validators;

namespace Projeto_Netrin.Models
{
    public class Pessoa
    {
        public Pessoa()
        {
        }

        public Pessoa(string nome, int idade, string cpf)
        {
            Nome = nome;
            Idade = idade;
            CPF = FormatCPF(cpf);
        }

        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int Idade { get; set; }

        [Required]
        public long CPF { get; set; }

        private long FormatCPF(string cpf) 
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            return Convert.ToInt64(cpf);
        }

    }
}
