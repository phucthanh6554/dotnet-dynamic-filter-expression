using System.Linq.Expressions;

namespace FilterExpression.Directive
{
    public interface IFilterDirective
    {
        string FilterSyntax { get; }

        Expression GenerateExpression(ref MemberExpression property, ConstantExpression value);
    }
}
