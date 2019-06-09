using BoolByte.Linq.Expressions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace BoolByte.Linq.Expressions
{
    public class PredicateBuilder
    {
        private readonly List<Filter> _filters;

        public PredicateBuilder()
        {
            _filters = new List<Filter>();
        }

        private Expression ToConstant(MemberExpression memberExpression, object value)
        {
            if (value.GetType().Equals(memberExpression.Type))
                return Expression.Constant(value);
            var concreteValue = TypeDescriptor.GetConverter(memberExpression.Type).ConvertFromInvariantString(value.ToString());
            var valueExpression = Expression.Constant(concreteValue);
            return Expression.Convert(valueExpression, memberExpression.Type);
        }

        private Expression ToCompare(MemberExpression memberExpression, CompareTypes compareType, object value)
        {
            switch (compareType)
            {
                case CompareTypes.Equals:
                    return Expression.Equal(memberExpression, ToConstant(memberExpression, value));
            }
            throw new NotImplementedException($"Requested compare type {compareType} has not been implemented yet");
        }

        private Expression ToCompare(ParameterExpression parameterExpression, Filter filter)
        {
            var memberExpression = Expression.PropertyOrField(parameterExpression, filter.PropertyName);
            return ToCompare(memberExpression, filter.CompareType, filter.Value);
        }

        public Expression<Func<T, bool>> Build<T>()
        {
            var parameterExpression = Expression.Parameter(typeof(T), "i");
            if (!_filters.Any())
            {
                var valueExpression = Expression.Constant(true);
                return Expression.Lambda<Func<T, bool>>(valueExpression, parameterExpression);
            }
            var andConditions = _filters
                .Select(f => ToCompare(parameterExpression, f));
            var expression = andConditions.First();
            andConditions
                .Skip(1)
                .ToList()
                .ForEach(e => expression = Expression.AndAlso(expression, e));
            return Expression.Lambda<Func<T, bool>>(expression, parameterExpression);

        }

        public PredicateBuilder WithFilter(string propertyName, object value, CompareTypes compareType = CompareTypes.Equals)
        {
            _filters.Add(new Filter { PropertyName = propertyName, Value = value, CompareType = compareType });
            return this;
        }
    }
}
