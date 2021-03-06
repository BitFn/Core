﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForIEnumerable
{
	[TestFixture]
	public class CountBy
	{
		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TValue, TKey>(
			IEnumerable<TValue> source, Func<TValue, TKey> selector)
		{
			return () => Core.Extensions.ForIEnumerable.CountBy(source, selector);
		}

		[ExcludeFromCodeCoverage]
		private static TestDelegate TestDelegate<TValue, TKey>(
			IEnumerable<TValue> source, Func<TValue, TKey> selector, IEqualityComparer<TKey> comparer)
		{
			return () => Core.Extensions.ForIEnumerable.CountBy(source, selector, comparer);
		}

		[ExcludeFromCodeCoverage]
		private static object SelectorDelegate(object obj) => obj;

		[Test]
		public void WhenCountingByItem_ShouldGroup()
		{
			// Arrange
			var enumerable = new[] {1, 1, 2, 3, 3, 4};
			var expected = new Dictionary<int, int> {[1] = 2, [2] = 1, [3] = 2, [4] = 1};
			Func<int, int> selector = _ => _;

			// Act
			var actual = Core.Extensions.ForIEnumerable.CountBy(enumerable, selector);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenCountingBySelector_ShouldGroup()
		{
			// Arrange
			var enumerable = new[] {1, 1, 2, 3, 3, 4};
			var expected = new Dictionary<string, int> {["1"] = 2, ["2"] = 1, ["3"] = 2, ["4"] = 1};
			Func<int, string> selector = _ => _.ToString();

			// Act
			var actual = Core.Extensions.ForIEnumerable.CountBy(enumerable, selector);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenGivenNullComparer_ShouldUseDefault()
		{
			// Arrange
			var enumerable = new[] {1, 1, 2, 3, 3, 4};
			var expected = new Dictionary<int, int> {[1] = 2, [2] = 1, [3] = 2, [4] = 1};
			Func<int, int> selector = _ => _;
			var comparer = null as IEqualityComparer<int>;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.CountBy(enumerable, selector, comparer);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenGivenComparer_ShouldUse()
		{
			// Arrange
			var enumerable = new[] {"A", "a", "b", "C", "d"};
			var expected = new Dictionary<string, int> {["A"] = 2, ["b"] = 1, ["C"] = 1, ["d"] = 1};
			Func<string, string> selector = _ => _;
			var comparer = StringComparer.InvariantCultureIgnoreCase;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var actual = Core.Extensions.ForIEnumerable.CountBy(enumerable, selector, comparer);

			// Assert
			CollectionAssert.AreEquivalent(expected, actual);
		}

		[Test]
		public void WhenGivenNullSelector_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = new[] {new object()};
			var selector = null as Func<object, object>;

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
		public void WhenGivenNullSelectorWithComparer_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = new[] {new object()};
			var selector = null as Func<object, object>;
			var comparer = EqualityComparer<object>.Default;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, selector, comparer);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenGivenNullEnumerableWithComparer_ShouldThrowArgumentNullException()
		{
			// Arrange
			var enumerable = null as IEnumerable<object>;
			var comparer = EqualityComparer<object>.Default;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, SelectorDelegate, comparer);

			// Assert
			Assert.Throws<ArgumentNullException>(code);
		}

		[Test]
		public void WhenSelectorReturnsNull_ShouldThrowArgumentException()
		{
			// Arrange
			var enumerable = new[] {1, 1, 2, 3, 3, 4};
			Func<int, int?> selector = _ => null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, selector);

			// Assert
			Assert.Throws<ArgumentException>(code);
		}

		[Test]
		public void WhenSelectorReturnsNullWithComparer_ShouldThrowArgumentException()
		{
			// Arrange
			var enumerable = new[] {1, 1, 2, 3, 3, 4};
			Func<int, int?> selector = _ => null;
			var comparer = EqualityComparer<int?>.Default;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			var code = TestDelegate(enumerable, selector, comparer);

			// Assert
			Assert.Throws<ArgumentException>(code);
		}
	}
}