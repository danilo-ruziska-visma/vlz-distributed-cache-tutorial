using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaNmbrs.DistributedCacheSample.Data.Options
{
    public class AzureCosmoOptions
    {
        public const string AzureCosmo = "AzureCosmo";
        public string EndpointUri { get; set; } = null!;
        public string PrimaryKey { get; set; } = null!;
    }
}
