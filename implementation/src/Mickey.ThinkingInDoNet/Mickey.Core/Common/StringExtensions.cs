using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Mickey.Core.Common
{
    /// <summary>
    /// <see cref="String"/>的扩展方法。
    /// </summary>
    public static class StringExtensions
    {
        public static TValue As<TValue>(this string value)
        {
            return As<TValue>(value, default(TValue));
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We want to make this user friendly and return the default value on all failures")]
        public static TValue As<TValue>(this string value, TValue defaultValue)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
                if (converter.CanConvertFrom(typeof(string)))
                {
                    return (TValue)converter.ConvertFrom(value);
                }
                // try the other direction
                converter = TypeDescriptor.GetConverter(typeof(string));
                if (converter.CanConvertTo(typeof(TValue)))
                {
                    return (TValue)converter.ConvertTo(value, typeof(TValue));
                }
            }
            catch
            {
                // eat all exceptions and return the defaultValue, assumption is that its always a parse/format exception
            }
            return defaultValue;
        }
    }
}
