using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace DF.Core.Models
{
    public class OIdEntity : IEntity<string>
    {
        public OIdEntity()
        {
            this.Id = ObjectId.GenerateNewId().ToString();
        }
        
        [MaxLength(24)]
        public string Id { get; set; }
    }
}
