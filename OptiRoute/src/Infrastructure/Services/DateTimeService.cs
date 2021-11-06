using OptiRoute.Application.Common.Interfaces;
using System;

namespace OptiRoute.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
