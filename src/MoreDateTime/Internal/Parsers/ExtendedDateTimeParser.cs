using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using MoreDateTime;
using MoreDateTime.Exceptions;

[assembly: InternalsVisibleTo("ExtendedDateTimeFormat.Tests")]

namespace MoreDateTime.Internal.Parsers
{
    /// <summary>
    /// The extended date time parser.
    /// </summary>
    internal static class ExtendedDateTimeParser
    {
        /// <summary>
        /// Parses the.
        /// </summary>
        /// <param name="extendedDateTimeString">The extended date time string.</param>
        /// <param name="extendedDateTime">The extended date time.</param>
        /// <returns>An ExtendedDateTime.</returns>
        internal static ExtendedDateTime Parse(string extendedDateTimeString, ExtendedDateTime? extendedDateTime = null)
        {
            // dont accept null strings
            if (extendedDateTimeString is null)
            {
                throw new ArgumentNullException(nameof(extendedDateTimeString));
            }

            extendedDateTime ??= new ExtendedDateTime();

            // emtpy string, means someone passed in "" as in "/someyear"
            if (string.IsNullOrEmpty(extendedDateTimeString))
            {
                extendedDateTime.IsUnknown = true;
                return extendedDateTime;
            }

            // ".." means "on or after" or "on or before" depending on position in ExtendedDateTimeFormat string
            if (".." == extendedDateTimeString)
            {
                extendedDateTime.IsOpen = true;
                return extendedDateTime;
            }

            if (extendedDateTimeString.StartsWith(".."))
            {
                extendedDateTime.IsOpenLeft = true;
                extendedDateTimeString = extendedDateTimeString.Substring(2);
            }

            if (extendedDateTimeString.EndsWith(".."))
            {
                extendedDateTime.IsOpenRight = true;
                extendedDateTimeString = extendedDateTimeString.Substring(0, extendedDateTimeString.Length - 2);
            }

            StringBuilder componentBuffer = new();

            var isDatePart = true;
            var isTimeZonePart = false;
            var isSeasonQualifierPart = false;

            var currentDateComponent = 0;
            var currentTimeComponent = 0;

            // lets figure out the simplest case first, if the string is hyphen separated
            if (extendedDateTimeString.Contains('-'))
            {
                // if the string starts with a hyphen, its a negative date, 
                if (extendedDateTimeString[0] != '-')
                {
                    if (extendedDateTimeString[0] == 'Y' && extendedDateTimeString[1] == '-')   // explicit year
                    {

                    }
                    else
                    {
                        return ParseSimple(extendedDateTimeString, ref extendedDateTime);
                    }
                }
            }

            var hasSeasonComponent = false;
            DateTimeValue.ValueFlags prefixflags = DateTimeValue.ValueFlags.None;       // those apply only to the current unit (year, months, etc)
            DateTimeValue.ValueFlags suffixflags = DateTimeValue.ValueFlags.None;       // those apply to ALL units up to the current and have to be applied posterior

            for (int i = 0; i < extendedDateTimeString.Length; i++)
            {
                var character = extendedDateTimeString[i];

                if (isDatePart)                                                                         // Parsing date portion of extended date time.
                {
                    ParseDatePartChar(extendedDateTimeString, ref extendedDateTime, componentBuffer, ref isDatePart, isSeasonQualifierPart, ref currentDateComponent, ref hasSeasonComponent, ref i, character);
                }
                else                                                                                    // Parsing time portion of extended date time.
                {
                    ParseTimePartChar(ref extendedDateTime, componentBuffer, ref isTimeZonePart, ref currentTimeComponent, character);
                }
            }

            // if there is anything left in the buffer, its another component
            if (componentBuffer.Length > 0)
            {
                if (isDatePart)
                {
                    CommitDateComponent(ref currentDateComponent, ref hasSeasonComponent, prefixflags, suffixflags, componentBuffer, ref extendedDateTime!);
                }
                else
                {
                    CommitTimeComponent(ref currentTimeComponent, isTimeZonePart, componentBuffer, ref extendedDateTime!);
                }
            }

            if (extendedDateTime is null)
            {
                throw new ParseException("Could not parse ExtendedDateTimeFormat string", extendedDateTimeString);
            }

            return extendedDateTime;

            // parse the supposed date part of the string until the next breaking character
            void ParseDatePartChar(string extendedDateTimeString, ref ExtendedDateTime? extendedDateTime, StringBuilder componentBuffer, ref bool isDatePart, bool isSeasonQualifierPart, ref int currentDateComponent, ref bool hasSeasonComponent, ref int i, char character)
            {
                if (character.IsAnyOf(Constants.RegularDateChars))                                              // if its any of the regular suffix date chars
                {
                    componentBuffer.Append(character);
                }
                else if (character == '-')
                {
                    if (i == 0 || i > 0 && extendedDateTimeString[i - 1] == 'Y')                      // Hyphen is a negative sign.
                    {
                        componentBuffer.Append(character);
                    }
                    else                                                                                    // Hyphen is a component separator.
                    {
                        CommitDateComponent(ref currentDateComponent, ref hasSeasonComponent, prefixflags, suffixflags, componentBuffer, ref extendedDateTime!);
                    }
                }
                else if (character == 'T')
                {
                    CommitDateComponent(ref currentDateComponent, ref hasSeasonComponent, prefixflags, suffixflags, componentBuffer, ref extendedDateTime!);

                    isDatePart = false;
                }
                else if (isSeasonQualifierPart)
                {
                    ThrowParseExceptionIfTrue(char.IsWhiteSpace(character), "Season qualifiers cannot contain whitespace.", componentBuffer.ToString());

                    componentBuffer.Append(character);
                }
                else if (character.IsAnyOf(Constants.QualifierChars))
                {
                    // here we decide if its prefix or suffix
                    if (componentBuffer.Length == 0)                                                    // prefix
                    {
                        prefixflags = GetFlag(character);
                        CommitDateComponent(ref currentDateComponent, ref hasSeasonComponent, prefixflags, suffixflags, componentBuffer, ref extendedDateTime!);
                    }
                    else                                                                            // suffix
                    {
                        suffixflags = GetFlag(character);
                        CommitDateComponent(ref currentDateComponent, ref hasSeasonComponent, suffixflags, suffixflags, componentBuffer, ref extendedDateTime!);
                    }
                    Debugger.Break();
                }
                else
                {
                    throw new ParseException("The character \'" + character + "\' could not be recognized.", componentBuffer.ToString());
                }
            }

        }

