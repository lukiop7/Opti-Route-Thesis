using OptiRoute.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace OptiRoute.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
