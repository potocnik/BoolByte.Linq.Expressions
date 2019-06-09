namespace BoolByte.Linq.Expressions.Models
{
    public class Filter
    {
        public string PropertyName { get; set; }
        public CompareTypes CompareType { get; set; }
        public object Value { get; set; }
    }
}
