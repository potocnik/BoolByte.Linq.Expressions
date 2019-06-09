using BoolByte.Linq.Expressions.Models;
using System.Linq.Expressions;

namespace BoolByte.Linq.Expressions.Extensions
{
    public static class ParameterExpressionExtensions
    {
        public static Expression ToCompare(this ParameterExpression expression, Filter filter)
        {
            var memberExpression = Expression.PropertyOrField(expression, filter.PropertyName);
            return memberExpression.ToCompare(filter.CompareType, filter.Value);
        }
    }
}
