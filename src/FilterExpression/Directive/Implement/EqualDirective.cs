using System.Linq.Expressions;

namespace FilterExpression.Directive
{
    internal class EqualDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return "eq";
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            return Expression.Equal(property, value);
        }
    }
}