        /// <summary>
        /// Parses a character supposed to be part of a time component
        /// </summary>
        /// <param name="extendedDateTime">The ExtendedDateTime object receiving the value</param>
        /// <param name="componentBuffer">The string value</param>
        /// <param name="isTimeZonePart">If true, the character is supposed to be part of the timezone specification</param>
        /// <param name="currentTimeComponent">The index of the time component (0=hour, 1=minute, etc)</param>
        /// <param name="character">The character to process</param>
        /// <exception cref="ParseException">Thrown if the character is not expected</exception>
        static void ParseTimePartChar(ref ExtendedDateTime? extendedDateTime, StringBuilder componentBuffer, ref bool isTimeZonePart, ref int currentTimeComponent, char character)
        {
            if (char.IsDigit(character) || character == ':' && isTimeZonePart)                                       // Add digit to component buffer.
            {
                componentBuffer.Append(character);
            }
            else if (character == ':' && !isTimeZonePart)
            {
                CommitTimeComponent(ref currentTimeComponent, isTimeZonePart, componentBuffer, ref extendedDateTime!);
            }
            else if (character == 'Z' || character == '+' || character == '-')                                         // Time zone component
            {
                CommitTimeComponent(ref currentTimeComponent, isTimeZonePart, componentBuffer, ref extendedDateTime!);

                componentBuffer.Append(character);

                isTimeZonePart = true;
            }
            else
            {
                throw new ParseException("The character \'" + character + "\' could not be recognized.", componentBuffer.ToString());
            }
        }


