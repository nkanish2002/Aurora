using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Repository
{
    /// <summary>
    /// Implementation of the IRepository Class
    /// </summary>
    /// <typeparam name="TEntity">An Object corresponding to the resources in the Aurora EcoSystem</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        internal DbContext Context;
        internal DbSet<TEntity> Collection;

        /// <summary>
        /// Accept a context and use it internally.
        /// Helpful for unit testing when collections have sample values in them
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            this.Context = context;
            this.Collection = context.Set<TEntity>();

            this.Context.Configuration.ProxyCreationEnabled = false;
            this.Context.Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Close all resources and excecute garbage collection
        /// </summary>
        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }

        /// <summary>
        /// Take a list of properties and construct the query accordingly
        /// </summary>
        /// <param name="query">The collection on which the properties must be included</param>
        /// <param name="includeProperties">The comma separated string of navigation properties</param>
        /// <returns>A constructed query that is set to perform necessary JOIN operations</returns>
        private IQueryable<TEntity> BuildProperties(IQueryable<TEntity> query, string includeProperties)
        {
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        /// <summary>
        /// Return All Records. Designed to be exposed via OData Services
        /// </summary>
        public virtual IQueryable<TEntity> All() {
            return Collection.AsQueryable();
        }

        /// <summary>
        /// Query and Filter the Collection
        /// </summary>
        /// <param name="filter">Filtering Criteria</param>
        /// <param name="includeProperties">Include Dependent Entities</param>
        /// <returns>Returns a IQueryable List of Resources</returns>
        public virtual IQueryable<TEntity> Filter(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = ""
        )
        {
            IQueryable<TEntity> query = Collection;
            query = BuildProperties(query, includeProperties);
            return query.Where(filter).AsQueryable();
        }

        /// <summary>
        /// Returns a paged list of objects based on a specific sorting condition
        /// </summary>
        /// <param name="orderBy">The sorting condition</param>
        /// <param name="page">The starting index which defaults to 0</param>
        /// <param name="size">The number of items to be retrieved which defaults to 25</param>
        /// <param name="includeProperties">The navigation properties to be included</param>
        /// <returns>Sorted and Paged list of objects</returns>
        public virtual IQueryable<TEntity> Filter(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int page = 0, 
            int size = 25, 
            string includeProperties = ""
        )
        {
            IQueryable<TEntity> query = Collection;
            query = BuildProperties(query, includeProperties);
            query = orderBy(query);
            query = query.Skip(page * size).Take(size);
            return query.AsQueryable();
        }

        /// <summary>
        /// Returns a filtered and sorted list of objects
        /// </summary>
        /// <param name="filter">Filtering Criteria</param>
        /// <param name="orderBy">Sorting Criteria</param>
        /// <param name="page">The starting index which defaults to 0</param>
        /// <param name="size">The number of items to be retrieved which defaults to 25</param>
        /// <param name="includeProperties">The navigation properties to be included</param>
        /// <returns>Filtered, Sorted and Paged list of objects</returns>
        public virtual IQueryable<TEntity> Filter(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int page = 0,
            int size = 25,
            string includeProperties = ""
        )
        {
            IQueryable<TEntity> query = Filter(filter, includeProperties);
            query = orderBy(query);
            query = query.Skip(page * size).Take(size);
            return query.AsQueryable();
        }

        /// <summary>
        /// Checks if any match exists in the collection
        /// </summary>
        /// <param name="predicate">Filtering Criteria</param>
        /// <returns>Returns a boolean value denoting the existence of the Resource</returns>
        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Count(predicate) > 0;
        }

        /// <summary>
        /// Find the values with in the context if they are already fetched.
        /// </summary>
        /// <param name="keys">Primary Keys of the Resource</param>
        public virtual TEntity Find(object[] keys)
        {
            return Collection.Find(keys);
        }

        /// <summary>
        /// Gets an Object from the collection based on a filtering criteria
        /// </summary>
        /// <param name="predicate">Filtering Criteria</param>
        /// <param name="includeProperties">The navigation properties to be included</param>
        /// <returns>Returns a Resource Object if the match is valid or null instead</returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, string includeProperties= "")
        {
            IQueryable<TEntity> query = Collection;
            query = BuildProperties(query, includeProperties);
            return query.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Creates a new Object
        /// </summary>
        /// <param name="TEntity"></param>
        /// <returns>Creates an instance of the Resource in the database</returns>
        public virtual int Create(TEntity TEntity)
        {
            Collection.Add(TEntity);
            return Context.SaveChanges();
        }

        /// <summary>
        /// Deletes a given Object
        /// </summary>
        /// <param name="TEntity"></param>
        /// <returns>Deletes the instance if it exists in the database and returns the status code</returns>
        public virtual int Delete(TEntity TEntity)
        {
            Collection.Remove(TEntity);
            return Context.SaveChanges();
        }

        /// <summary>
        /// Deletes Objects based on a filtering criteria
        /// </summary>
        /// <param name="predicate">Filtering Criteria</param>
        /// <returns>Deletes multiple matches of the Resource if they exist in the database and returns the status code</returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var item in objects)
            {
                Collection.Remove(item);
            }
            return Context.SaveChanges();
        }

        /// <summary>
        /// Updates a given Object
        /// </summary>
        /// <param name="TEntity">Object to Updated</param>
        /// <returns>Updates the entity and returns the status code</returns>
        public virtual int Update(TEntity TEntity)
        {
            var value = Context.Entry(TEntity);
            Collection.Attach(TEntity);
            value.State = System.Data.EntityState.Modified;
            return Context.SaveChanges();
        }

        /// <summary>
        /// Returns the total number of elements present in the Collection
        /// </summary>
        /// <param name="predicate">Filtering Criteria</param>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Where(predicate).Count();
        }
    }
}
