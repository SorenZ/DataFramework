using System;
using System.Linq;
using System.Collections.Generic;

namespace DF.Core.Query
{
    /// <summary>
    /// Represents the criteria used to show a paginated data.
    /// </summary>
    /// <remarks>
    /// The <see cref="PaginatedCriteria"/> properties are intentionally left without property change notification.
    /// </remarks>
    public class PaginatedCriteria : IPaginatedCriteria
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedCriteria" /> class.
        /// </summary>
        public PaginatedCriteria()
        {
            this.PageSize = 10;
            this.CurrentPage = 1;
        }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <remarks>
        /// Starts from 1.
        /// </remarks>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        /// <remarks>
        /// The default value is 10.
        /// </remarks>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the sort descriptions.
        /// </summary>
        public SortDescription[] SortDescriptions { get; set; }

        /// <summary>
        /// Gets the start index according to the current page and page size.
        /// </summary>
        public int StartIndex
        {
            get { return (this.CurrentPage - 1)*this.PageSize; }
        }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        public string SortExpression
        {
            get
            {
                if (this.SortDescriptions == null)
                {
                    return string.Empty;
                }

                var sortExpressions = this.SortDescriptions
                    .Select(sd => sd.PropertyName + (sd.Direction == SortDirection.Descending ? "DESC" : string.Empty))
                    .ToArray();

                return string.Join(", ", sortExpressions);
            }

            set
            {
                if (value == null)
                {
                    this.SortDescriptions = null;
                    return;
                }

                this.SortDescriptions = ParseSortExpression(value).ToArray();
            }
        }

        /// <summary>
        /// Parses the specified sort expression.
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        private static IEnumerable<SortDescription> ParseSortExpression(string sortExpression)
        {
            var sortExpressions = sortExpression.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var sort in sortExpressions)
            {
                SortDescription sortDescription;
                if (SortDescription.TryParse(sort, out sortDescription))
                {
                    yield return sortDescription;
                }
            }
        }
    }
}
