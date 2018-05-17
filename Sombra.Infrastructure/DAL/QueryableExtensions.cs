using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Enums;

namespace Sombra.Infrastructure.DAL
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IOrderedQueryable<T> queryable, int pageNumber, int pageSize)
            => queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        public static Task<List<TDestination>> ProjectToPagedListAsync<TDestination, TSource>(this IOrderedQueryable<TSource> queryable, int pageNumber, int pageSize, IConfigurationProvider mapperConfiguration)
            => queryable.ApplyPagination(pageNumber, pageSize).ProjectToListAsync<TDestination>(mapperConfiguration);

        public static async Task<List<TDestination>> ProjectToPagedListAsync<TDestination, TKey, TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, SortOrder sortOrder, int pageNumber, int pageSize, IConfigurationProvider mapperConfiguration)
        {
            var orderedQueryable = sortOrder == SortOrder.Asc
                ? queryable.OrderBy(keySelector)
                : queryable.OrderByDescending(keySelector);

            return await orderedQueryable.ProjectToPagedListAsync<TDestination, TSource>(pageNumber, pageSize, mapperConfiguration);
        }

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider mapperConfiguration)
            => queryable.ProjectTo<TDestination>(mapperConfiguration).DecompileAsync().ToListAsync();
    }
}