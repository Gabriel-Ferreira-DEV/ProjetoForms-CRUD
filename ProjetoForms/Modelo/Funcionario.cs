using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjetoForms.Modelo
{
    public class Funcionario
    {
        public int Id { get; set; }

        [Required] //torna o campo nome obrigatorio
        [StringLength(70, MinimumLength = 5)] // atribui um caracter maximo e um caracter minimo
        public string Nome { get; set; }

        [Required, EmailAddress, StringLength(70, MinimumLength = 5)] // validação
        public string Email { get; set; }
        
        public string Sexo { get; set; }

        [Required]
        public double Salario { get; set; }
        [Required]
        public string TipoContrato { get; set; }
        public DateTime DataCadastro { get; set; }
        public Nullable<DateTime>DataAtualizacao { get; set; }

    }
}
