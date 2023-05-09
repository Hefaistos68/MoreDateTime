namespace MoreDateTime.Internal
{
    /// <summary>
    /// The enumerable extensions.
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Contains the before.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="x">The before.</param>
        /// <param name="y">The after.</param>
        /// <returns>A bool.</returns>
        internal static bool ContainsBefore<TSource>(this IEnumerable<TSource> source, TSource x, TSource y)
        {
            return ContainsBefore(source, x, y, EqualityComparer<TSource>.Default);
        }

        /// <summary>
        /// Verifies the IEnumerable if the element <see param="before"/> is contained before the element <see param="after"/>.
        /// If before is not contained at all, false is returned. The element <see param="after"/> may or may not be contained.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="before">The before.</param>
        /// <param name="after">The after.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>A bool.</returns>
        internal static bool ContainsBefore<TSource>(IEnumerable<TSource> source, TSource before, TSource after, IEqualityComparer<TSource> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var element in source)
            {
                if (comparer.Equals(element, before))
                {
                    return true;
                }

                if (comparer.Equals(element, after))
                {
                    return false;
                }
            }

            return false;
        }
    }
}
