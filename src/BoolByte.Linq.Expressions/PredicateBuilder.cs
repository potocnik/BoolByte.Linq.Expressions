using System;
using System.Linq.Expressions;

namespace BoolByte.Linq.Expressions
{
    public class PredicateBuilder
    {
        public Expression<Func<T, bool>> Build<T>()
        {
            throw new NotImplementedException();
        }
    }
}
