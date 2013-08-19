using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Aurora.Models.Primitives;
using Aurora.Entity;

namespace Aurora.API
{
    public class SampleController : ApiController
    {
        private AuroraContext db = new AuroraContext();

        // GET api/Sample
        public IEnumerable<Company> GetCompanies()
        {
            return db.Companies.AsEnumerable();
        }

        // GET api/Sample/5
        public Company GetCompany(int id)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return company;
        }

        // PUT api/Sample/5
        public HttpResponseMessage PutCompany(int id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != company.CompanyId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(company).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Sample
        public HttpResponseMessage PostCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, company);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = company.CompanyId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Sample/5
        public HttpResponseMessage DeleteCompany(int id)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Companies.Remove(company);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, company);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}