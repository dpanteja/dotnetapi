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

namespace StudioChateauAPI.Controllers
{

    public class ExternalServicesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/ExternalServices
        [EnableQuery]
        public IQueryable<ExternalServices> GetExternalServices()
        {
            return db.ExternalServices;
        }

        // GET: odata/ExternalServices(5)
        [EnableQuery]
        public SingleResult<ExternalServices> GetExternalServices([FromODataUri] int key)
        {
            return SingleResult.Create(db.ExternalServices.Where(externalServices => externalServices.ExternalServiceId == key));
        }

        // PUT: odata/ExternalServices(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<ExternalServices> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExternalServices externalServices = db.ExternalServices.Find(key);
            if (externalServices == null)
            {
                return NotFound();
            }

            patch.Put(externalServices);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExternalServicesExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(externalServices);
        }

        // POST: odata/ExternalServices
        public IHttpActionResult Post(ExternalServices externalServices)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExternalServices.Add(externalServices);
            db.SaveChanges();

            return Created(externalServices);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExternalServicesExists(int key)
        {
            return db.ExternalServices.Count(e => e.ExternalServiceId == key) > 0;
        }
    }
}
