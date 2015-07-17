using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteCandidato.Models
{
    [Table("VagasCandidatos")]
    public class VagaCandidato
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int VagaID { get; set; }
       
        public virtual Vaga Vaga { get; set; }

        [Required]
        public int CandidatoID { get; set; }
      
        public virtual Candidato Candidato { get; set; }

        public int Rank { get; set; }

    }
}