namespace MoreDateTime
{
	/// <summary>
	/// The date time value class, which can be a string or an integer, with flags.
	/// </summary>
	public class DateTimeValue
	{
		/// <summary>
		/// The value flags, specifying whether the day is approximate, uncertain, unspecified, unknown, or exact.
		/// it also can be a mix of most flags, and be Long to indicate its a value above the normal range (used for years).
		/// </summary>
		[Flags]
		public enum ValueFlags
		{
			/// <summary>Value was not define at all</summary>
			None,
			/// <summary>Value is uncertain (?)</summary>
			Uncertain   = 0b000001,
			/// <summary>Value is approximate (~)</summary>
			Approximate = 0b000010,
			/// <summary>Value is unknown</summary>
			Unknown     = 0b000100,
			/// <summary>Value is unspecified</summary>
			Unspecified = 0b001000,
			/// <summary>Value is exact</summary>
			Exact       = 0b010000,
			/// <summary>Value is a year value above 9999 or below -9999 (or specified with E)</summary>
			Long        = 0b100000,
		}

		object? dtValue = null;
		ValueFlags dtFlags = ValueFlags.None;
		int dtSignificantDigits = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeValue"/> class.
		/// </summary>
		public DateTimeValue()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeValue"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="flags">The flags.</param>
		public DateTimeValue(int value, ValueFlags flags = ValueFlags.Exact)
		{
			dtValue = value;
			dtFlags = flags;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeValue"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="flags">The flags.</param>
		public DateTimeValue(string value, ValueFlags flags = ValueFlags.Exact)
		{
			dtValue = value;
			dtFlags = flags;
		}

		/// <summary>
		/// implicit conversion from integer to DateTimeValue
		/// </summary>
		/// <param name="value"></param>
		public static implicit operator DateTimeValue(int value)
		{
			return new DateTimeValue(value, ValueFlags.Exact);
		}

		/// <summary>
		/// explicit conversion from DateTimeValue to integer
		/// </summary>
		/// <param name="dtv"></param>
		public static implicit operator int(DateTimeValue dtv)
		{
			if (dtv.dtValue is int)
				return (int)dtv.dtValue;

			if (dtv.dtValue is string)
			{
				return int.TryParse((string)dtv.dtValue, out int result) ? result : int.MinValue;
			}

			return 0;
		}

		/// <summary>
		/// Implicit conversion from DateTimeValue to string
		/// </summary>
		/// <param name="dtv"></param>
		public static implicit operator string(DateTimeValue dtv)
		{
			if (dtv.dtValue is int)
				return ((int)dtv.dtValue).ToString();

			if (dtv.dtValue is string)
			{
				return (string)dtv.dtValue;
			}

			return string.Empty;
		}

		/// <summary>
		/// Explicit conversion from string to DateTimeValue
		/// </summary>
		/// <param name="value"></param>
		public static explicit operator DateTimeValue(string value) => new DateTimeValue(value);

		/// <summary>
		/// Gets or sets a value indicating whether is exact.
		/// </summary>
		public bool IsNone
		{
			get { return dtFlags == ValueFlags.None; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether is exact.
		/// </summary>
		public bool IsExact
		{
			get { return dtFlags.HasFlag(ValueFlags.Exact); }
			set { SetFlag(ValueFlags.Exact, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether is exact.
		/// </summary>
		public bool IsLong
		{
			get { return dtFlags.HasFlag(ValueFlags.Long); }
			set { SetFlag(ValueFlags.Long, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether is approximate.
		/// </summary>
		public bool IsApproximate
		{
			get { return dtFlags.HasFlag(ValueFlags.Approximate); }
			set { SetFlag(ValueFlags.Approximate, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether is uncertain.
		/// </summary>
		public bool IsUncertain
		{
			get { return dtFlags.HasFlag(ValueFlags.Uncertain); }
			set { SetFlag(ValueFlags.Uncertain, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether is unspecified.
		/// </summary>
		public bool IsUnspecified
		{
			get { return dtFlags.HasFlag(ValueFlags.Unspecified); }
			set { SetFlag(ValueFlags.Unspecified, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether is unknown.
		/// </summary>
		public bool IsUnknown
		{
			get { return dtFlags == ValueFlags.Unknown; }
			set { SetFlag(ValueFlags.Unknown, value); }
		}

		/// <summary>
		/// Gets a value indicating whether the year is between -9999 and 9999.
		/// </summary>
		public bool IsRegularYear
		{
			get { return dtValue is int ? ((int)dtValue <= 9999 && (int)dtValue >= -9999) : false; }
		}

		/// <summary>
		/// Sets bits in a flag.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="setFlag">If true, set flag.</param>
		/// <returns>A ValueFlags.</returns>
		private ValueFlags SetFlag(ValueFlags value, bool setFlag)
		{
			return setFlag ? (dtFlags | value) : (dtFlags & ~value);
		}
		/// <summary>
		/// Gets or sets the significant digits.
		/// </summary>
		public int SignificantDigits { get => this.dtSignificantDigits; set => this.dtSignificantDigits = value; }

		/// <summary>
		/// Gets or sets the insignificant digits, the number of digits in the number minus the significant digits.
		/// </summary>
		public int InsignificantDigits { get => this.ToString().Length - this.dtSignificantDigits; set => this.dtSignificantDigits = this.ToString().Length - this.dtSignificantDigits; }


		/// <summary>
		/// Gets the flags associated with Qualifiers
		/// </summary>
		public ValueFlags QualifierFlags
		{
			get { return dtFlags & (ValueFlags.Uncertain | ValueFlags.Unknown | ValueFlags.Unspecified | ValueFlags.Approximate); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the qualifier should be prefixed (or suffixed if false)
		/// </summary>
		public bool PrefixQualifier { get; set; }


		/// <summary>
		/// An object representing an empty value.
		/// </summary>
		public static DateTimeValue Empty = new DateTimeValue { dtFlags = ValueFlags.None };

		/// <inheritdoc/>
		public override string ToString()
		{
			if (this.dtValue is int)
				return ((int)this.dtValue).ToString();

			if (this.dtValue is string)
			{
				return (string)this.dtValue;
			}

			return string.Empty;
		}

		/// <summary>
		/// Formats the integer value as a string with leading zeros according to the significant digits.
		/// </summary>
		/// <returns>A string.</returns>
		public string ToNumberString()
		{
			if (this.dtValue is not int)
				throw new InvalidOperationException();

			int v = (int)this.dtValue;

			return v.ToString("D" + (this.SignificantDigits == 0 ? "2" : this.SignificantDigits.ToString()));
		}

		/// <summary>
		/// Sets the flags for approximate, uncertain, unspecified, and exact.
		/// </summary>
		/// <param name="flags">The flags.</param>
		internal void SetFlags(ValueFlags flags)
		{
			dtFlags = flags;
		}

		/// <summary>
		/// Sets the flags for by adding (OR) to existing flags.
		/// </summary>
		/// <param name="flags">The flags.</param>
		internal void AddFlags(ValueFlags flags)
		{
			dtFlags |= flags;
		}
	}

	/// <summary>
	/// The date time value unit test extensions for Shouldly
	/// </summary>
	public static class DateTimeValueTestExtensions
	{
		/// <summary>
		/// compares the value to the expected value
		/// </summary>
		/// <param name="dtv">The dtv.</param>
		/// <param name="value">The value.</param>
		/// <returns>A bool.</returns>
		public static bool ShouldBe(this DateTimeValue dtv, int value)
		{
			return value == (int)dtv;
		}

		/// <summary>
		/// compares the value to the expected value
		/// </summary>
		/// <param name="dtv">The dtv.</param>
		/// <param name="value">The value.</param>
		/// <returns>A bool.</returns>
		public static bool ShouldBe(this DateTimeValue dtv, Season value)
		{
			return (int)value == (int)dtv;
		}

		/// <summary>
		/// compares the value to the expected value
		/// </summary>
		/// <param name="dtv">The dtv.</param>
		/// <param name="value">The value.</param>
		/// <returns>A bool.</returns>
		public static bool ShouldBe(this DateTimeValue dtv, string value)
		{
			return value == dtv.ToString();
		}
	}
}