using System.Linq.Expressions;

namespace FilterExpression.Directive
{
    internal class GreaterThanDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return "gt";
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            return Expression.GreaterThan(property, value);
        }
    }
}
