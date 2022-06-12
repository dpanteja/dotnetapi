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
using System.Web.Http.OData.Query;
using StudioChateauAPI.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http.Results;
using System.Xml.Serialization;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace StudioChateauAPI.Controllers
{
    [Authorize(Roles="Builder")]
    public class PhasesController : StudioChateauAPIControllerBase
    {
        private ProjectConfigDbContext db = new ProjectConfigDbContext();

        // GET: odata/Phases
        [EnableQuery]
        public IHttpActionResult GetPhases()
        {
            Phase p = new Phase();
            p.phase_id = 1000;
            p.phase_no = "1001";
            p.phase_desc = "this is desc";
            p.community_id = 2;
            p.phase_hold_orders = "y";
            p.phase_admin_email = "test@test.com";
            p.phase_retail_price_override = "y";
            p.phase_external_GUID = "Some GuID";
            p.msrepl_tran_version = new Guid();
            p.external_exports = true;

            Phase p1 = new Phase();
            p.phase_id = 1000;
            p.phase_no = "1001";
            p.phase_desc = "this is desc";
            p.community_id = 2;
            p.phase_hold_orders = "y";
            p.phase_admin_email = "test@test.com";
            p.phase_retail_price_override = "y";
            p.phase_external_GUID = "Some GuID";
            p.msrepl_tran_version = new Guid();
            p.external_exports = true;

            IEnumerable<Phase> phases = new Phase[]{
               p, p1
            };

            return ResponseMessage(buildResponse(phases, this));
            //return ResponseMessage(buildResponse(db.phases.ToList<Phase>(),this));
        }

        // GET: odata/Phases(5)
        [EnableQuery]
       // public SingleResult<Phase> GetPhase([FromODataUri] int key)
        public IHttpActionResult GetPhase([FromODataUri] int key)
        {
            Phase p = new Phase();
            p.phase_id = 1000;
            p.phase_no = "1001";
            p.phase_desc = "this is desc";
            p.community_id = 2;
            p.phase_hold_orders = "y";
            p.phase_admin_email = "test@test.com";
            p.phase_retail_price_override = "y";
            p.phase_external_GUID = "Some GuID";
            p.msrepl_tran_version = new Guid();
            p.external_exports = true;   

            //Phase p = (Phase) db.phases.Where(phase => phase.phase_id == key).ToList<Phase>()[0];
          
            return ResponseMessage(buildResponse(p,this));
         }

        // PUT: odata/Phases(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Phase> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Phase phase = db.phases.Find(key);
            if (phase == null)
            {
                return NotFound();
            }

            Communities communities = db.Communities.Find(phase.community_id);
            if (communities == null)
            {
                return NotFound();
            }

            BuilderAccessList builderAccess;
            IEnumerable<AuthenticationHeaderValue> challenges;
            GetBuilderAccess(communities, out builderAccess, out challenges);

            if (builderAccess == null)
                return Unauthorized(challenges);


            patch.Put(phase);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhaseExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(phase);
        }

        private Phase PopulatePhaseDefaults(Phase phase)
        {
            Communities community = db.Communities.Where(c => c.Community_id == phase.community_id).FirstOrDefault<Communities>();

            phase.phase_hold_orders = community.Phase_Hold_Orders;
        
            return phase;
        }
        private IHttpActionResult HandleMultiplePhases(Phase[] phases)
        {
            foreach (var phase in phases)
            {
                Communities communities = db.Communities.Find(phase.community_id);
                if (communities == null)
                {
                    return NotFound();
                }

                Phase phaseModified = PopulatePhaseDefaults(phase);

                BuilderAccessList builderAccess;
                IEnumerable<AuthenticationHeaderValue> challenges;
                GetBuilderAccess(communities, out builderAccess, out challenges);

                if (builderAccess == null)
                    return Unauthorized(challenges);

                db.phases.Add(phaseModified);
                db.SaveChanges();
            }

            return Ok();
        }
        // POST: odata/Phases
        public IHttpActionResult Post(Phase phase)
        {
            if (phase.phases != null)
            {
                return HandleMultiplePhases(phase.phases);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Communities communities = db.Communities.Find(phase.community_id);
            if (communities == null)
            {
                return NotFound();
            }

            Phase existingPhase = db.phases.Where(p => p.phase_id == phase.phase_id &&
                                            p.community_id == phase.community_id).FirstOrDefault<Phase>();

            if (existingPhase == null)
            {
                phase = PopulatePhaseDefaults(phase);

                BuilderAccessList builderAccess;
                IEnumerable<AuthenticationHeaderValue> challenges;
                GetBuilderAccess(communities, out builderAccess, out challenges);

                if (builderAccess == null)
                    return Unauthorized(challenges);

                db.phases.Add(phase);
                db.SaveChanges();

                return Created(phase);
            }

            return Created(existingPhase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhaseExists(int key)
        {
            return db.phases.Count(e => e.phase_id == key) > 0;
        }

        private HttpResponseMessage buildResponse(Phase p, ODataController controller)
        {

            MediaTypeFormatter bestMatchFormatter = null;

            bestMatchFormatter = GetMediaType(bestMatchFormatter);

            IEnumerable<Phase> phaseEnum = new Phase[]{
                p
            };


            return new HttpResponseMessage()
            {
                Content = new ObjectContent(typeof(Phase), p, bestMatchFormatter),

            };
        }
        
        private MediaTypeFormatter GetMediaType(MediaTypeFormatter bestMatchFormatter)
        {
            var mediaType = Request.Headers.Accept.ToString();

            if (mediaType == "application/xml")
            {
                bestMatchFormatter = new XmlMediaTypeFormatter();
            }
            else
            {
                //Return JSON formatter - we default to JSON Formatting
                bestMatchFormatter = new JsonMediaTypeFormatter();
            }
            return bestMatchFormatter;
        }


        private HttpResponseMessage buildResponse(IEnumerable<Phase> results, ODataController controller)
        {
            MediaTypeFormatter bestMatchFormatter = null;
            
            bestMatchFormatter = GetMediaType(bestMatchFormatter);
            String mediaType = Request.Headers.Accept.ToString();

            //build up a response with  them
            return new HttpResponseMessage()
            {
                Content = new ObjectContent<IList<Phase>>(
                    new List<Phase>(results), bestMatchFormatter, mediaType
                )
            };
        }
    
    }
}
