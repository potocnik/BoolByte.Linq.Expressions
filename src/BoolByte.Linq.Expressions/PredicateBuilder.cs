using BoolByte.Linq.Expressions.Extensions;
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

        public Expression<Func<T, bool>> Build<T>()
        {
            var parameterExpression = Expression.Parameter(typeof(T), "i");
            if (!_filters.Any())
            {
                var valueExpression = Expression.Constant(true);
                return Expression.Lambda<Func<T, bool>>(valueExpression, parameterExpression);
            }
            var andConditions = _filters
                .Select(parameterExpression.ToCompare);
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
