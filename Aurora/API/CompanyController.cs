using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aurora.Models.Primitives;
using Aurora.Models.Derived;
using Aurora.Entity;
using Aurora.Repository;

namespace Aurora.API
{
    /// <summary>
    /// Controller for Companies
    /// </summary>

    [RoutePrefix("api/Company")]
    public class CompanyController : BaseController<Company>
    {
        protected override System.Linq.Expressions.Expression<Func<Company, bool>> IdFilter(int id)
        {
            return t => t.CompanyId == id;
        }

        protected override Func<IQueryable<Company>, IOrderedQueryable<Company>> OrderFilter()
        {
            return t => t.OrderBy(o => o.CompanyName);
        }

        protected override System.Linq.Expressions.Expression<Func<Company, bool>> QueryFilter(string query)
        {
            return t => t.CompanyName.Contains(query);
        }

        [HttpGet("{id}/hiringprofiles")]
        public IEnumerable<HiringProfile> HiringProfiles(int id) {
            try
            {
                return Repository
                    .Find(IdFilter(id), "HiringProfiles")
                    .HiringProfiles;
            }
            catch (Exception e)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected Error: " + e.Message);
                return null;
            }
        }
    }
}
