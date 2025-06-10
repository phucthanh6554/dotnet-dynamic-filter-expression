using System.Linq.Expressions;

namespace FilterExpression.Directive.Implement
{
    internal class LessThanOrEqualDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return "le";
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            return Expression.LessThanOrEqual(property, value); 
        }
    }
}
