namespace MoreDateTime.Internal
{
    /// <summary>
    /// The constants for character comparisons in EDT formatted strings
    /// </summary>
    public static class Constants
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>All explicit unit identifiers: YAMDTHSW</summary>
        internal static readonly char[] ExplicitIdentifierChars = new char[] { 'Y', 'A', 'M', 'D', 'T', 'H', 'S', 'W' };

        /// <summary>All date part identifiers: YAMKD</summary>
        internal static readonly char[] DatePartIdentifierChars = new char[] { 'Y', 'A', 'M', 'K', 'D' };

        /// <summary>All date part identifiers: 0-9, YAMDES</summary>
        internal static readonly char[] RegularDateChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'Y', 'A', 'M', 'D', 'E', 'S' };

        /// <summary>All scope opening identifiers: [ and {</summary>
        internal static readonly char[] ScopeOpeningChars = new char[] { '[', '{' };

        /// <summary>All scope closing identifiers: ] and }</summary>
        internal static readonly char[] ScopeClosingChars = new char[] { '}', ']' };

        /// <summary>All unit qualifier identifiers: ? (Uncertain) ~ (approximate) % (both)</summary>
        internal static readonly char[] QualifierChars = new char[] { '?', '~', '%' };
#pragma warning restore IDE1006 // Naming Styles
    }
}