        /// <summary>
        /// Parses the simple.
        /// </summary>
        /// <param name="extendedDateTimeString">The extended date time string.</param>
        /// <param name="edt"></param>
        /// <returns>An ExtendedDateTime.</returns>
        private static ExtendedDateTime ParseSimple(string extendedDateTimeString, ref ExtendedDateTime edt)
        {
            var components = extendedDateTimeString.Split('T'); // split date and time
            var parts = components.First().Split('-');
            List<DateTimeValue> values = new List<DateTimeValue>();

            foreach (var part in parts)
            {
                // if the part starts with any of the qualifiers, it applies to this part only
                if (part[0].IsAnyOf(Constants.QualifierChars))
                {
                    var qualifier = part[0];
                    values.Add(new DateTimeValue(int.Parse(part[1..]), GetFlag(qualifier)));
                    values.Last().SignificantDigits = part.Length - 1;
                    values.Last().PrefixQualifier = true;
                }
                else
                {
                    char lastChar = part[^1];
                    if (lastChar.IsAnyOf(Constants.QualifierChars))
                    {
                        var flags = GetFlag(lastChar);

                        // apply prefixFlag to all other values
                        foreach (var v in values)
                        {
                            v.AddFlags(flags);
                        }

                        values.Add(new DateTimeValue(int.Parse(part[..^1]), flags));
                        values.Last().SignificantDigits = part.Length - 1;
                    }
                    else
                    {
                        values.Add(new DateTimeValue(int.Parse(part), DateTimeValue.ValueFlags.Exact));
                        values.Last().SignificantDigits = part.Length;
                    }
                }
            }

            edt.Year = values[0];
            edt.Precision = ExtendedDateTimePrecision.Year;
            ValidateYear(edt.Year, 4);

            if (values.Count > 1)
            {
                int month = (int)values[1];

                ValidateMonth(month);

                if (IsSeason(month))
                {
                    edt.Season = values[1];
                    edt.Precision = ExtendedDateTimePrecision.Season;
                }
                else
                {
                    edt.Month = values[1];
                    edt.Precision = ExtendedDateTimePrecision.Month;
                }
            }

            if (values.Count > 2)
            {
                edt.Day = values[2];
                edt.Precision = ExtendedDateTimePrecision.Day;
                ValidateDay(edt.Day, edt.Month, edt.Year);
            }

            if (components.Length > 1)
            {
                ParseTimeComponent(components.Last(), ref edt);
            }

            return edt;
        }

        /// <summary>
        /// Verifies if the given value could be a month value
        /// </summary>
        /// <param name="m">The integer value</param>
        /// <returns>A bool.</returns>
        private static bool IsMonth(int m)
        {
            return m is >= 1 and <= 12;
        }

        /// <summary>
        /// Verifies if the given value could be a season value
        /// </summary>
        /// <param name="s">The integer value</param>
        /// <returns>A bool.</returns>
        private static bool IsSeason(int s)
        {
            return s is >= (int)Season.First and <= (int)Season.Last;
        }

        /// <summary>
        /// Validates a year. Values can be -9999 to +9999 when expectedLength is 4, or any other if expectedLength is != 4.
        /// </summary>
        /// <param name="year">The year value</param>
        /// <param name="expectedLength">The expected length in characters of the year value</param>
        private static void ValidateYear(DateTimeValue year, int expectedLength)
        {
            ThrowParseExceptionIfTrue(year.SignificantDigits != expectedLength, "The year component is not the expected length.", year.ToString());

            if (expectedLength == 4)
            {
                ThrowInvalidDateExceptionIfTrue(year < -9999 || year > 9999, "Year must be in the range -9999 to 9999");
            }
        }

