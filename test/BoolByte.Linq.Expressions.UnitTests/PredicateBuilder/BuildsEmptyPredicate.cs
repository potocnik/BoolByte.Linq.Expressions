using BoolByte.Linq.Expressions.UnitTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;

namespace BoolByte.Linq.Expressions.UnitTests.PredicateBuilder
{
    public class BuildsEmptyPredicate
    {
        [Test]
        public void WhenNoConditionsAreSupplied()
        {
            var actual = new Expressions.PredicateBuilder()
                .Build<TestClass>();
            actual.Should().NotBeNull();
            var sample = new TestClass { IntValue = int.MaxValue };
            actual.Compile().Invoke(sample).Should().BeTrue();
        }
    }
}
