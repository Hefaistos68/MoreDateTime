using System.Text;

namespace MoreDateTime.Internal
{
    /// <summary>
    /// The string builder extensions.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends the string if it is not null or empty to the StringBuilder
        /// </summary>
        /// <param name="me">The StringBuilder</param>
        /// <param name="value">The values</param>
        public static StringBuilder AppendIfNotNullOrEmpty(this StringBuilder me, string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                me.Append(value);
            }
            return me;
        }

        /// <summary>
        /// Appends the string if it is not null or empty to the StringBuilder
        /// </summary>
        /// <param name="me">The StringBuilder</param>
        /// <param name="bValue"></param>
        /// <param name="value">The values</param>
        public static StringBuilder AppendIfTrue(this StringBuilder me, bool bValue, params string?[] value)
        {
            if (bValue)
            {
                foreach (string? one in value)
                {
                    me.AppendIfNotNullOrEmpty(one);
                }
            }

            return me;
        }

        /// <summary>
        /// Appends the string if it is not null or empty to the StringBuilder
        /// </summary>
        /// <param name="me">The StringBuilder</param>
        /// <param name="bValue"></param>
        /// <param name="values">The values</param>
        public static StringBuilder AppendIfTrue(this StringBuilder me, bool bValue, params char[] values)
        {
            if (bValue)
            {
                foreach (char one in values)
                {
                    me.Append(one);
                }
            }

            return me;
        }

    }

    /// <summary>
    /// The string extensions.
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// Shrinks the given string by the number of characters on both sides
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="charsToShrink">The chars to shrink.</param>
        /// <returns>A string.</returns>
        public static string Shrink(this string value, int charsToShrink)
        {
            if (value.Length <= charsToShrink * 2)
            {
                return value;
            }

            return value.Substring(charsToShrink, value.Length - charsToShrink * 2);
        }

        /// <summary>
        /// Shrinks the given string by the number of characters specified for start and end
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="charsToShrinkFront">The chars to shrink at the beginning</param>
        /// <param name="charsToShrinkEnd">The chars to shrink at the end</param>
        /// <returns>A string.</returns>
        public static string Shrink(this string value, int charsToShrinkFront, int charsToShrinkEnd)
        {
            if (value.Length <= charsToShrinkFront + charsToShrinkEnd)
            {
                return value;
            }

            return value.Substring(charsToShrinkFront, value.Length - (charsToShrinkFront + charsToShrinkEnd));
        }

        /// <summary>
        /// Verifies if the string contains any of the values.
        /// </summary>
        /// <param name="value">The values to verify</param>
        /// <param name="values">The values to be contained</param>
        /// <returns>True if <see paramref="values"/> contains one of the values</returns>
        public static bool ContainsAny(this string value, params string[] values)
        {
            foreach (string one in values)
            {
                if (value.Contains(one))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Verifies if the string contains any of the values.
        /// </summary>
        /// <param name="value">The values to verify</param>
        /// <param name="values">The values to be contained</param>
        /// <returns>True if <see paramref="values"/> contains one of the values</returns>
        public static bool ContainsAny(this string value, params char[] values)
        {
            foreach (char one in values)
            {
                if (value.Contains(one))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Verifies if the char is any of the values.
        /// </summary>
        /// <param name="value">The values.</param>
        /// <param name="values">The values.</param>
        /// <returns>A bool.</returns>
        public static bool IsAnyOf(this char value, params char[] values)
        {
            foreach (char one in values)
            {
                if (value == one)
                {
                    return true;
                }
            }

            return false;
        }
    }
}