        /// <summary>
        /// Validates the month or season. Values can be 1-12 for months or Season.First to Season.Last for seasons.
        /// </summary>
        /// <param name="monthOrSeason">The month or season value</param>
        private static void ValidateMonth(int monthOrSeason)
        {
            if (monthOrSeason is >= ((int)Season.First) and <= ((int)Season.Last))
            {
                return;
            }

            ThrowInvalidDateExceptionIfTrue(monthOrSeason < 1 || monthOrSeason > 12, "Month must be in the range 1 to 12");
        }

        /// <summary>
        /// Validates the day.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        private static void ValidateDay(DateTimeValue day, DateTimeValue month, DateTimeValue year)
        {
            ThrowInvalidDateExceptionIfTrue(day < 1 || day > 31, "Day must be in the range 1 to 31");

            int daysInMonth;

            if (year.IsExact && month.IsExact)                                                      // get days per month exact
            {
                daysInMonth = ExtendedDateTimeCalculator.DaysInMonth(year, month);
            }
            else if (month.IsExact)                                                                 // we have only the month, use "usual" values
            {
                daysInMonth = ExtendedDateTimeCalculator.DaysInMonth(month);
            }
            else                                                                                    // year and month unknown, has fallen in the first throw
            {
                return;
            }

            ThrowInvalidDateExceptionIfTrue(day > daysInMonth, "The month in the year has less days than indicated.");
        }

        /// <summary>
        /// Parses the time component.
        /// </summary>
        /// <param name="extendedDateTimeString">The EDT fomratted string.</param>
        /// <param name="extendedDateTime">The ExtendedDateTime object receiving the values.</param>
        private static void ParseTimeComponent(string extendedDateTimeString, ref ExtendedDateTime extendedDateTime)
        {
            bool isTimeZonePart = false;
            int currentTimeComponent = 0;
            StringBuilder componentBuffer = new StringBuilder();

            for (int i = 0; i < extendedDateTimeString.Length; i++)
            {
                var character = extendedDateTimeString[i];

                ParseTimePartChar(ref extendedDateTime!, componentBuffer, ref isTimeZonePart, ref currentTimeComponent, character);
            }

            // if there is anything left in the buffer, its another component
            if (componentBuffer.Length > 0)
            {
                CommitTimeComponent(ref currentTimeComponent, isTimeZonePart, componentBuffer, ref extendedDateTime!);
            }
        }

