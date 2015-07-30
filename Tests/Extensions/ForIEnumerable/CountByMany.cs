﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class CountByMany
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TValue, TKey>(
			IEnumerable<TValue> source, Func<TValue, IEnumerable<TKey>> selector)
		{
			return () => Core.Extensions.ForIEnumerable.CountByMany(source, selector);
		}

		[ExcludeFromCodeCoverage]
		private static IEnumerable<object> SelectorDelegate(object obj) => new[] {obj};

		[Test]
		public void WhenCountingBySelector_ShouldGroup()
		{
			// Arrange
			var enumerable = new[] {"a", "ab", "bc", "c", "d"};
			var expected = new Dictionary<char, int> {['a'] = 2, ['b'] = 2, ['c'] = 2, ['d'] = 1};
			Func<string, IEnumerable<char>> selector = _ => _;

			// Act
			var actual = Core.Extensions.ForIEnumerable.CountByMany(enumerable, selector);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenCountingWithEmptyKeys_ShouldNotGroup()
		{
			// Arrange
			var enumerable = new[] {string.Empty, "a", "ab", "bc", "c", "d"};
			var expected = new Dictionary<char, int> {['a'] = 2, ['b'] = 2, ['c'] = 2, ['d'] = 1};
			Func<string, IEnumerable<char>> selector = _ => _;

			// Act
			var actual = Core.Extensions.ForIEnumerable.CountByMany(enumerable, selector);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenGivenNullSelector_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = new[] {new object()};
			var selector = null as Func<object, IEnumerable<object>>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, selector);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullEnumerable_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = null as IEnumerable<object>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, SelectorDelegate);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenSelectorReturnsNull_ShouldThrowArgumentException()
		{
			// Arrange
			var enumerable = new[] {1, 1, 2, 3, 3, 4};
			Func<int, IEnumerable<int>> selector = _ => null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, selector);

			// Assert
			Assert.Throws<ArgumentException>(code);
		}
	}
}