namespace MoreDateTime.Tests.Extensions
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using MoreDateTime;
	using MoreDateTime.Extensions;

	using Shouldly;

	/// <summary>
	/// Unit tests for the type <see cref="DateOnlyExtensions"/>.
	/// </summary>
	public partial class DateOnlyExtensionsTests
	{
		private readonly DateOnly _startDate1 = new DateOnly(2020, 01, 01);
		private readonly DateOnly _endDate1   = new DateOnly(2020, 01, 10);
		private readonly DateOnly _startDate2 = new DateOnly(2020, 01, 05);
		private readonly DateOnly _endDate2   = new DateOnly(2020, 01, 20);
		private readonly DateOnly _startDate3 = new DateOnly(2020, 02, 01);
		private readonly DateOnly _endDate3   = new DateOnly(2020, 02, 10);

		/// <summary>
		/// Checks that the Union method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Union_With_A_B_C()
		{
			// Arrange
			var a = new DateOnlyRange(_startDate1, _endDate1);
			var b = new DateOnlyRange(_startDate2, _endDate2);
			var c = new DateOnlyRange(_startDate3, _endDate3);

			// Act
			var result1 = a.Union(b);
			var result2 = a.Union(c);
			var result3 = b.Union(a);
			var result4 = b.Union(c);
			var result5 = c.Union(a);
			var result6 = c.Union(b);

			// Assert
			result1.ShouldBeEquivalentTo(result3);
			result2.IsEmpty.ShouldBeTrue();
			result4.IsEmpty.ShouldBeTrue();
			result5.IsEmpty.ShouldBeTrue();
			result6.IsEmpty.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the Union method throws when the a parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_Union_WithNullA()
		{
			Should.Throw<ArgumentNullException>(() => default(DateOnlyRange)!.Union(new DateOnlyRange()));
		}

		/// <summary>
		/// Checks that the Union method throws when the b parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_Union_WithNullB()
		{
			Should.Throw<ArgumentNullException>(() => new DateOnlyRange().Union(default(DateOnlyRange)!));
		}

		/// <summary>
		/// Checks that the Intersection method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Intersection_With_A_B_C()
		{
			// Arrange
			var a = new DateOnlyRange(_startDate1, _endDate1);
			var b = new DateOnlyRange(_startDate2, _endDate2);
			var c = new DateOnlyRange(_startDate3, _endDate3);

			// Act
			var result1 = a.Intersection(b);
			var result2 = a.Intersection(c);
			var result3 = b.Intersection(a);
			var result4 = b.Intersection(c);
			var result5 = c.Intersection(a);
			var result6 = c.Intersection(b);

			// Assert
			result1.ShouldBeEquivalentTo(result3);
			result2.IsEmpty.ShouldBeTrue();
			result4.IsEmpty.ShouldBeTrue();
			result5.IsEmpty.ShouldBeTrue();
			result6.IsEmpty.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the Intersection method throws when the a parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_Intersection_WithNullA()
		{
			Should.Throw<ArgumentNullException>(() => default(DateOnlyRange)!.Intersection(new DateOnlyRange()));
		}

		/// <summary>
		/// Checks that the Intersection method throws when the b parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_Intersection_WithNullB()
		{
			Should.Throw<ArgumentNullException>(() => new DateOnlyRange().Intersection(default(DateOnlyRange)!));
		}

		/// <summary>
		/// Checks that the Difference method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_Difference_With_A_B_C()
		{
			// Arrange
			var a = new DateOnlyRange(_startDate1, _endDate1);
			var b = new DateOnlyRange(_startDate2, _endDate2);
			var c = new DateOnlyRange(_startDate3, _endDate3);

			// Act
			var result1 = a.Difference(b);
			var result2 = a.Difference(c);
			var result3 = b.Difference(a);
			var result4 = b.Difference(c);
			var result5 = c.Difference(a);
			var result6 = c.Difference(b);

			// Assert
			result1.First().Start.ShouldBe(_startDate1);
			result1.First().End.ShouldBe(_startDate2);

			result2.Count().ShouldBe(0);

			result3.Count().ShouldBe(1);
			result3.First().Start.ShouldBe(_endDate1);
			result3.First().End.ShouldBe(_endDate2);

			result4.Count().ShouldBe(0);
			result5.Count().ShouldBe(0);
			result6.Count().ShouldBe(0);
		}

		/// <summary>
		/// Checks that the Difference method throws when the a parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_Difference_WithNullA()
		{
			Should.Throw<ArgumentNullException>(() => default(DateOnlyRange)!.Difference(new DateOnlyRange()));
		}

		/// <summary>
		/// Checks that the Difference method throws when the b parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_Difference_WithNullB()
		{
			Should.Throw<ArgumentNullException>(() => new DateOnlyRange().Difference(default(DateOnlyRange)!));
		}

		/// <summary>
		/// Checks that the DoesOverlap method functions correctly.
		/// </summary>
		[TestMethod]
		public void CanCall_DoesOverlap()
		{
			// Arrange
			var a = new DateOnlyRange(_startDate1, _endDate1);
			var b = new DateOnlyRange(_startDate2, _endDate2);
			var c = new DateOnlyRange(_startDate3, _endDate3);

			// Act
			var result1 = a.DoesOverlap(b);
			var result2 = a.DoesOverlap(c);
			var result3 = b.DoesOverlap(a);
			var result4 = b.DoesOverlap(c);
			var result5 = c.DoesOverlap(a);
			var result6 = c.DoesOverlap(b);

			var result7 = c.DoesOverlap(c);

			// Assert
			result1.ShouldBeTrue();
			result2.ShouldBeFalse();
			result3.ShouldBeTrue();
			result4.ShouldBeFalse();
			result5.ShouldBeFalse();
			result6.ShouldBeFalse();

			result7.ShouldBeTrue();
		}

		/// <summary>
		/// Checks that the DoesOverlap method throws when the a parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_DoesOverlap_WithNullA()
		{
			Should.Throw<NullReferenceException>(() => default(DateOnlyRange)!.DoesOverlap(new DateOnlyRange()));
		}

		/// <summary>
		/// Checks that the DoesOverlap method throws when the b parameter is null.
		/// </summary>
		[TestMethod]
		public void CannotCall_DoesOverlap_WithNullB()
		{
			Should.Throw<NullReferenceException>(() => new DateOnlyRange().DoesOverlap(default(DateOnlyRange)!));
		}
	}
}