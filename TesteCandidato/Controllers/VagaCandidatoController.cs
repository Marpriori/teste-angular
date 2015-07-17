using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TesteCandidato.Models;

namespace TesteCandidato.Controllers
{
    public class VagaCandidatoController : ApiController
    {
        private TesteCandidatoContext db = new TesteCandidatoContext();

        // GET api/VagaCandidato
        public IQueryable<VagaCandidato> GetVagaCandidatoes()
        {
            return db.VagaCandidatoes;
        }
        
        // GET api/VagaCandidato/5
        [HttpGet]
        [Route("api/VagaCandidatos/{VagaID}")]
        [ResponseType(typeof(IQueryable<VagaCandidato>))]
        public IHttpActionResult GetVagaCandidato(int VagaID)
        {
            IQueryable<VagaCandidato> vagacandidato = db.VagaCandidatoes.Where(r => r.VagaID == VagaID);
            if (vagacandidato == null)
            {
                return NotFound();
            }
            foreach (VagaCandidato v in vagacandidato)
            {
                v.Candidato = db.Candidatoes.Find(v.CandidatoID);
                v.Rank = GetRank(VagaID, v.CandidatoID);
            }

            return Ok(vagacandidato.OrderByDescending(r=> r.Rank));
        }



        // PUT api/VagaCandidato/5
        public IHttpActionResult PutVagaCandidato(int id, VagaCandidato vagacandidato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vagacandidato.Id)
            {
                return BadRequest();
            }

            db.Entry(vagacandidato).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VagaCandidatoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/VagaCandidato
        [ResponseType(typeof(VagaCandidato))]
        public IHttpActionResult PostVagaCandidato(VagaCandidato vagacandidato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (VagaCandidatoExists(vagacandidato.VagaID, vagacandidato.CandidatoID))
                return BadRequest("Candidato já está concorrendo a vaga.");

            db.VagaCandidatoes.Add(vagacandidato);
            db.SaveChanges();
            vagacandidato.Candidato = db.Candidatoes.Find(vagacandidato.CandidatoID);
            return CreatedAtRoute("DefaultApi", new { id = vagacandidato.Id }, vagacandidato);
        }

        // DELETE api/VagaCandidato/5
        [ResponseType(typeof(VagaCandidato))]
        public IHttpActionResult DeleteVagaCandidato(int id)
        {
            VagaCandidato vagacandidato = db.VagaCandidatoes.Find(id);
            if (vagacandidato == null)
            {
                return NotFound();
            }

            db.VagaCandidatoes.Remove(vagacandidato);
            db.SaveChanges();

            return Ok(vagacandidato);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VagaCandidatoExists(int id)
        {
            return db.VagaCandidatoes.Count(e => e.Id == id) > 0;
        }
        private bool VagaCandidatoExists(int idVaga, int idCandidato)
        {
            return db.VagaCandidatoes.Count(e => e.VagaID == idVaga && e.CandidatoID == idCandidato) > 0;
        }

        private int GetRank(int idVaga, int idCandidato)
        {
            int rank = 0;
            string sql = string.Format(
                @"select sum(vt.Peso * coalesce(ct.Conhecimento,0))
                from Candidatoes c
                    left join VagasCandidatos vc on (vc.CandidatoID = c.Id)
                    left join VagasTecnologias vt on (vt.VagaID = vc.VagaID)
                    left join CandidatoesTecnologias ct on (ct.CandidatoID = c.Id and vt.TecnologiaID = ct.TecnologiaID)
                where vc.VagaID = {0} and c.Id = {1}", idVaga,idCandidato);
            foreach (int _r in db.Database.SqlQuery<int>(sql))
            {
                rank = _r;
            }

            return rank;

        }
    }
}