namespace FilterExpression.Extension
{
    public static class QueryableFilterExtension
    {
        private static readonly FilterService FilterService = new FilterService();

        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, string fe)
        {
            var filter = FilterService.Filter<T>(fe);

            return queryable.Where(filter);
        }
    }
}
