using System.Linq.Expressions;

namespace Assignment9LINQAdvanced.Tasks
{
    /// <summary>
    /// Represents the query builder.
    /// </summary>
    /// <typeparam name="T">The reference type of the domain model entity.</typeparam>
    internal class QueryBuilder<T>
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
        /// Initializes a new instance of the <see cref="QueryBuilder{T}"/> class.
        /// </summary>
        /// <param name="query">The query to be assigned to the resultant query.</param>
        public QueryBuilder(IQueryable<T> query)
        {
            this._resultantQuery = query;
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
        /// Filters string properties that contain the target text value.
        /// </summary>
        /// <param name="propertyName">The property name used for filtering.</param>
        /// <param name="value">Value to be filtered.</param>
        /// <returns>The current instance of the <see cref="QueryBuilder{T}"/> to enable fluent method chaining.</returns>
        public QueryBuilder<T> Contains(string propertyName, string value)
        {
            Expression<Func<string, string, bool>> expr = (s, v) => s.Contains(v);
            return this.ApplyStringFilter(propertyName, expr, value);
        }

        /// <summary>
        /// Filters string properties that start with the target text value.
        /// </summary>
        /// <param name="propertyName">The property name used for filtering.</param>
        /// <param name="value">Value to filter.</param>
        /// <returns>The current instance of the <see cref="QueryBuilder{T}"/> to enable fluent method chaining</returns>
        public QueryBuilder<T> StartsWith(string propertyName, string value)
        {
            Expression<Func<string, string, bool>> expr = (s, v) => s.StartsWith(v);
            return this.ApplyStringFilter(propertyName, expr, value);
        }

        /// <summary>
        /// Filters string properties that end with the target text value.
        /// </summary>
        /// <param name="propertyName">The property name used for filtering.</param>
        /// <param name="value">Value to filter.</param>
        /// <returns>The current instance of the <see cref="QueryBuilder{T}"/> to enable fluent method chaining</returns>
        public QueryBuilder<T> EndsWith(string propertyName, string value)
        {
            Expression<Func<string, string, bool>> expr = (s, v) => s.EndsWith(v);
            return this.ApplyStringFilter(propertyName, expr, value);
        }

        /// <summary>
        /// Filters numeric or date properties that are greater than or equal to the target value.
        /// </summary>
        /// <param name="propertyName">The property name used for filtering.</param>
        /// <param name="value">Value to filter.</param>
        /// <returns>The current instance of the <see cref="QueryBuilder{T}"/> to enable fluent method chaining</returns>
        public QueryBuilder<T> GreaterThanOrEqualTo(string propertyName, object value)
        {
            return this.ApplyComparisonFilter(propertyName, value, Expression.GreaterThanOrEqual);
        }

        /// <summary>
        /// Filters numeric or date properties that are less than or equal to the target value.
        /// </summary>
        /// <param name="propertyName">The property name used for filtering.</param>
        /// <param name="value">Value to filter.</param>
        /// <returns>The current instance of the <see cref="QueryBuilder{T}"/> to enable fluent method chaining</returns>
        public QueryBuilder<T> LessThanOrEqualTo(string propertyName, object value)
        {
            return this.ApplyComparisonFilter(propertyName, value, Expression.LessThanOrEqual);
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

        /// <summary>
        /// Join the elements of two collections based on matching keys.
        /// </summary>
        /// <typeparam name="TInner">The type of the elements in the inner collection.</typeparam>
        /// <typeparam name="TKey">The type of the common key shared by both entities.</typeparam>
        /// <typeparam name="TResult">The type of the output elements created by the join operation.</typeparam>
        /// <param name="innerCollection">The inner collection to join with the outer collection.</param>
        /// <param name="outerKeySelector">A lambda expression to extract the join key from the outer collection.</param>
        /// <param name="innerKeySelector">A lambda expression to extract the join key from the inner collection.</param>
        /// <param name="resultSelector">A lambda expression that defines how to combine the two matching elements into a new result.</param>
        /// <returns>The current instance of the <see cref="QueryBuilder{T}"/> to enable fluent method chaining.</returns>
        public QueryBuilder<TResult> Join<TInner, TKey, TResult>(
            IEnumerable<TInner> innerCollection,
            Expression<Func<T, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<T, TInner, TResult>> resultSelector)
        {
            IQueryable<TResult> query = this._resultantQuery.Join(
                innerCollection,
                outerKeySelector,
                innerKeySelector,
                resultSelector);

            return new QueryBuilder<TResult>(query);
        }

        private QueryBuilder<T> ApplyStringFilter(string propertyName, Expression<Func<string, string, bool>> stringMethod, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(value, typeof(string));

            var body = Expression.Invoke(stringMethod, property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

            return this.Filter(lambda);
        }

        private QueryBuilder<T> ApplyComparisonFilter(string propertyName, object value, Func<Expression, Expression, BinaryExpression> comparisonFactory)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(value, property.Type);

            var body = comparisonFactory(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

            return this.Filter(lambda);
        }
    }
}
