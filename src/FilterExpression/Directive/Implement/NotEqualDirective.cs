using System.Linq.Expressions;

namespace FilterExpression.Directive
{
    internal class NotEqualDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return "ne";
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            return Expression.NotEqual(property, value);
        }
    }
}
