using System;
using System.Collections.Generic;

namespace Mickey.Core.Common
{
    /// <summary>
    /// 字典扩展方法。
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 尝试从字典中获取指定键的值，如果键不存在返回默认值。
        /// </summary>
        /// <typeparam name="TKey">键类型。</typeparam>
        /// <typeparam name="TValue">值类型。</typeparam>
        /// <param name="dictionary">指定的字典。</param>
        /// <param name="key">指定的键。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns>获取指定键的值，如果键不存在返回默认值。</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            TValue value;
            if (dictionary.TryGetValue(key, out value))
                return value;

            return defaultValue;
        }
    }
}
