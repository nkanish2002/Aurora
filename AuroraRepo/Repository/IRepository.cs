using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Models.Derived;
using Aurora.Models.Primitives;
using System.Linq.Expressions;

namespace Aurora.Repository
{
    /// <summary>
    /// A Generic Interface that defines operations that can be performed on various resources available in the
    /// Aurora EcoSystem.
    /// </summary>
    /// <typeparam name="TEntity">The Class corresponding to the resource</typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Return all the records.
        /// </summary>
        IQueryable<TEntity> All();
        
        /// <summary>
        /// Performs a filter on all records in the databases
        /// </summary>
        /// <param name="filter">LINQ Expression to filter data</param>
        /// <param name="includeProperties">The navigation properties to be included</param>
        /// <returns></returns>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter, string includeProperties = "");

        /// <summary>
        /// Returns a paged list of objects based on a specific sorting condition
        /// </summary>
        /// <param name="orderBy">The sorting condition</param>
        /// <param name="page">The starting index which defaults to 0</param>
        /// <param name="size">The number of items to be retrieved which defaults to 25</param>
        /// <param name="includeProperties">The navigation properties to be included</param>
        /// <returns>Sorted and Paged list of objects</returns>
        IQueryable<TEntity> Filter(
            Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy, 
            int page = 0, 
            int size = 25, 
            string includeProperties = ""
        );

        /// <summary>
        /// Returns a filtered and sorted list of objects
        /// </summary>
        /// <param name="filter">Filtering Criteria</param>
        /// <param name="orderBy">Sorting Criteria</param>
        /// <param name="page">The starting index which defaults to 0</param>
        /// <param name="size">The number of items to be retrieved which defaults to 25</param>
        /// <param name="includeProperties">The navigation properties to be included</param>
        /// <returns>Filtered, Sorted and Paged list of objects</returns>
        IQueryable<TEntity> Filter(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            int page = 0,
            int size = 25,
            string includeProperties = ""
        );

        /// <summary>
        /// Checks the Object(s) exist in the database
        /// </summary>
        /// <param name="predicate">LINQ Expression to filter data</param>
        /// <returns>True or False based on the filter</returns>
        bool Contains(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find the values with in the context if they are already fetched.
        /// </summary>
        /// <param name="keys">Primary Keys of the Resource</param>
        TEntity Find(object[] keys);

        /// <summary>
        /// Returns an object based on the filtering criteria
        /// </summary>
        /// <param name="predicate">LINQ Expression to filter data</param>
        /// <param name="includeProperties">The navigation properties to be included</param>
        /// <returns>A single instance of an object. If multiple such values exist then it will throw an error</returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate, string includeProperties = "");

        /// <summary>
        /// Creates a new object in the database
        /// </summary>
        /// <param name="TEntity">Object to be added</param>
        /// <returns></returns>
        int Create(TEntity TEntity);

        /// <summary>
        /// Deletes an object from the database
        /// </summary>
        /// <param name="TEntity">Object to be deleted</param>
        /// <returns></returns>
        int Delete(TEntity TEntity);

        /// <summary>
        /// Deletes multiple objects based on criteria
        /// </summary>
        /// <param name="predicate">LINQ Expression to filter data</param>
        /// <returns></returns>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Updates an object in the database
        /// </summary>
        /// <param name="TEntity">Object to be updated</param>
        /// <returns></returns>
        int Update(TEntity TEntity);

        /// <summary>
        /// Number of Elements of the Object Type
        /// </summary>
        /// <param name="predicate">LINQ Expression to filter data</param>
        int Count(Expression<Func<TEntity, bool>> predicate);
    }
}
