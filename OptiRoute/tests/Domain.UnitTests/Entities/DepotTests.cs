using FluentAssertions;
using NUnit.Framework;
using OptiRoute.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Domain.UnitTests.Entities
{
    public class DepotTests
    {
        [Test]
        public void ShouldParseCustomerFromString()
        {
            Depot depot = Depot.Parse("    0      40         50          0          0       1236          0   ");


            depot.Should().NotBeNull();
            depot.Id.Should().Be(0);
            depot.X.Should().Be(40);
            depot.Y.Should().Be(50);
            depot.DueDate.Should().Be(1236);
        }

    }
}
