using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TesteCandidato.Models
{
    public class TesteCandidatoContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TesteCandidatoContext() : base("name=TesteCandidatoContext")
        {
        }

        public System.Data.Entity.DbSet<TesteCandidato.Models.Vaga> Vagas { get; set; }

        public System.Data.Entity.DbSet<TesteCandidato.Models.Tecnologia> Tecnologias { get; set; }

        public System.Data.Entity.DbSet<TesteCandidato.Models.Candidato> Candidatoes { get; set; }

        public System.Data.Entity.DbSet<TesteCandidato.Models.VagaTecnologia> VagaTecnologias { get; set; }

        public System.Data.Entity.DbSet<TesteCandidato.Models.VagaCandidato> VagaCandidatoes { get; set; }

        public System.Data.Entity.DbSet<TesteCandidato.Models.CandidatoTecnologia> CandidatoTecnologias { get; set; }
    
    }
}
