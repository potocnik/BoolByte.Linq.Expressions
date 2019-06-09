using FluentAssertions;
using NUnit.Framework;

namespace BoolByte.Linq.Expressions.UnitTests.PredicateBuilder
{
    public class Build
    {
        [Test]
        public void BuildsPredicate()
        {
            var testSubject = new Expressions.PredicateBuilder();
            var actual = testSubject.Build<object>();
            actual.Should().NotBeNull();
        }
    }
}
