using System.Text;

using MoreDateTime.Exceptions;
using MoreDateTime.Interfaces;

namespace MoreDateTime.Internal.Parsers
{
    /// <summary>
    /// The extended date time possibility collection parser.
    /// </summary>
    internal static class ExtendedDateTimePossibilityCollectionParser
    {
        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="extendedDateTimePossibilityCollectionString">The extended date time possibility collection string.</param>
        /// <param name="possibilityCollection">The possibility collection.</param>
        /// <param name="isOpenInterval"></param>
        /// <returns>An ExtendedDateTimePossibilityCollection.</returns>
        internal static ExtendedDateTimePossibilityCollection Parse(string extendedDateTimePossibilityCollectionString, ExtendedDateTimePossibilityCollection? possibilityCollection = null, bool isOpenInterval = false)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimePossibilityCollectionString))
            {
                throw new ParseException("A possibility collection string must not be empty.", extendedDateTimePossibilityCollectionString);
            }

            if (!(extendedDateTimePossibilityCollectionString.StartsWith('[') && extendedDateTimePossibilityCollectionString.EndsWith(']')))
            {
                throw new ParseException("A possibility collection string must be surrounded by square brackets.", extendedDateTimePossibilityCollectionString);
            }

            var contentsString = extendedDateTimePossibilityCollectionString[1..^1];
            var closingChar = (char?)null;
            var setRanges = new Dictionary<int, int>();             // A dictionary of indexes where sets begin and end within the contents string.
            var setStartingIndex = (int?)null;

            for (int i = 0; i < contentsString.Length; i++)         // Locate nested sets.
            {
                if (contentsString[i] == '{' && setStartingIndex == null)
                {
                    closingChar = '}';
                    setStartingIndex = i;
                }
                else if (contentsString[i] == '[' && setStartingIndex == null)
                {
                    closingChar = ']';
                    setStartingIndex = i;
                }
                else if (contentsString[i] == closingChar && setStartingIndex != null)
                {
                    setRanges.Add(setStartingIndex.Value, i);
                    setStartingIndex = null;
                }
            }

            var currentSetRangeIndex = 0;
            StringBuilder remainingChars = new();

            possibilityCollection ??= new ExtendedDateTimePossibilityCollection();

            if (setRanges.Count > 0)
            {
                for (int i = 0; i < contentsString.Length; i++)                                     // Add set contents, including nested sets.
                {
                    if (setRanges.Count > currentSetRangeIndex && i == setRanges.Keys.ElementAt(currentSetRangeIndex))
                    {
                        var preceedingElementsString = remainingChars.ToString();        // Add elements preceeding the nested set.
                        var preceedingElementStrings = preceedingElementsString.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var preceedingElementString in preceedingElementStrings)
                        {
                            if (preceedingElementString.Contains(".."))
                            {
                                possibilityCollection.Add(ExtendedDateTimeRangeParser.Parse(preceedingElementString));
                            }
                            else
                            {
                                possibilityCollection.Add(ExtendedDateTimeParser.Parse(preceedingElementString));
                            }
                        }

                        remainingChars.Clear();

                        var setString = contentsString.Substring(i, setRanges[i] - i);      // Add nested set.

                        if (setString[0] == '{')
                        {
                            possibilityCollection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimeCollectionParser.Parse(setString));
                        }
                        else
                        {
                            possibilityCollection.Add(Parse(setString));
                        }

                        i = setRanges[i];
                        currentSetRangeIndex++;
                    }
                    else
                    {
                        remainingChars.Append(contentsString[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < contentsString.Length; i++)                                     // Add set contents, including nested sets.
                {
                    remainingChars.Append(contentsString[i]);
                }
            }

            var remainingElementsString = remainingChars.ToString();        // Add all elements that remain.
            var remainingElementStrings = remainingElementsString.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var remainingElementString in remainingElementStrings)
            {
                if (remainingElementString.Contains(".."))
                {
                    possibilityCollection.Add(ExtendedDateTimeRangeParser.Parse(remainingElementString));
                }
                else
                {
                    possibilityCollection.Add(ExtendedDateTimeParser.Parse(remainingElementString));
                }
            }

            return possibilityCollection;
        }
    }
}