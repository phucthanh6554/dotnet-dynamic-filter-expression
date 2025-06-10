using System.Linq.Expressions;

namespace FilterExpression.Directive.Implement
{
    internal class LessThanDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return "lt";
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            return Expression.LessThan(property, value);
        }
    }
}
