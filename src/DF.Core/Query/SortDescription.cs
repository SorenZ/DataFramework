using System;

namespace DF.Core.Query
{
    /// <summary>
    /// Represents a sort description.
    /// </summary>
    public class SortDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortDescription" /> class.
        /// </summary>
        /// <param name="propertyName"> Name of the property. </param>
        /// <param name="direction"> The direction. </param>
        public SortDescription(string propertyName, SortDirection direction)
        {
            this.PropertyName = propertyName;
            this.Direction = direction;
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value> The direction. </value>
        public SortDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value> The name of the property. </value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Parses the specified sort expression.
        /// </summary>
        /// <param name="sortExpression"> The sort expression. </param>
        /// <returns> </returns>
        public static SortDescription Parse(string sortExpression)
        {
            string[] parts = sortExpression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 2 || parts.Length == 0)
            {
                throw new FormatException("Invalid sort expression");
            }

            string propertyName = parts[0];
            var direction = SortDirection.Ascending;

            if (parts.Length == 2 && parts[1].ToLower() == "desc")
            {
                direction = SortDirection.Descending;
            }

            return new SortDescription(propertyName, direction);
        }

        /// <summary>
        /// Tries the parse.
        /// </summary>
        /// <param name="sortExpression"> The sort expression. </param>
        /// <param name="sortDescription"> The sort description. </param>
        /// <returns> </returns>
        public static bool TryParse(string sortExpression, out SortDescription sortDescription)
        {
            if (string.IsNullOrWhiteSpace(sortExpression))
            {
                sortDescription = null;
                return false;
            }

            try
            {
                sortDescription = Parse(sortExpression);
                return true;
            }
            catch (FormatException)
            {
                sortDescription = null;
                return false;
            }
        }
    }

    /// <summary>
    /// Specifies the direction of a sort operation.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Ascending 
        /// </summary>
        Ascending,

        /// <summary>
        /// Descending
        /// </summary>
        Descending
    }
}
