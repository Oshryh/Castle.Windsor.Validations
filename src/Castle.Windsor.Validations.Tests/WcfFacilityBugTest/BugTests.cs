using System;
using Castle.Windsor.Validations;
using FluentAssertions;
using NUnit.Framework;

namespace Castle.Windsor.Validations.Tests.WcfFacilityBugTest
{
    public class WcfFacilityBugTests
    {

        [Test]
        public void ResolveInstallerWithWcfFacilityTest()
        {
            var action = ((Action)(() => new WcfFacilityTestInstaller().ValidateAllDependenciesResolvable()));
            action.Should().NotThrow();
        }
    }
}