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
        public static Task<List<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
            => queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        public static Task<List<TDestination>> ProjectToPagedListAsync<TDestination>(this IQueryable queryable, int pageNumber, int pageSize, IMapper mapper)
            => queryable.ProjectTo<TDestination>(mapper.ConfigurationProvider).DecompileAsync().ToPagedListAsync(pageNumber, pageSize);

        public static Task<List<TDestination>> ProjectToPagedListAsync<TDestination, TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, SortOrder sortOrder, int pageNumber, int pageSize, IMapper mapper)
            => queryable.OrderBy(keySelector, sortOrder).ProjectToPagedListAsync<TDestination>(pageNumber, pageSize, mapper);

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, SortOrder sortOrder)
            => sortOrder == SortOrder.Asc
            ? queryable.OrderBy(keySelector)
            : queryable.OrderByDescending(keySelector);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IMapper mapper)
            => queryable.ProjectTo<TDestination>(mapper.ConfigurationProvider).DecompileAsync().ToListAsync();
    }
}