using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteCandidato.Models
{
    [Table("Vagas")]
    public class Vaga
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "A {0} precisa ter tamanho entre {1} e {2} caracteres.", MinimumLength = 1)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Range(1,10000)]
        [Display(Name="Quantidade Vagas")]
        public int QuantidadeVagas { get; set; }
    }
}