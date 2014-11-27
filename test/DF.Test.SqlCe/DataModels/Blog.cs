using System.Collections.Generic;
using DF.Core.Models;

namespace DF.Test.SqlCe.DataModels
{
    public class Blog : GuidAggregate
    {
        public string Name { get; set; }
        public string Url { get; set; }
        
        public virtual List<Post> Posts { get; set; } 
    }
}
