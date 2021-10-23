using System;
using System.Collections.Generic;
using System.Text;

namespace OptiRoute.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
