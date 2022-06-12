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
using System.Net.Http.Headers;

namespace StudioChateauAPI.Controllers
{
    [Authorize(Roles = "Builder")]
    public class PlansController : StudioChateauAPIControllerBase
    {
        private ProjectConfigDbContext db = new ProjectConfigDbContext();

        // GET: odata/Plans
        [EnableQuery]
        public IQueryable<Plan> GetPlans()
        {
            return db.plans;
        }

        // GET: odata/Plans(5)
        [EnableQuery]
        public SingleResult<Plan> GetPlan([FromODataUri] int key)
        {
            return SingleResult.Create(db.plans.Where(plan => plan.plan_id == key));
        }

        // PUT: odata/Plans(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Plan> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Plan plan = db.plans.Find(key);
            if (plan == null)
            {
                return NotFound();
            }

            Communities communities = db.Communities.Find(plan.community_id);
            if (communities == null)
            {
                return NotFound();
            }

            BuilderAccessList builderAccess;
            IEnumerable<AuthenticationHeaderValue> challenges;
            GetBuilderAccess(communities, out builderAccess, out challenges);

            if (builderAccess == null)
                return Unauthorized(challenges);

            patch.Put(plan);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(plan);
        }

        private Plan PopulateSequence(Plan plan)
        {
            if (plan.plan_seq == null || plan.plan_seq == 0)
            {
                int pln = db.plans.Select(p => p.plan_seq).ToList().Max<int>();
                plan.plan_seq = pln + 10;
            }

            return plan;
        }

        private IHttpActionResult HandleMultiplePlans(Plan[] plans)
        {
            foreach(var Plan in plans)
            {
                Communities communities = db.Communities.Find(Plan.community_id);
                if (communities == null)
                {
                    return NotFound();
                }

               
                BuilderAccessList builderAccess;
                IEnumerable<AuthenticationHeaderValue> challenges;
                GetBuilderAccess(communities, out builderAccess, out challenges);

                if (builderAccess == null)
                    return Unauthorized(challenges);

                Plan planModified = PopulateSequence(Plan);

                db.plans.Add(planModified);
                db.SaveChanges();
            }

            return Ok();
        }
        // POST: odata/Plans
        public IHttpActionResult Post(Plan plan)
        {
            if (plan.plans != null)
            {
                return HandleMultiplePlans(plan.plans);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Communities communities = db.Communities.Find(plan.community_id);
            if (communities == null)
            {
                return NotFound();
            }

            BuilderAccessList builderAccess;
            IEnumerable<AuthenticationHeaderValue> challenges;
            GetBuilderAccess(communities, out builderAccess, out challenges);

            if (builderAccess == null)
                return Unauthorized(challenges);

            plan = PopulateSequence(plan);

            db.plans.Add(plan);
            db.SaveChanges();

            return Created(plan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlanExists(int key)
        {
            return db.plans.Count(e => e.plan_id == key) > 0;
        }
    }
}