        /// <summary>
        /// Commits the date component.
        /// </summary>
        /// <param name="dateComponentIndex">The date component index.</param>
        /// <param name="hasSeasonComponent">If true, has season component.</param>
        /// <param name="prefixFlag">The prefixFlag.</param>
        /// <param name="suffixFlag"></param>
        /// <param name="componentBuffer">The component buffer.</param>
        /// <param name="extendedDateTime">The extended date time.</param>
        private static CommitResult CommitDateComponent(ref int dateComponentIndex, ref bool hasSeasonComponent, DateTimeValue.ValueFlags prefixFlag, DateTimeValue.ValueFlags suffixFlag, StringBuilder componentBuffer, ref ExtendedDateTime extendedDateTime)
        {
            if (componentBuffer.Length == 0)
            {
                return CommitResult.Success;
            }

            var componentString = componentBuffer.ToString();

            if (dateComponentIndex == 0)                                                           // We expect a year to appear first.
            {
                CommitYear(prefixFlag, extendedDateTime, componentString);

                dateComponentIndex++;
            }
            else if (dateComponentIndex == 1)                                                 // We expect either a monthOrSeason or a season to appear second.
            {
                hasSeasonComponent = CommitMonthOrSeason(hasSeasonComponent, prefixFlag, extendedDateTime, componentString);

                // apply flags to preceeding units
                if (suffixFlag != DateTimeValue.ValueFlags.None && !extendedDateTime.Year.IsNone)
                {
                    extendedDateTime.Year.SetFlags(suffixFlag);
                }

                dateComponentIndex++;
            }
            else if (dateComponentIndex == 2)                                                   // We expect a day.
            {
                CommitDay(hasSeasonComponent, prefixFlag, extendedDateTime, componentString);

                // apply flags to preceeding units
                if (suffixFlag != DateTimeValue.ValueFlags.None)
                {
                    if (!extendedDateTime.Year.IsNone)
                    {
                        extendedDateTime.Year.SetFlags(suffixFlag);
                    }
                    if (!extendedDateTime.Month.IsNone)
                    {
                        extendedDateTime.Month.SetFlags(suffixFlag);
                    }
                    if (!extendedDateTime.Season.IsNone)
                    {
                        extendedDateTime.Season.SetFlags(suffixFlag);
                    }
                }

                dateComponentIndex++;
            }
            else if (dateComponentIndex > 2)
            {
                throw new ParseException("The date can have at most three components.", componentString);
            }

            componentBuffer.Clear();

            return CommitResult.Success;

            //////////////////////////////////////////////////////////////////////////
            //
            //
            void CommitYear(DateTimeValue.ValueFlags flags, ExtendedDateTime extendedDateTime, string componentString)
            {
                var isLongFormYear = componentString.StartsWith('Y');
                var isExponent = false;
                var isPrecision = false;

                StringBuilder digits = new();

                for (int i = isLongFormYear ? 1 : 0; i < componentString.Length; i++)
                {
                    var currentChar = componentString[i];

                    if (char.IsDigit(currentChar) || currentChar == '-')                // Character is year digit or negative sign.
                    {
                        digits.Append(currentChar);
                    }
                    else if (currentChar == 'E')                                            // Component indicates exponent.
                    {
                        extendedDateTime.Year = int.Parse(digits.ToString());
                        extendedDateTime.Year.SetFlags(flags == DateTimeValue.ValueFlags.None ? DateTimeValue.ValueFlags.Exact : flags);
                        extendedDateTime.Year.SignificantDigits = digits.Length - (digits[0] == '-' ? 1 : 0);
                        extendedDateTime.Precision = ExtendedDateTimePrecision.Year;

                        isLongFormYear = true;

                        digits.Clear();

                        isExponent = true;
                    }
                    else if (currentChar == 'S')
                    {
                        if (isExponent)                            // Component indicates precision.
                        {
                            extendedDateTime.YearExponent = int.Parse(digits.ToString());

                            digits.Clear();

                            isPrecision = true;
                            isExponent = false;
                        }
                        else
                        {
                            extendedDateTime.Year = int.Parse(digits.ToString());
                            extendedDateTime.Year.SetFlags(flags == DateTimeValue.ValueFlags.None ? DateTimeValue.ValueFlags.Exact : flags);
                            extendedDateTime.Year.IsLong = true;
                            extendedDateTime.Precision = ExtendedDateTimePrecision.Year;

                            isPrecision = true;
                            isLongFormYear = true;

                            digits.Clear();
                        }
                    }
                    else
                    {
                        throw new ParseException("The date part (possibly year) is invalid.", componentString);
                    }
                }

                if (isExponent)
                {
                    extendedDateTime.YearExponent = int.Parse(digits.ToString());
                }
                else if (isPrecision)
                {
                    extendedDateTime.YearPrecision = int.Parse(digits.ToString());
                    extendedDateTime.Year.SignificantDigits = (int)extendedDateTime.YearPrecision;
                }
                else if (extendedDateTime.IsEmpty)
                {
                    char firstChar = digits[0];
                    bool bNegative = firstChar == '-';
                    int significant = bNegative ? digits.Length - 1 : digits.Length;        // remember the number of significant digits, if leading zeros are present

                    ThrowParseExceptionIfTrue(significant == 0 || significant == 1 && bNegative, "The value must have more than zero digits.", componentString);

                    var year = int.Parse(digits.ToString());

                    // set here, it may get overwritten later
                    extendedDateTime.Year = year;
                    if (isLongFormYear)
                    {
                        extendedDateTime.Year.SetFlags((flags == DateTimeValue.ValueFlags.None ? DateTimeValue.ValueFlags.Exact : flags) | DateTimeValue.ValueFlags.Long);
                    }
                    else
                    {
                        extendedDateTime.Year.SetFlags(flags == DateTimeValue.ValueFlags.None ? DateTimeValue.ValueFlags.Exact : flags);
                    }

                    extendedDateTime.Year.SignificantDigits = significant;
                    extendedDateTime.Precision = ExtendedDateTimePrecision.Year;

                    if (isLongFormYear)
                    {
                        ThrowParseExceptionIfTrue(isExponent && year == 0, "The significand of a long year cannot be zero.", componentString);
                        ThrowParseExceptionIfTrue(significant < 4, "The long year must have at least four digits.", componentString);
                    }
                    else if (significant < 4 || year < -9999 || year > 9999)
                    {
                        throw new ParseException("The year must have four digits.", componentString);
                    }
                }
            }

            bool CommitMonthOrSeason(bool hasSeasonComponent, DateTimeValue.ValueFlags flags, ExtendedDateTime extendedDateTime, string componentString)
            {
                // monthOrSeason or season
                int significant = componentString.Length;        // remember the number of significant digits, if leading zeros are present

                ThrowParseExceptionIfTrue(significant != 2, "The monthOrSeason/season must have two digits.", componentString);
                ThrowParseExceptionIfTrue(componentString.Any(c => !char.IsDigit(c)), "The monthOrSeason must be a number.", componentString);

                var monthInteger = int.Parse(componentString);

                if (monthInteger < 1 || monthInteger > 12)
                {
                    ThrowParseExceptionIfTrue(monthInteger == 0 || monthInteger < (int)Season.First || monthInteger > (int)Season.Last, $"The monthOrSeason must be between 1 and 12. Or, if a Season is intended, the value must be between {(int)Season.First} and {(int)Season.Last}", componentString);

                    extendedDateTime.Season = monthInteger;
                    extendedDateTime.Season.SetFlags(flags);
                    extendedDateTime.Season.SignificantDigits = significant;
                    extendedDateTime.Precision = ExtendedDateTimePrecision.Season;

                    hasSeasonComponent = true;
                }
                else
                {
                    extendedDateTime.Month = monthInteger;
                    extendedDateTime.Month.SetFlags(flags);
                    extendedDateTime.Season.SignificantDigits = significant;
                    extendedDateTime.Precision = ExtendedDateTimePrecision.Month;
                }

                return hasSeasonComponent;
            }

            void CommitDay(bool hasSeasonComponent, DateTimeValue.ValueFlags flags, ExtendedDateTime extendedDateTime, string componentString)
            {
                ThrowParseExceptionIfTrue(hasSeasonComponent, "A season and day cannot coexist.", componentString);
                ThrowParseExceptionIfTrue(componentString.Length != 2, "The day must have two digits.", componentString);
                ThrowParseExceptionIfTrue(componentString.Any(c => !char.IsDigit(c)), "The day must be a number.", componentString);

                var dayInteger = int.Parse(componentString);

                ThrowParseExceptionIfTrue(dayInteger < 1 || dayInteger > 31, "The day must be between 1 and 31.", componentString);

                var daysInMonth = ExtendedDateTimeCalculator.DaysInMonth(extendedDateTime.Year, extendedDateTime.Month);

                ThrowParseExceptionIfTrue(dayInteger > daysInMonth, "The monthOrSeason in the year has less days than indicated.", componentString);

                extendedDateTime.Day = dayInteger;
                extendedDateTime.Day.SetFlags(flags);
                extendedDateTime.Precision = ExtendedDateTimePrecision.Day;
            }
        }

