using IndicativeExchangeRates.FilterSort.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.FilterSort
{
    public static class CreateExpressionTree
    {
        public static Expression<Func<T, bool>> ConstructFilterExpressionTree<T>(List<ExpressionFilter> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
            {
                exp = ExpressionRetriever.GetFilterExpression<T>(param, filters[0]);
            }
            else
            {
                exp = ExpressionRetriever.GetFilterExpression<T>(param, filters[0]);
                for (int i = 1; i < filters.Count; i++)
                {
                    if (filters[i].LogicalOperator == LogicalOperation.And)
                    {
                        exp = Expression.And(exp, ExpressionRetriever.GetFilterExpression<T>(param, filters[i]));
                    }
                    else if (filters[i].LogicalOperator == LogicalOperation.Or)
                    {
                        exp = Expression.Or(exp, ExpressionRetriever.GetFilterExpression<T>(param, filters[i]));
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }        

        public static MethodCallExpression GenerateOrderMethodCall<T>(IQueryable<T> source, List<ExpressionSort> sorts) where T : class
        {
            if (sorts.Count == 0)
                throw new InvalidOperationException();

            Type type = typeof(T);
            Type selectorResultType;
            LambdaExpression selector = ExpressionRetriever.GetSortExpression<T>(sorts[0].PropertyName.ToString(), out selectorResultType);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable),
                "OrderBy" + (sorts[0].SortDirection == SortDirection.Descending ? sorts[0].SortDirection.ToString() : string.Empty),
                new Type[] { type, selectorResultType },
                source.Expression,
                Expression.Quote(selector));

            if (sorts.Count > 1)
            {
                for (int i = 1; i < sorts.Count; i++)
                {
                    selector = ExpressionRetriever.GetSortExpression<T>(sorts[i].PropertyName.ToString(), out selectorResultType);
                    resultExp = Expression.Call(typeof(Queryable),
                        "ThenBy" + (sorts[i].SortDirection == SortDirection.Descending ? sorts[i].SortDirection.ToString() : string.Empty),
                        new Type[] { type, selectorResultType },
                        resultExp,
                        Expression.Quote(selector));
                }
            }           

            return resultExp;
        }        
    }
}
