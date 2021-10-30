using OptiRoute.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptiRoute.Domain.Entities
{
    public class Test : AuditableBaseEntity
    {
        public string Name { get; set; }
    }
}
