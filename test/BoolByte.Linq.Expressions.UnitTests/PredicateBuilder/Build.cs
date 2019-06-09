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
            var actual = new Expressions.PredicateBuilder()
                .Build<TestClass>();
            actual.Should().NotBeNull();
            var sample = new TestClass { IntValue = int.MaxValue };
            actual.Compile().Invoke(sample).Should().BeTrue();
        }

        [Test]
        public void WhenSingleSimpleConditionsSIsSupplied_BuildsExpectedPredicate()
        {
            var actual = new Expressions.PredicateBuilder()
                .WithFilter("IntValue", int.MaxValue)
                .Build<TestClass>();
            actual.Should().NotBeNull();
            var sample = new TestClass { IntValue = 0 };
            actual.Compile().Invoke(sample).Should().BeFalse();
        }
    }
}
