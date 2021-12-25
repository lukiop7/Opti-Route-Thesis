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
   public class VehicleTests
    {
        [Test]
        public void ShouldReturnDeepCopy()
        {
            Vehicle original = new Vehicle(1, 1, 1, 1);

            Vehicle cloned = original.Clone();

            cloned.Should().BeEquivalentTo(original);
            Assert.AreNotSame(original, cloned);           
        }

    }
}
