using System;
using System.ComponentModel.DataAnnotations;

namespace Mickey.Core.Common
{
    /// <summary>
    /// 枚举帮助类。
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举值的名称。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
                throw new ArgumentException("传入的值不属于正确的枚举定义值", "value");

            var attributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Name : value.ToString();
        }
    }
}
