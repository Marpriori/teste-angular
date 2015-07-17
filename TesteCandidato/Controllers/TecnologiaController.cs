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
    public class TecnologiaController : ApiController
    {
        private TesteCandidatoContext db = new TesteCandidatoContext();

        // GET api/Tecnologia
        public IQueryable<Tecnologia> GetTecnologias()
        {
            return db.Tecnologias;
        }

        // GET api/Tecnologia/5
        [ResponseType(typeof(Tecnologia))]
        public IHttpActionResult GetTecnologia(int id)
        {
            Tecnologia tecnologia = db.Tecnologias.Find(id);
            if (tecnologia == null)
            {
                return NotFound();
            }

            return Ok(tecnologia);
        }

        // PUT api/Tecnologia/5
        [HttpPost]
        public IHttpActionResult PutTecnologia(int id, Tecnologia tecnologia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tecnologia.Id)
            {
                return BadRequest();
            }

            db.Entry(tecnologia).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TecnologiaExists(id))
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

        // POST api/Tecnologia
        [ResponseType(typeof(Tecnologia))]
        public IHttpActionResult PostTecnologia(Tecnologia tecnologia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (tecnologia.Id > 0 && TecnologiaExists(tecnologia.Id))
                return PutTecnologia(tecnologia.Id, tecnologia);

            db.Tecnologias.Add(tecnologia);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tecnologia.Id }, tecnologia);
        }

        // DELETE api/Tecnologia/5
        [ResponseType(typeof(Tecnologia))]
        public IHttpActionResult DeleteTecnologia(int id)
        {
            Tecnologia tecnologia = db.Tecnologias.Find(id);
            if (tecnologia == null)
            {
                return NotFound();
            }

            db.Tecnologias.Remove(tecnologia);
            db.SaveChanges();

            return Ok(tecnologia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TecnologiaExists(int id)
        {
            return db.Tecnologias.Count(e => e.Id == id) > 0;
        }
    }
}