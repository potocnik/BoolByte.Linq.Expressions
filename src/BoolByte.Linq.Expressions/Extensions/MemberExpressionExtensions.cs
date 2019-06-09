using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace BoolByte.Linq.Expressions.Extensions
{
    public static class MemberExpressionExtensions
    {
        public static Expression ToConstant(this MemberExpression expression, object value)
        {
            if (value.GetType().Equals(expression.Type))
                return Expression.Constant(value);
            var concreteValue = TypeDescriptor.GetConverter(expression.Type).ConvertFromInvariantString(value.ToString());
            var valueExpression = Expression.Constant(concreteValue);
            return Expression.Convert(valueExpression, expression.Type);
        }

        public static Expression ToCompare(this MemberExpression expression, CompareTypes compareType, object value)
        {
            switch (compareType)
            {
                case CompareTypes.Equals:
                    return Expression.Equal(expression, expression.ToConstant(value));
                case CompareTypes.GreaterThan:
                    return Expression.GreaterThan(expression, expression.ToConstant(value));
                case CompareTypes.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(expression, expression.ToConstant(value));
                case CompareTypes.LessThan:
                    return Expression.LessThan(expression, expression.ToConstant(value));
                case CompareTypes.LessThanOrEqual:
                    return Expression.LessThanOrEqual(expression, expression.ToConstant(value));
            }
            throw new NotImplementedException($"Requested compare type {compareType} has not been implemented yet");
        }
    }
}
