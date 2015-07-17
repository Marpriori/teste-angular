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
    public class CandidatoTecnologiaController : ApiController
    {
        private TesteCandidatoContext db = new TesteCandidatoContext();

        // GET api/CandidatoTecnologia
        public IQueryable<CandidatoTecnologia> GetCandidatoTecnologias()
        {
            return db.CandidatoTecnologias;
        }

        // GET api/CandidatoTecnologia/5
        [HttpGet]
        [Route("api/CandidatoTecnologias/{CandidatoID}")]
        [ResponseType(typeof(IQueryable<CandidatoTecnologia>))]
        public IHttpActionResult GetCandidatoTecnologia(int CandidatoID)
        {
            IQueryable<CandidatoTecnologia> candidatotecnologia = db.CandidatoTecnologias.Where(r => r.CandidatoID == CandidatoID);
            if (candidatotecnologia == null)
            {
                return NotFound();
            }
            foreach (CandidatoTecnologia v in candidatotecnologia)
            {
                v.Tecnologia = db.Tecnologias.Find(v.TecnologiaID);
            }

            return Ok(candidatotecnologia);
        }

        // PUT api/CandidatoTecnologia/5
        public IHttpActionResult PutCandidatoTecnologia(int id, CandidatoTecnologia candidatotecnologia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != candidatotecnologia.Id)
            {
                return BadRequest();
            }

            db.Entry(candidatotecnologia).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatoTecnologiaExists(id))
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

        // POST api/CandidatoTecnologia
        [ResponseType(typeof(CandidatoTecnologia))]
        public IHttpActionResult PostCandidatoTecnologia(CandidatoTecnologia candidatotecnologia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (CandidatoTecnologiaExists(candidatotecnologia.CandidatoID, candidatotecnologia.TecnologiaID))
                return BadRequest("Tecnologia já registrada.");

            db.CandidatoTecnologias.Add(candidatotecnologia);
            db.SaveChanges();
            candidatotecnologia.Tecnologia = db.Tecnologias.Find(candidatotecnologia.TecnologiaID);
            return CreatedAtRoute("DefaultApi", new { id = candidatotecnologia.Id }, candidatotecnologia);
        }

        // DELETE api/CandidatoTecnologia/5
        [ResponseType(typeof(CandidatoTecnologia))]
        public IHttpActionResult DeleteCandidatoTecnologia(int id)
        {
            CandidatoTecnologia candidatotecnologia = db.CandidatoTecnologias.Find(id);
            if (candidatotecnologia == null)
            {
                return NotFound();
            }

            db.CandidatoTecnologias.Remove(candidatotecnologia);
            db.SaveChanges();

            return Ok(candidatotecnologia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CandidatoTecnologiaExists(int id)
        {
            return db.CandidatoTecnologias.Count(e => e.Id == id) > 0;
        }
        private bool CandidatoTecnologiaExists(int idCandidato, int idTecnologia)
        {
            return db.CandidatoTecnologias.Count(e => e.CandidatoID == idCandidato && e.TecnologiaID == idTecnologia) > 0;
        }
    }
}