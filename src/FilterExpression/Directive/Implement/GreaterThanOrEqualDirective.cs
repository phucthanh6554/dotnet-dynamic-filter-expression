using System.Linq.Expressions;

namespace FilterExpression.Directive.Implement
{
    internal class GreaterThanOrEqualDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return "ge";
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            return Expression.GreaterThanOrEqual(property, value);
        }
    }
}
