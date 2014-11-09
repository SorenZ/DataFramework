using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

using DF.Core.Query;

namespace DF.EntityFramework.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Sorts the specified query.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="query"> The query. </param>
        /// <param name="sortDescriptions"> The sort descriptions. </param>
        /// <returns> </returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, SortDescription[] sortDescriptions)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            if (sortDescriptions != null)
            {
                foreach (SortDescription sortDescription in sortDescriptions.Reverse())
                {
                    string property = sortDescription.PropertyName;
                    if (sortDescription.Direction == SortDirection.Descending)
                    {
                        property += " DESC";
                    }

                    query = query.OrderBy(property);
                }
            }

            return query;
        }

        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="query"> The query. </param>
        /// <param name="startIndex"> Start index of the row. </param>
        /// <param name="itemCount"> Size of the page. </param>
        /// <param name="sortDescriptions"> The sort descriptions. </param>
        /// <returns> </returns>
        public static IQueryable<T> ToPage<T>(this IQueryable<T> query, int startIndex, int itemCount, SortDescription[] sortDescriptions)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            if (sortDescriptions != null)
            {
                foreach (SortDescription sortDescription in sortDescriptions.Reverse())
                {
                    string property = sortDescription.PropertyName;
                    if (sortDescription.Direction == SortDirection.Descending)
                    {
                        property += " DESC";
                    }

                    query = query.OrderBy(property);
                }
            }

            if (startIndex < 0)
            {
                startIndex = 0;
            }

            return query.Skip(startIndex).Take(itemCount);
        }

        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IQueryable<T> ToPage<T>(this IQueryable<T> query, int? page, int pageSize, string sortExpression)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.ToPage(
                ((page ?? 1) - 1) * pageSize,
                pageSize,
                GetSortDescriptions(sortExpression).ToArray());
        }

        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="paginatedCriteria">The paginated criteria.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">query</exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IQueryable<T> ToPage<T>(this IQueryable<T> query, IPaginatedCriteria paginatedCriteria)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.ToPage(paginatedCriteria.StartIndex, paginatedCriteria.PageSize, paginatedCriteria.SortDescriptions);
        }

        /// <summary>
        /// Creates an <see cref="IPaginated{T}" /> instance from the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="paginatedCriteria">The paginated criteria.</param>
        /// <returns></returns>
        public static IPaginated<T> ToPaginated<T>(this IQueryable<T> query, IPaginatedCriteria paginatedCriteria)
        {
            return new Paginated<T>(
                query.ToPage(paginatedCriteria.StartIndex, paginatedCriteria.PageSize, paginatedCriteria.SortDescriptions),
                query.Count(),
                paginatedCriteria.CurrentPage,
                paginatedCriteria.PageSize);
        }

        /// <summary>
        /// Gets the sort descriptions from the specified criteria.
        /// </summary>
        /// <param name="sort">The sort.</param>
        /// <returns></returns>
        private static IEnumerable<SortDescription> GetSortDescriptions(string sort)
        {
            SortDescription sortDescription;
            if (SortDescription.TryParse(sort, out sortDescription))
            {
                yield return sortDescription;
            }
        }
    }
}
