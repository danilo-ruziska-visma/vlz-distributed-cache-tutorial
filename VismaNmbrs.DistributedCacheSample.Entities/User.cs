using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaNmbrs.DistributedCacheSample.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!; 
    }
}
