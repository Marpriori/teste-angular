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
    public class VagaController : ApiController
    {
        private TesteCandidatoContext db = new TesteCandidatoContext();

        // GET api/Vaga
        public IQueryable<Vaga> GetVagas()
        {
            return db.Vagas;
        }

        // GET api/Vaga/5
        [ResponseType(typeof(Vaga))]
        public IHttpActionResult GetVaga(int id)
        {
            Vaga vaga = db.Vagas.Find(id);
            if (vaga == null)
            {
                return NotFound();
            }

            return Ok(vaga);
        }

        [AcceptVerbs("POST")]
        public IHttpActionResult PutVaga(int id, Vaga vaga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vaga.Id)
            {
                return BadRequest();
            }

            db.Entry(vaga).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VagaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST api/Vaga
        [ResponseType(typeof(Vaga))]
        public IHttpActionResult PostVaga(Vaga vaga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (vaga.Id > 0 && VagaExists(vaga.Id))
                return PutVaga(vaga.Id, vaga);

            db.Vagas.Add(vaga);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vaga.Id }, vaga);
        }

        // DELETE api/Vaga/5
        [ResponseType(typeof(Vaga))]
        public IHttpActionResult DeleteVaga(int id)
        {
            Vaga vaga = db.Vagas.Find(id);
            if (vaga == null)
            {
                return NotFound();
            }

            db.Vagas.Remove(vaga);
            db.SaveChanges();

            return Ok(vaga);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VagaExists(int id)
        {
            return db.Vagas.Count(e => e.Id == id) > 0;
        }
    }
}