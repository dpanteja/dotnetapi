using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using StudioChateauAPI.Models;
using StudioChateauAPI.Exceptions;
using Microsoft.Data.OData;
using System.Net.Http.Headers;

namespace StudioChateauAPI.Controllers
{
    [Authorize(Roles = "Builder")]
    public class LotsController : StudioChateauAPIControllerBase
    {
        private ProjectConfigDbContext db = new ProjectConfigDbContext();

        // GET: odata/Lots
        [EnableQuery]
        public IQueryable<Lot> GetLots()
        {
            return db.lots;
        }

        // GET: odata/Lots(5)
        [EnableQuery]
        public SingleResult<Lot> GetLot([FromODataUri] int key)
        {
            return SingleResult.Create(db.lots.Where(lot => lot.lot_id == key));
        }

        // PUT: odata/Lots(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Lot> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lot lot = db.lots.Find(key);
            if (lot == null)
            {
                return NotFound();
            }

            try
            {
                bool result = IsLotPayloadValid(null, lot);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            patch.Put(lot);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LotExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lot);
        }

        private Lot PopulateSequence(Lot lot)
        {
            if (lot.lot_sequence == null || lot.lot_sequence == 0)
            {
                int? lotSeq = db.lots.Select(l => l.lot_sequence).ToList().Max<int?>();

                lot.lot_sequence = lotSeq + 1;
            }

            return lot;
        }

        private IHttpActionResult HandleMultipleCommunities(Lot[] lots)
        {
            foreach (var lot in lots)
            {

                try
                {
                    bool result = IsLotPayloadValid(lot, null);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                Lot lotModified = PopulateSequence(lot);

                db.lots.Add(lotModified);
                db.SaveChanges();
            }

            return Ok();
        }

        // POST: odata/Lots
        public IHttpActionResult Post(Lot lot)
        {
            if (lot.lots != null)
            {
                return HandleMultipleCommunities(lot.lots);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool result = IsLotPayloadValid(lot, null);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }

            lot = PopulateSequence(lot);

            db.lots.Add(lot);

            db.SaveChanges();
            
            return Created(lot);
        }

        // PATCH: odata/Lots(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Lot> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lot lot = db.lots.Find(key);
            if (lot == null)
            {
                return NotFound();
            }

            patch.Patch(lot);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LotExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lot);
        }

        // DELETE: odata/Lots(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Lot lot = db.lots.Find(key);
            if (lot == null)
            {
                return NotFound();
            }

            db.lots.Remove(lot);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LotExists(int key)
        {
            return db.lots.Count(e => e.lot_id == key) > 0;
        }
    }
}
