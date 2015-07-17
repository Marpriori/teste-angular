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
    public class CandidatoController : ApiController
    {
        private TesteCandidatoContext db = new TesteCandidatoContext();

        // GET api/Candidato
        public IQueryable<Candidato> GetCandidatoes()
        {
            return db.Candidatoes;
        }

        // GET api/Candidato/5
        [ResponseType(typeof(Candidato))]
        public IHttpActionResult GetCandidato(int id)
        {
            Candidato candidato = db.Candidatoes.Find(id);
            if (candidato == null)
            {
                return NotFound();
            }

            return Ok(candidato);
        }

        [HttpPost]
        public IHttpActionResult PutCandidato(int id, Candidato candidato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != candidato.Id)
            {
                return BadRequest();
            }

            db.Entry(candidato).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatoExists(id))
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

        // POST api/Candidato
        [ResponseType(typeof(Candidato))]
        public IHttpActionResult PostCandidato(Candidato candidato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (candidato.Id > 0 && CandidatoExists(candidato.Id))
                return PutCandidato(candidato.Id, candidato);

            db.Candidatoes.Add(candidato);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = candidato.Id }, candidato);
        }

        // DELETE api/Candidato/5
        [ResponseType(typeof(Candidato))]
        public IHttpActionResult DeleteCandidato(int id)
        {
            Candidato candidato = db.Candidatoes.Find(id);
            if (candidato == null)
            {
                return NotFound();
            }

            db.Candidatoes.Remove(candidato);
            db.SaveChanges();

            return Ok(candidato);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CandidatoExists(int id)
        {
            return db.Candidatoes.Count(e => e.Id == id) > 0;
        }
    }
}