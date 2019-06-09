using BoolByte.Linq.Expressions.UnitTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;

namespace BoolByte.Linq.Expressions.UnitTests.PredicateBuilder
{
    public class Build
    {
        [Test]
        public void WhenNoConditionsAreSupplied_BuildsEmptyPredicate()
        {
            var testSubject = new Expressions.PredicateBuilder();
            var actual = testSubject.Build<TestClass>();
            actual.Should().NotBeNull();
            var sample = new TestClass { IntValue = int.MaxValue };
            actual.Compile().Invoke(sample).Should().BeTrue();
        }
    }
}
