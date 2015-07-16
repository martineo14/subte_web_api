using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiSubte.Models;
using WebApiSubte.Utils;

namespace WebApiSubte.Controllers
{
    public class LineaSubteModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LineaSubteModels
        public List<LineaSubteModel> GetLineaSubteModels()
        {
          List<LineaSubteModel> lstLineaSubteModels = new List<LineaSubteModel>();
          WebClient w = new WebClient();
          string s = w.DownloadString("http://www.metrovias.com.ar");
          foreach (LineaSubteModel i in EstadoLineaFinder.Find(s))
          {
            lstLineaSubteModels.Add(i);
          }
          return lstLineaSubteModels.ToList();
          //return db.LineaSubteModels;
        }

        // GET: api/LineaSubteModels/5
        [ResponseType(typeof(LineaSubteModel))]
        public async Task<IHttpActionResult> GetLineaSubteModel(string id)
        {
            LineaSubteModel lineaSubteModel = await db.LineaSubteModels.FindAsync(id);
            if (lineaSubteModel == null)
            {
                return NotFound();
            }

            return Ok(lineaSubteModel);
        }

        // PUT: api/LineaSubteModels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLineaSubteModel(string id, LineaSubteModel lineaSubteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lineaSubteModel.Id)
            {
                return BadRequest();
            }

            db.Entry(lineaSubteModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineaSubteModelExists(id))
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

        // POST: api/LineaSubteModels
        [ResponseType(typeof(LineaSubteModel))]
        public async Task<IHttpActionResult> PostLineaSubteModel(LineaSubteModel lineaSubteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LineaSubteModels.Add(lineaSubteModel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LineaSubteModelExists(lineaSubteModel.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lineaSubteModel.Id }, lineaSubteModel);
        }

        // DELETE: api/LineaSubteModels/5
        [ResponseType(typeof(LineaSubteModel))]
        public async Task<IHttpActionResult> DeleteLineaSubteModel(string id)
        {
            LineaSubteModel lineaSubteModel = await db.LineaSubteModels.FindAsync(id);
            if (lineaSubteModel == null)
            {
                return NotFound();
            }

            db.LineaSubteModels.Remove(lineaSubteModel);
            await db.SaveChangesAsync();

            return Ok(lineaSubteModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LineaSubteModelExists(string id)
        {
            return db.LineaSubteModels.Count(e => e.Id == id) > 0;
        }
    }
}