using System;
using System.Linq.Expressions;

namespace BoolByte.Linq.Expressions
{
    public class PredicateBuilder
    {
        public Expression<Func<T, bool>> Build<T>()
        {
            var parameterExpression = Expression.Parameter(typeof(T), "i");
            var valueExpression = Expression.Constant(true);
            return Expression.Lambda<Func<T, bool>>(valueExpression, parameterExpression);
        }
    }
}
