using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Text;
using Sombra.Core.Enums;

namespace Sombra.Infrastructure.DAL
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
            where T : class => queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        public static async Task<List<TDestination>> ProjectToPagedListAsync<TDestination, TSource>(this IQueryable<TSource> queryable, int pageNumber, int pageSize, IConfigurationProvider mapperConfiguration)
            where TSource : class
            where TDestination : class => await queryable.ApplyPagination(pageNumber, pageSize).ProjectToListAsync<TDestination>(mapperConfiguration);

        public static async Task<List<TDestination>> ProjectToPagedListAsync<TDestination, TKey, TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, SortOrder sortOrder, int pageNumber, int pageSize, IConfigurationProvider mapperConfiguration)
            where TSource : class
            where TDestination : class
        {
            var orderedQueryable = sortOrder == SortOrder.Asc
                ? queryable.OrderBy(keySelector)
                : queryable.OrderByDescending(keySelector);

            return await orderedQueryable.ProjectToPagedListAsync<TDestination, TSource>(pageNumber, pageSize, mapperConfiguration);
        }
    }
}