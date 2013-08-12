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
using System.Web.Http.OData.Query;

namespace Aurora.API
{
    /// <summary>
    /// Controller for Companies
    /// </summary>

    [RoutePrefix("api/HiringProfile")]
    public class HiringProfileController : BaseController<HiringProfile>
    {
        protected override string IncludeFilter()
        {
            return "Company, VisitingCampus, Departments";
        }

        protected override System.Linq.Expressions.Expression<Func<HiringProfile, bool>> IdFilter(int id)
        {
            return t => t.HiringProfileId == id;
        }

        protected override Func<IQueryable<HiringProfile>, IOrderedQueryable<HiringProfile>> OrderFilter()
        {
            return t => t.OrderBy(o => o.HiringProfileName);
        }

        protected override System.Linq.Expressions.Expression<Func<HiringProfile, bool>> QueryFilter(string query)
        {
            return t => t.HiringProfileName.Contains(query);
        }

        [HttpGet("{id}/registered")]
        public IEnumerable<Student> HiringProfiles(int id) {
            try
            {
                var progress = Repository
                    .Find(IdFilter(id), includeProperties: "RegisteredStudents")
                    .RegisteredStudents;

                var output = (from item in progress select item.Student).ToList();
                return output;
            }
            catch (Exception e)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected Error: " + e.Message);
                return null;
            }
        }
    }
}
