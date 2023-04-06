// Copyright 2022 The MoreDateTime Authors. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE.txt file.

using System;
using System.Runtime.CompilerServices;
using System.Threading;

using MoreDateTime.Extensions;
using MoreDateTime.Interfaces;

namespace MoreDateTime
{
	/// <summary>
	/// A date and time provider, that can be used to simulate a certain date or time<br/>
	/// When no mock date/time is set, it returns the datetime values from the system,
	/// otherwise the set value. The mock values does not change or reflect time passing.
	/// </summary>
	public class DateTimeProvider : IDateTimeProvider
	{
		#region Private Fields

		private static AsyncLocal<DateTime?> mockDateTimeNow = new AsyncLocal<DateTime?>();
		private static AsyncLocal<DateTime?> mockDateTimeUtcNow = new AsyncLocal<DateTime?>();
		private static IDateTimeProvider? currentProvider = null;
		private static AsyncLocal<bool> bNowIsUtc = new AsyncLocal<bool>();
		#endregion Private Fields

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeProvider"/> class.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3010:Static fields should not be updated in constructors", Justification = "We guarantee that the member is only updated when it is null")]
		public DateTimeProvider(bool nowIsUtc = true)
		{
			if (currentProvider is null)
			{
				currentProvider = this as IDateTimeProvider;
			}

			bNowIsUtc.Value = nowIsUtc;
		}

		#region Public Properties

		/// <summary>
		/// Gets the current DateTimeProvider 
		/// </summary>
		public static IDateTimeProvider? Current 
		{ 
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return currentProvider; } 
		}

		/// <inheritdoc/>
		public DateTime Now
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if(bNowIsUtc.Value)
				{
					return mockDateTimeUtcNow.Value ?? DateTime.UtcNow;
				}
				else
				{
					return mockDateTimeNow.Value ?? DateTime.Now;
				}
			}
		}

		/// <inheritdoc/>
		public DateTime Today
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Now.TruncateToDay();
			}
		}

		/// <inheritdoc/>
		public DateTime UtcToday
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.UtcNow.TruncateToDay();
			}
		}

		/// <inheritdoc/>
		public DateTime UtcNow
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return mockDateTimeUtcNow.Value ?? DateTime.UtcNow;
			}
		}

		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Sets the mock date time. This substitutes delivering the current date time from the system. Use for testing and verification.
		/// </summary>
		/// <param name="dtNow">The fixed DateTime for the Now property</param>
		/// <param name="dtUtc">The fixed DateTime for the UtcNow property, leave null to use dtNow value</param>
		public static void SetMockDateTime(DateTime? dtNow, DateTime? dtUtc = null)
		{
			mockDateTimeNow.Value = dtNow;
			mockDateTimeUtcNow.Value = dtUtc is null ? dtNow : dtUtc;
		}

		/// <summary>
		/// Sets how UTC is handled by the <see cref="Now"/> property, if true then <see cref="Now"/> is returning UTC time
		/// </summary>
		/// <param name="NowIsUtc">If true, <see cref="Now"/> is UTC time</param>
		public void SetUtcHandling(bool NowIsUtc)
		{
			bNowIsUtc.Value = NowIsUtc;
		}

		#endregion Public Methods
	}
}
