using SharedKernel.Models;

using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace GenericRepositoryEntityFramework
{
    public static class SortingExtension
    {
        public static IQueryable<T> GetOrdering<T>(IQueryable<T> source, List<SortParam> SortParams)
        {
            IOrderedQueryable<T> myorder = null;

            foreach (var SortParam in SortParams)
            {
                if (myorder == null)
                {
                    myorder = SortParam.SortOrderDescending.HasValue && SortParam.SortOrderDescending.Value

                        ? source.OrderByDescending(SortParam.OrderProperty)

                        : source.OrderBy(SortParam.OrderProperty);
                }
                else
                {
                    myorder = SortParam.SortOrderDescending.HasValue && SortParam.SortOrderDescending.Value

                        ? myorder.ThenByDescending(SortParam.OrderProperty)

                        : myorder.ThenBy(SortParam.OrderProperty);

                }
            }

            return myorder;
        }


        private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        {
            var param = Expression.Parameter(typeof(T), "x");

            Expression body = param;

            foreach (var props in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, props);
            }

            var sort = Expression.Lambda(body, param);

            MethodCallExpression call = Expression.Call(typeof(Queryable),
                                        (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                                        new[] { typeof(T), body.Type },
                                        source.Expression,
                                        Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }


        private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, false);
        }


        private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, false);
        }


        private static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, true);
        }


        private static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, true);
        }

    }

}
