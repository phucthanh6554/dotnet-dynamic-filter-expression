using System.Linq.Expressions;

namespace FilterExpression.Directive.Implement
{
    internal class InArrayDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return "in";
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            var function = typeof(ICollection<>).MakeGenericType(property.Type).GetMethod("Contains");

            if (function == null)
                throw new Exception("Contains function is not found");

            return Expression.Call(value, function, property);
        }
    }
}
