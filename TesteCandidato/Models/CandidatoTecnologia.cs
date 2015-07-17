using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteCandidato.Models
{
     [Table("CandidatoesTecnologias")]
    public class CandidatoTecnologia
    {

         [Key]
         [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
         public int Id { get; set; }

        [Required]
        public int CandidatoID { get; set; }
        
        public virtual Candidato Candidato { get; set; }

        [Required]
        public int TecnologiaID { get; set; }
        
        public virtual Tecnologia Tecnologia { get; set; }

        [Required]
        public PesoGrau Conhecimento { get; set; } 
    }
}