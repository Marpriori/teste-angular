using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteCandidato.Models
{
    [Table("VagasTecnologias")]
    public class VagaTecnologia
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }

        [Required]
        
        public int VagaID { get; set; }
        
        public virtual Vaga Vaga { get; set; }
       
        [Required]
        
        public int TecnologiaID { get; set; }
        
        public virtual Tecnologia Tecnologia { get; set; }

        [Required]
        public PesoGrau Peso { get; set; } 
    }
}