        /// <summary>
        /// The commit result for date and time commits
        /// </summary>
        private enum CommitResult
        {
            Success,
            Invalid,
            Incomplete
        }

        /// <summary>
        /// Throws a ParseException if the expression is true.
        /// </summary>
        /// <param name="expression">The expression result</param>
        /// <param name="message">The exception message</param>
        /// <param name="component">The component causing</param>
        private static void ThrowParseExceptionIfTrue(bool expression, string message, string? component = null)
        {
            if (expression)
            {
                throw new ParseException(message, component);
            }
        }

        /// <summary>
        /// Throws a ParseException if the expression is true.
        /// </summary>
        /// <param name="expression">The expression result</param>
        /// <param name="message">The exception message</param>
        /// <param name="component">The component causing</param>
        private static void ThrowInvalidDateExceptionIfTrue(bool expression, string message, string? component = null)
        {
            if (expression)
            {
                throw new InvalidDateException(message, component);
            }
        }

        /// <summary>
        /// Throws a ParseException if the expression is true.
        /// </summary>
        /// <param name="expression">The expression result</param>
        /// <param name="message">The exception message</param>
        /// <param name="component">The component causing</param>
        private static void ThrowInvalidTimeExceptionIfTrue(bool expression, string message, string? component = null)
        {
            if (expression)
            {
                throw new InvalidTimeException(message, component);
            }
        }

