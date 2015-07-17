using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteCandidato.Models
{
    public enum PesoGrau
    {
    
    Baixo = 1,
    [Display(Name = "Médio")]
    Medio = 2,
 
    Alto = 3
    }
}