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
    public class StudioChateauAPIErrorLogsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/StudioChateauAPIErrorLogs
        [EnableQuery]
        public IQueryable<StudioChateauAPIErrorLog> GetStudioChateauAPIErrorLogs()
        {
            return db.StudioChateauAPIErrorLogs;
        }

        // GET: odata/StudioChateauAPIErrorLogs(5)
        [EnableQuery]
        public SingleResult<StudioChateauAPIErrorLog> GetStudioChateauAPIErrorLog([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudioChateauAPIErrorLogs.Where(studioChateauAPIErrorLog => studioChateauAPIErrorLog.ID == key));
        }


        // POST: odata/StudioChateauAPIErrorLogs
        public IHttpActionResult Post(StudioChateauAPIErrorLog studioChateauAPIErrorLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudioChateauAPIErrorLogs.Add(studioChateauAPIErrorLog);
            db.SaveChanges();

            return Created(studioChateauAPIErrorLog);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudioChateauAPIErrorLogExists(int key)
        {
            return db.StudioChateauAPIErrorLogs.Count(e => e.ID == key) > 0;
        }
    }
}
