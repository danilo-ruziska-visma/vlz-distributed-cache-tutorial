using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaNmbrs.DistributedCacheSample.Entities
{
    public class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
    }
}
