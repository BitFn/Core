﻿using System.Collections.Generic;
using System.Linq;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods for the <see cref="IQueryable{T}" /> interface.
	/// </summary>
	public static class ForIQueryable
	{
		/// <summary>
		///     Sorts the elements of a sequence in ascending order according to themselves.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <returns>An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted according to themselves.</returns>
		public static IOrderedQueryable<T> Order<T>(this IQueryable<T> source)
		{
			return source.OrderBy(_ => _);
		}

		/// <summary>
		///     Sorts the elements of a sequence in ascending order according to themselves by using a specified comparer.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="comparer">An <see cref="IComparer{T}" /> to compare elements.</param>
		/// <returns>An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted according to themselves.</returns>
		public static IOrderedQueryable<T> Order<T>(this IQueryable<T> source, IComparer<T> comparer)
		{
			return source.OrderBy(_ => _, comparer);
		}

		/// <summary>
		///     Sorts the elements of a sequence in descending order according to themselves.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <returns>An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted according to themselves.</returns>
		public static IOrderedQueryable<T> OrderDescending<T>(this IQueryable<T> source)
		{
			return source.OrderByDescending(_ => _);
		}

		/// <summary>
		///     Sorts the elements of a sequence in descending order according to themselves by using a specified comparer.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="comparer">An <see cref="IComparer{T}" /> to compare elements.</param>
		/// <returns>An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted according to themselves.</returns>
		public static IOrderedQueryable<T> OrderDescending<T>(this IQueryable<T> source, IComparer<T> comparer)
		{
			return source.OrderByDescending(_ => _, comparer);
		}
	}
}