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
    public class VagaTecnologiaController : ApiController
    {
        private TesteCandidatoContext db = new TesteCandidatoContext();

        // GET api/VagaTecnologia/{ID Vaga}
        [HttpGet]
        public IQueryable<VagaTecnologia> GetVagaTecnologias()
        {
            return db.VagaTecnologias;
        }

        // GET api/VagaTecnologia/5
        [HttpGet]
        [Route("api/VagaTecnologias/{VagaID}")]
        [ResponseType(typeof(IQueryable<VagaTecnologia>))]
        public IHttpActionResult GetVagaTecnologia(int VagaID)
        {
            IQueryable<VagaTecnologia> vagatecnologia = db.VagaTecnologias.Where(r => r.VagaID == VagaID);
            if (vagatecnologia == null)
            {
                return NotFound();
            }
            foreach (VagaTecnologia v in vagatecnologia)
            {
                v.Tecnologia = db.Tecnologias.Find(v.TecnologiaID);
            }

            return Ok(vagatecnologia);
        }

        // PUT api/VagaTecnologia/5
        public IHttpActionResult PutVagaTecnologia(int id, VagaTecnologia vagatecnologia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vagatecnologia.Id)
            {
                return BadRequest();
            }

            db.Entry(vagatecnologia).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VagaTecnologiaExists(id))
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

        // POST api/VagaTecnologia
        [ResponseType(typeof(VagaTecnologia))]
        public IHttpActionResult PostVagaTecnologia(VagaTecnologia vagatecnologia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (VagaTecnologiaExists(vagatecnologia.VagaID, vagatecnologia.TecnologiaID))
                return BadRequest("Tecnologia já registrada.");

            db.VagaTecnologias.Add(vagatecnologia);
            db.SaveChanges();
            vagatecnologia.Tecnologia = db.Tecnologias.Find(vagatecnologia.TecnologiaID);
            return CreatedAtRoute("DefaultApi", new { id = vagatecnologia.Id }, vagatecnologia);
        }

        // DELETE api/VagaTecnologia/5
        [ResponseType(typeof(VagaTecnologia))]
        public IHttpActionResult DeleteVagaTecnologia(int id)
        {
            VagaTecnologia vagatecnologia = db.VagaTecnologias.Find(id);
            if (vagatecnologia == null)
            {
                return NotFound();
            }

            db.VagaTecnologias.Remove(vagatecnologia);
            db.SaveChanges();

            return Ok(vagatecnologia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VagaTecnologiaExists(int id)
        {
            return db.VagaTecnologias.Count(e => e.Id == id) > 0;
        }
        private bool VagaTecnologiaExists(int idVaga, int idTecnologia)
        {
            return db.VagaTecnologias.Count(e => e.VagaID == idVaga && e.TecnologiaID == idTecnologia) > 0;
        }
    }
}