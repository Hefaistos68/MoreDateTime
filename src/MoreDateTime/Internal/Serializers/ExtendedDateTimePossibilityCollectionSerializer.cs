using System.Text;

namespace MoreDateTime.Internal.Serializers
{
    /// <summary>
    /// The extended date time possibility collection serializer.
    /// </summary>
    internal static class ExtendedDateTimePossibilityCollectionSerializer
    {
        /// <summary>
        /// Serializes the.
        /// </summary>
        /// <param name="extendedDateTimePossibilityCollection">The extended date time possibility collection.</param>
        /// <returns>A string.</returns>
        internal static string Serialize(ExtendedDateTimePossibilityCollection extendedDateTimePossibilityCollection)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append('[');

            for (int i = 0; i < extendedDateTimePossibilityCollection.Count; i++)
            {
                stringBuilder.Append(extendedDateTimePossibilityCollection[i].ToString());

                if (i != extendedDateTimePossibilityCollection.Count - 1)                              // Don't put comma after last element.
                {
                    stringBuilder.Append(',');
                }
            }

            stringBuilder.Append(']');

            return stringBuilder.ToString();
        }
    }
}