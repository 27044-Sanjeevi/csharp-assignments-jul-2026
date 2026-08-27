using System.Linq.Expressions;

namespace Assignment9LINQAdvanced.Tasks
{
    /// <summary>
    /// Represents the query builder.
    /// </summary>
    /// <typeparam name="T">The reference type of the domain model entity.</typeparam>
    internal class QueryBuilder<T>
        where T : class
    {
        private IQueryable<T> _resultantQuery;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder{T}"/> class.
        /// </summary>
        /// <param name="enumerable">An instance of the IEnumerable object.</param>
        public QueryBuilder(IEnumerable<T> enumerable)
        {
            this._resultantQuery = enumerable.AsQueryable();
        }

        /// <summary>
        /// Applies the filter query.
        /// </summary>
        /// <param name="predicate">An expression tree representing the evalucation condition.</param>
        /// <returns>The current instance of the <see cref="QueryBuilder{T}"/> to enable fluent method chaining.</returns>
        public QueryBuilder<T> Filter(Expression<Func<T, bool>> predicate)
        {
            this._resultantQuery = this._resultantQuery.Where(predicate);
            return this;
        }

        /// <summary>
        /// Applies the sort query.
        /// </summary>
        /// <typeparam name="TKey">The data type of the property being used as sorting criteria.</typeparam>
        /// <param name="keySelector">An lambda expression to specifying the sorting criteria.</param>
        /// <returns>The current instance of the <see cref="QueryBuilder{T}"/> to enable fluent method chaining.</returns>
        public QueryBuilder<T> SortBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            this._resultantQuery = this._resultantQuery.OrderBy(keySelector);
            return this;
        }

        /// <summary>
        /// Executes the built query and returns the concrete results as a list.
        /// </summary>
        /// <returns>A list containing the processed query elements.</returns>
        public List<T> Execute()
        {
            return this._resultantQuery.ToList();
        }
    }
}
