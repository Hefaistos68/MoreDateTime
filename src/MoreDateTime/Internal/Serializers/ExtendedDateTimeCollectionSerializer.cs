using System.Text;

namespace MoreDateTime.Internal.Serializers
{
    /// <summary>
    /// The extended date time collection serializer.
    /// </summary>
    internal static class ExtendedDateTimeCollectionSerializer
    {
        /// <summary>
        /// Serializes the.
        /// </summary>
        /// <param name="extendedDateTimeCollection">The extended date time collection.</param>
        /// <returns>A string.</returns>
        internal static string Serialize(ExtendedDateTimeCollection extendedDateTimeCollection)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendIfTrue(extendedDateTimeCollection.IsBracketedSet, '{');

            for (int i = 0; i < extendedDateTimeCollection.Count; i++)
            {
                stringBuilder.Append(extendedDateTimeCollection[i].ToString());

                if (i != extendedDateTimeCollection.Count - 1)                              // Don't put comma after last element.
                {
                    stringBuilder.Append(',');
                }
            }

            stringBuilder.AppendIfTrue(extendedDateTimeCollection.IsBracketedSet, '}');

            return stringBuilder.ToString();
        }
    }
}