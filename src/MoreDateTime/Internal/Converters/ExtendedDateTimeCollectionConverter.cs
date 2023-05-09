using System.ComponentModel;
using System.Globalization;

namespace MoreDateTime.Internal.Converters
{

    /// <summary>
    /// The extended date time collection converter.
    /// </summary>
    internal sealed class ExtendedDateTimeCollectionConverter : TypeConverter
    {
        /// <summary>
        /// Cans the convert from.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns>A bool.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Cans the convert to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>A bool.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the from.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <returns>An object.</returns>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value == null)
            {
                throw GetConvertFromException(value);
            }

            var source = value as string;

            if (source != null)
            {
                return ExtendedDateTimeCollection.Parse(source);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>An object.</returns>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType != null && value is ExtendedDateTimeCollection)
            {
                var instance = (ExtendedDateTimeCollection)value;

                if (destinationType == typeof(string))
                {
                    return instance.ToString();
                }
            }

#pragma warning disable CS8604 // Possible null reference argument.
            return base.ConvertTo(context, culture, value, destinationType);
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}