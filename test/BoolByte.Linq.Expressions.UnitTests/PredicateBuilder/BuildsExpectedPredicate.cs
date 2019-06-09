using BoolByte.Linq.Expressions.UnitTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;

namespace BoolByte.Linq.Expressions.UnitTests.PredicateBuilder
{
    public class BuildsExpectedPredicate
    {
        [Test]
        public void WhenSingleEqualConditionIsSupplied()
        {
            var actual = new Expressions.PredicateBuilder()
                .WithFilter("IntValue", int.MaxValue)
                .Build<TestClass>();
            var sample = new TestClass { IntValue = 0 };
            actual.Compile().Invoke(sample).Should().BeFalse();
        }

        [Test]
        public void WhenSingleGreaterThanConditionIsSupplied()
        {
            var actual = new Expressions.PredicateBuilder()
                .WithFilter("IntValue", 999, CompareTypes.GreaterThan)
                .Build<TestClass>();
            var sample = new TestClass { IntValue = 1000 };
            actual.Compile().Invoke(sample).Should().BeTrue();
        }

        [Test]
        public void WhenSingleGreaterThanOrEqualConditionIsSupplied()
        {
            var actual = new Expressions.PredicateBuilder()
                .WithFilter("IntValue", 999, CompareTypes.GreaterThanOrEqual)
                .Build<TestClass>();
            var sample = new TestClass { IntValue = 999 };
            actual.Compile().Invoke(sample).Should().BeTrue();
        }

        [Test]
        public void WhenSingleLessThanConditionIsSupplied()
        {
            var actual = new Expressions.PredicateBuilder()
                .WithFilter("IntValue", 1000, CompareTypes.LessThan)
                .Build<TestClass>();
            var sample = new TestClass { IntValue = 999 };
            actual.Compile().Invoke(sample).Should().BeTrue();
        }

        [Test]
        public void WhenSingleLessThanOrEqualConditionIsSupplied()
        {
            var actual = new Expressions.PredicateBuilder()
                .WithFilter("IntValue", 1000, CompareTypes.LessThanOrEqual)
                .Build<TestClass>();
            var sample = new TestClass { IntValue = 1000 };
            actual.Compile().Invoke(sample).Should().BeTrue();
        }

        [Test]
        public void WhenRangeFiltersAreSupplied()
        {
            var actual = new Expressions.PredicateBuilder()
                .WithFilter("IntValue", 2, CompareTypes.GreaterThanOrEqual)
                .WithFilter("IntValue", 4, CompareTypes.LessThanOrEqual)
                .Build<TestClass>()
                .Compile();
            actual.Invoke(new TestClass { IntValue = 1 }).Should().BeFalse("Failed for 1");
            actual.Invoke(new TestClass { IntValue = 2 }).Should().BeTrue("Failed for 2");
            actual.Invoke(new TestClass { IntValue = 3 }).Should().BeTrue("Failed for 3");
            actual.Invoke(new TestClass { IntValue = 4 }).Should().BeTrue("Failed for 4");
            actual.Invoke(new TestClass { IntValue = 5 }).Should().BeFalse("Failed for 5");
        }
    }
}
