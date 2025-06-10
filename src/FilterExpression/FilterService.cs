using FilterExpression.Directive;
using FilterExpression.DirectiveDispatcher;
using System.Collections;
using System.Linq.Expressions;

namespace FilterExpression
{
    public class FilterService
    {
        private static readonly DirectiveDispatchService DispatchService = new DirectiveDispatchService();
        private static readonly ParseValueService ParseValueService = new ParseValueService();

        public Expression<Func<T, bool>> Filter<T>(string fe)
        {
            if (string.IsNullOrEmpty(fe))
                return x => true;

            List<object> output = new List<object>();
            Stack<string> stack = new Stack<string>();

            string str = "";
            var operators = new List<string> { "(", "&", "|", "!" };

            int singleQuoteCount = 0;

            var parameter = Expression.Parameter(typeof(T), "x");

            foreach (var c in fe)
            {
                if (operators.Contains(c.ToString()))
                {
                    if (stack.Any() && stack.First() == "!" && c != '!')
                    {
                        stack.Pop();
                        stack.Push(c.ToString());
                        stack.Push("!");
                        continue;
                    }

                    stack.Push(c.ToString());
                    continue;
                }

                if (c == '`')
                    singleQuoteCount++;

                if (singleQuoteCount > 0 && singleQuoteCount % 2 == 0)
                {
                    var expression = StringToExpression<T>(str, ref parameter);

                    output.Add(expression);

                    str = "";
                    singleQuoteCount = 0;
                    continue;
                }
                else if (c == ')')
                {
                    output.AddRange(GetStack(ref stack));
                }
                else
                {
                    str += c;
                }
            }

            output.AddRange(GetStack(ref stack));

            var resultStack = new Stack();

            for (var i = 0; i < output.Count; i++)
            {
                if (output[i] is not string)
                    resultStack.Push(output[i]);
                else if (output[i] is string && (string)output[i] != "!")
                {
                    var val1 = (Expression)resultStack.Pop()!;
                    var val2 = (Expression)resultStack.Pop()!;

                    if (output[i].ToString() == "&")
                        resultStack.Push(Expression.And(val1, val2));
                    else if (output[i].ToString() == "|")
                        resultStack.Push(Expression.Or(val1, val2));
                }
                else if (output[i] is string && (string)output[i] == "!")
                {
                    var val1 = (Expression)resultStack.Pop()!;
                    resultStack.Push(Expression.Not(val1));
                }
            }

            var resultExpression = resultStack.Pop();

            return Expression.Lambda<Func<T, bool>>((Expression)resultExpression!, parameter);
        }

        private List<object> GetStack(ref Stack<string> stack)
        {
            var result = new List<object>();

            var current = "";

            while (current != "(" && stack.Count != 0)
            {
                current = stack.Pop();

                if (current != "(")
                    result.Add(current);
            }

            return result;
        }

        private Expression StringToExpression<T>(string str, ref ParameterExpression parameter)
        {
            var strData = str.Trim().Split(' ', 3);

            Expression? result = null;

            if (strData.Length == 3)
            {
                var nestedProperties = strData[0].Split('.');
                
                var property = Expression.Property(parameter, nestedProperties[0]);
                
                for(var i = 1; i < nestedProperties.Length; i++)
                    property = Expression.Property(property, nestedProperties[i]);

                ConstantExpression value = ParseValueService.GetConstantExpression(strData[2], property.Type);

                IFilterDirective filterDirective = DispatchService.GetDirective(strData[1]);

                return filterDirective.GenerateExpression(ref property, value);               
            }

            return result!;
        }
    }
}
