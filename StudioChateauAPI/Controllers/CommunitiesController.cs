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
    [Authorize(Roles="Builder")]
    public class CommunitiesController : StudioChateauAPIControllerBase
    {
        private ProjectConfigDbContext db = new ProjectConfigDbContext();

        // GET: odata/Communities
        [EnableQuery]
        public IQueryable<Communities> GetCommunities()
        {
            return db.Communities;
        }

        // GET: odata/Communities(5)
        [EnableQuery]
        public SingleResult<Communities> GetCommunities([FromODataUri] int key)
        {
            return SingleResult.Create(db.Communities.Where(communities => communities.Community_id == key));
        }

        // PUT: odata/Communities(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Communities> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Communities communities = db.Communities.Find(key);
            if (communities == null)
            {
                return NotFound();
            }

            BuilderAccessList builderAccess;
            IEnumerable<AuthenticationHeaderValue> challenges;
            GetBuilderAccess(communities, out builderAccess, out challenges);

            if (builderAccess == null)
                return Unauthorized(challenges);


            patch.Put(communities);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunitiesExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(communities);
        }

        private IHttpActionResult HandleMultipleCommunities(Communities[] communties)
        {
            foreach (var community in communties)
            {
                Communities communities = db.Communities.Where(c =>
                     c.Community_External_GUID == community.Community_External_GUID ||
                     c.Community_Name == community.Community_Name).FirstOrDefault<Communities>();

                if (communities == null)
                {
                    BuilderAccessList builderAccess;
                    IEnumerable<AuthenticationHeaderValue> challenges;
                    GetBuilderAccess(community, out builderAccess, out challenges);

                    if (builderAccess == null)
                        return Unauthorized(challenges);

                    communities = PopulateDefaults(communities);

                    db.Communities.Add(community);
                    db.SaveChanges();
                }
            }

            return Ok();
        }
        // POST: odata/Communities
        public IHttpActionResult Post(Communities communities)
        {
            if (communities.communities != null)
            {
                return HandleMultipleCommunities(communities.communities);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Communities community = db.Communities.Where(c => 
                 c.Community_External_GUID==communities.Community_External_GUID ||
                 c.Community_Name == communities.Community_Name).FirstOrDefault<Communities>();

            if (community == null)
            {
                BuilderAccessList builderAccess;
                IEnumerable<AuthenticationHeaderValue> challenges;
                GetBuilderAccess(communities, out builderAccess, out challenges);

                if (builderAccess == null)
                    return Unauthorized(challenges);

                communities = PopulateDefaults(communities);

                db.Communities.Add(communities);

                db.SaveChanges();

                return Created(communities);
            }

            return Created(community);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommunitiesExists(int key)
        {
            return db.Communities.Count(e => e.Community_id == key) > 0;
        }
    }
}
