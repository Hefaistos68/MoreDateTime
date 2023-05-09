using System.Text;

using MoreDateTime.Exceptions;
using MoreDateTime.Interfaces;

namespace MoreDateTime.Internal.Parsers
{
    /// <summary>
    /// The extended date time collection parser.
    /// </summary>
    internal static class ExtendedDateTimeCollectionParser
    {
        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="extendedDateTimeCollectionString">The extended date time collection string.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>An ExtendedDateTimeCollection.</returns>
        internal static ExtendedDateTimeCollection Parse(string extendedDateTimeCollectionString, ExtendedDateTimeCollection? collection = null)
        {
            if (string.IsNullOrWhiteSpace(extendedDateTimeCollectionString))
            {
                throw new ParseException("A collection string must not be empty.", "");
            }

            var hasStartBrace = extendedDateTimeCollectionString.First() == '{';
            var hasEndBrace = extendedDateTimeCollectionString.Last() == '}';

            if (!hasStartBrace || !hasEndBrace)
            {
                if (!extendedDateTimeCollectionString.Contains(','))
                {
                    throw new ParseException("A collection string must be surrounded by curly braces, or contain a coma.", extendedDateTimeCollectionString);
                }
            }

            var contentsString = extendedDateTimeCollectionString.Substring(hasStartBrace ? 1 : 0, extendedDateTimeCollectionString.Length - (hasEndBrace ? 2 : 0));
            var closingChar = (char?)null;
            var setRanges = new Dictionary<int, int>();             // A dictionary of indexes where sets begin and end within the contents string.
            var setStartingIndex = (int?)null;

            if (contentsString.ContainsAny(Constants.ScopeOpeningChars))
            {
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
            }

            var currentSetRangeIndex = 0;
            StringBuilder remainingChars = new();

            if (collection == null)
            {
                collection = new ExtendedDateTimeCollection();

                if (hasStartBrace && hasEndBrace)
                {
                    collection.IsBracketedSet = true;      // to later distinguish between "[1500..1600]" and "1500..1600"
                }
            }

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
                                collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimeRangeParser.Parse(preceedingElementString));
                            }
                            else
                            {
                                collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimeParser.Parse(preceedingElementString));
                            }
                        }

                        remainingChars.Clear();

                        var setString = contentsString.Substring(i, setRanges[i] - i);      // Add nested set.

                        if (setString[0] == '{')
                        {
                            collection.Add((IExtendedDateTimeCollectionChild)Parse(setString));
                        }
                        else
                        {
                            collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimePossibilityCollectionParser.Parse(setString));
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
                remainingChars.Append(contentsString);
            }

            var remainingElementsString = remainingChars.ToString();        // Add all elements that remain.
            var remainingElementStrings = remainingElementsString.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var remainingElementString in remainingElementStrings)
            {
                if (remainingElementString.Contains(".."))
                {
                    collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimeRangeParser.Parse(remainingElementString));
                }
                else
                {
                    collection.Add((IExtendedDateTimeCollectionChild)ExtendedDateTimeParser.Parse(remainingElementString));
                }
            }

            return collection;
        }
    }
}