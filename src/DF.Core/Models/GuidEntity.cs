using System;

namespace DF.Core.Models
{
    /// <summary>
    /// Represents an entity with standard GUID key.
    /// </summary>
    /// <remarks>
    /// Used to set the GUID key automatically.
    /// </remarks>
    public class GuidEntity : IEntity<Guid>
    {
        public GuidEntity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
