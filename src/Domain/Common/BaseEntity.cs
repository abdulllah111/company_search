using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public required Guid Id { get; set; }
        public required DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}