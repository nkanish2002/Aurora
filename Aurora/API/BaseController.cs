using Aurora.Entity;
using Aurora.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Linq.Expressions;
using System.Web.Http.OData.Query;
using System.Web.Http.Cors;

namespace Aurora.API
{
    /// <summary>
    /// Template Class to Create CRUD Operations
    /// </summary>
    /// <typeparam name="T">A Resource Object</typeparam>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BaseController<T> : ApiController where T : class
    {
        protected Repository<T> Repository;

        /// <summary>
        /// Initializes the Entity Framework and Repository
        /// </summary>
        public BaseController()
        {
            var Context = new AuroraContext();
            Repository = new Repository<T>(Context);
        }

        /// <summary>
        /// Override this so as to include various sub entities if not using OData to do the same
        /// </summary>
        /// <returns></returns>
        protected virtual string IncludeFilter()
        {
            return "";
        }

        /// <summary>
        /// Override this so as to perform queries that depend on the Primary Key
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<T, bool>> IdFilter(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Override this so as to perform queries that depend on a search string
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<T, bool>> QueryFilter(string query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Override this so as to perform queries that depend on an ordering scheme
        /// </summary>
        /// <returns></returns>
        protected virtual Func<IQueryable<T>, IOrderedQueryable<T>> OrderFilter()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Paged Multiple Read Request Implementation
        /// </summary>
        [AllowAnonymous, HttpGet("{page:int:min(0)}/{size:int:range(1,50)}")]
        public IEnumerable<T> ReadAll(int page, int size)
        {
            try
            {
                return Repository.Filter(
                    orderBy: OrderFilter(),
                    page: page,
                    size: size,
                    includeProperties: IncludeFilter()
                ).ToList();
            }
            catch (Exception e)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected Error: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Read Request Implementation
        /// </summary>
        [AllowAnonymous, HttpGet("{id:int:min(1)}")]
        public T Read(int id)
        {
            try
            {
                if (IncludeFilter() == "")
                {
                    return Repository.Find(new object[] { id });
                }
                else
                {
                    return Repository.Find(IdFilter(id), IncludeFilter());
                }
            }
            catch (Exception e)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected Error: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Search Request Implementation
        /// </summary>
        [AllowAnonymous, HttpGet("search/{query:alpha}/{page:int:min(0)}/{size:int:range(1,50)}")]
        public IEnumerable<T> Search(string query, int page, int size)
        {
            try
            {
                return Repository.Filter(
                    filter: QueryFilter(query),
                    orderBy: OrderFilter(),
                    page: page,
                    size: size,
                    includeProperties: IncludeFilter()
                ).ToList();
            }
            catch (Exception e)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected Error: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Create Request Implementation
        /// </summary>
        [HttpPost("")]
        public virtual HttpResponseMessage Create([FromBody]T value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if (Repository.Create(value) == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Create Failed");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "Created");
                }
            }
            catch (OptimisticConcurrencyException oce)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Conflict: " + oce.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected Error: " + e.Message + " Inner Exception: " + e.InnerException.ToString());
            }
        }

        /// <summary>
        /// Update Request Implementation
        /// </summary>
        [HttpPut("")]
        public virtual HttpResponseMessage Update([FromBody]T value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if (Repository.Update(value) == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Update Failed");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Updated");
                }
            }
            catch (OptimisticConcurrencyException oce)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Conflict: " + oce.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected Error: " + e.Message);
            }
        }

        /// <summary>
        /// Delete Request Implementation
        /// </summary>
        [HttpDelete("{id:int:min(1)}")]
        public virtual HttpResponseMessage Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var value = Repository.Find(IdFilter(id));
                if (Repository.Delete(value) == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Delete Failed");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
                }
            }
            catch (OptimisticConcurrencyException oce)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Conflict: " + oce.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected Error: " + e.Message);
            }
        }

        /// <summary>
        /// OData Endpoint
        /// </summary>
        /// <returns></returns>
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All, MaxExpansionDepth = 5), AcceptVerbs("GET", "HEAD")]
        public IQueryable<T> OData()
        {
            try
            {
                return Repository.All();
            }
            catch (Exception e)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unexpected Error: " + e.Message);
                return null;
            }
        }
    }
}