        /// <summary>
        /// Commits the time component.
        /// </summary>
        /// <param name="timeComponentIndex">The time component index.</param>
        /// <param name="timeZonePart">If true, time zone part.</param>
        /// <param name="componentBuffer">The component buffer.</param>
        /// <param name="extendedDateTime">The extended date time.</param>
        private static CommitResult CommitTimeComponent(ref int timeComponentIndex, bool timeZonePart, StringBuilder componentBuffer, ref ExtendedDateTime extendedDateTime)
        {
            if (componentBuffer.Length == 0)
            {
                return CommitResult.Success;
            }

            var componentString = componentBuffer.ToString();

            if (timeComponentIndex == 0 && !timeZonePart)                                                               // We expect hours to appear first.
            {
                ThrowParseExceptionIfTrue(componentString.Any(c => !char.IsDigit(c)), "The hour must be a number.", componentString);
                ThrowParseExceptionIfTrue(componentString.Length != 2 && !(componentString.Length == 3 && componentString.StartsWith("-")), "The hour must be two digits long.", componentString);

                extendedDateTime.Hour = int.Parse(componentString);
                extendedDateTime.Precision = ExtendedDateTimePrecision.Hour;

                ThrowInvalidTimeExceptionIfTrue(!((int)extendedDateTime.Hour).IsWithin(0, 23), "The hour must be 0 - 23.");
                timeComponentIndex++;
            }
            else if (timeComponentIndex == 1 && !timeZonePart)                                                         // We expect minutes to appear second.
            {
                ThrowParseExceptionIfTrue(componentString.Any(c => !char.IsDigit(c)), "The minute must be a number.", componentString);
                ThrowParseExceptionIfTrue(componentString.Length != 2, "The minute must be two digits long.", componentString);

                extendedDateTime.Minute = int.Parse(componentString);
                extendedDateTime.Precision = ExtendedDateTimePrecision.Minute;

                ThrowInvalidTimeExceptionIfTrue(!((int)extendedDateTime.Minute).IsWithin(0, 59), "The minute must be 0 - 59.");

                timeComponentIndex++;
            }
            else if (timeComponentIndex == 2 && !timeZonePart)                                                        // We expect seconds to appear third.
            {
                ThrowParseExceptionIfTrue(componentString.Any(c => !char.IsDigit(c)), "The second must be a number.", componentString);
                ThrowParseExceptionIfTrue(componentString.Length != 2, "The second must be two digits long.", componentString);

                extendedDateTime.Second = int.Parse(componentString);
                extendedDateTime.Precision = ExtendedDateTimePrecision.Second;

                ThrowInvalidTimeExceptionIfTrue(!((int)extendedDateTime.Second).IsWithin(0, 59), "The second must be 0 - 59. (leap seconds are not supported)");

                timeComponentIndex++;
            }
            else if (timeZonePart)
            {
                if (componentString.StartsWith("Z"))
                {
                    extendedDateTime.UtcOffset = TimeSpan.Zero;
                }
                else if (componentString.StartsWith("+") || componentString.StartsWith("-"))         // It must be a non-UTC time zone offset.
                {
                    var timeZoneOffsetComponentStrings = componentString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                    ThrowParseExceptionIfTrue(timeZoneOffsetComponentStrings.Length == 0, "The time zone offset must have at least two digits.", componentString);

                    var hourOffsetString = timeZoneOffsetComponentStrings[0];                        // Time zone hours offset.

                    ThrowParseExceptionIfTrue(hourOffsetString.Any(c => !char.IsDigit(c) && c != '+' && c != '-'), "The time zone hour offset must be a number.", hourOffsetString);

                    if (hourOffsetString.StartsWith("+") || hourOffsetString.StartsWith("-"))
                    {
                        ThrowParseExceptionIfTrue(hourOffsetString.Length != 3, "The time zone hour offset must have exactly two digits.", hourOffsetString);
                    }
                    else
                        ThrowParseExceptionIfTrue(hourOffsetString.Length != 2, "The time zone hour offset must have exactly two digits.", hourOffsetString);

                    extendedDateTime.UtcOffset = TimeSpan.FromHours(double.Parse(hourOffsetString));

                    if (timeZoneOffsetComponentStrings.Length == 2)                                  // Optional time zone minutes offset.
                    {
                        var minuteOffsetString = timeZoneOffsetComponentStrings[1];

                        ThrowParseExceptionIfTrue(minuteOffsetString.Any(c => !char.IsDigit(c)), "The time zone minute offset must be a number.", minuteOffsetString);
                        ThrowParseExceptionIfTrue(minuteOffsetString.Length != 2, "The time zone minute offset must have exactly two digits.", minuteOffsetString);

                        extendedDateTime.UtcOffset = extendedDateTime.UtcOffset is null
                            ? TimeSpan.FromMinutes(double.Parse(minuteOffsetString))
                            : extendedDateTime.UtcOffset + TimeSpan.FromMinutes(double.Parse(minuteOffsetString));
                    }

                    ThrowParseExceptionIfTrue(timeZoneOffsetComponentStrings.Length > 2, "The time zone offset can have at most two components: hours and minutes.", componentString);
                }
            }
            else
            {
                throw new ParseException("The time can have at most three components excluding the time zone.", componentString);
            }

            componentBuffer.Clear();

            return CommitResult.Success;
        }

        /// <summary>
        /// redundant here, duplicate of the one in DateTimeValue
        /// </summary>
        /// <param name="me">The me.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>A bool.</returns>
        public static bool IsWithin(this int me, int start, int end)
        {
            if (end < start)
            {
                throw new ArgumentException("End must be greater than Start");
            }

            return me >= start && me <= end;
        }

        /// <summary>
        /// Gets the flag.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns>An ValueFlag</returns>
        private static DateTimeValue.ValueFlags GetFlag(char character)
        {
            if (character == '?')           // Uncertain
            {
                return DateTimeValue.ValueFlags.Uncertain;
            }
            else if (character == '~')      // Approximate
            {
                return DateTimeValue.ValueFlags.Approximate;
            }
            else if (character == '%')      // Uncertain + Approximate
            {
                return DateTimeValue.ValueFlags.Approximate | DateTimeValue.ValueFlags.Uncertain;
            }

            return DateTimeValue.ValueFlags.None;
        }
    }
}