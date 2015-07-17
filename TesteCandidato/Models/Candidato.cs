using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteCandidato.Models
{
    [Table("Candidatoes")]
    public class Candidato
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "O {0} precisa ter tamanho entre {1} e {2} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "A {0} precisa ter tamanho entre {1} e {2} caracteres.", MinimumLength = 3)]
        public string Cidade { get; set; }
    }
}