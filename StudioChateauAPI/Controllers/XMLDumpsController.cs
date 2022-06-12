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
    public class XMLDumpsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/XMLDumps
        [EnableQuery]
        public IQueryable<XMLDump> GetXMLDumps()
        {
            return db.XMLDumps;
        }

        // GET: odata/XMLDumps(5)
        [EnableQuery]
        public SingleResult<XMLDump> GetXMLDump([FromODataUri] int key)
        {
            return SingleResult.Create(db.XMLDumps.Where(xMLDump => xMLDump.ID == key));
        }


        // POST: odata/XMLDumps
        public IHttpActionResult Post(XMLDump xMLDump)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.XMLDumps.Add(xMLDump);
            db.SaveChanges();

            return Created(xMLDump);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool XMLDumpExists(int key)
        {
            return db.XMLDumps.Count(e => e.ID == key) > 0;
        }
    }
}
