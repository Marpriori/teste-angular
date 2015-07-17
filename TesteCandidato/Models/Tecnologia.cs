using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteCandidato.Models
{
    [Table("Tecnologias")]
    public class Tecnologia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "A {0} precisa ter tamanho entre {1} e {2} caracteres.", MinimumLength = 1)]
        [Display(Name="Descrição")]
        public string Descricao { get; set; }

    }
}