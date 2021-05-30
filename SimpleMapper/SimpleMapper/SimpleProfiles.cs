using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SimpleMapper
{
    /// <summary>
    /// Profiles
    /// <author>mango</author>
    /// </summary>
    /// <typeparam name="TSource">Source</typeparam>
    /// <typeparam name="TTarget">Target</typeparam>
    public class SimpleProfiles<TSource, TTarget>
    {
        // mapping fields 
        private static IDictionary<string, Dictionary<string, string>> _fieldMaps = new Dictionary<string, Dictionary<string, string>>();
        public SimpleProfiles(IDictionary<string, string> fieldMapList = null)
        {
            if (fieldMapList != null)
            {
                foreach (var item in fieldMapList)
                {
                    this.CreateMap(item.Key, item.Value);
                }
            }
        }
        /// <summary>
        /// unique profile_Key
        /// </summary>
        public static string ProfileKey => BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes($"{typeof(TSource)}&{typeof(TTarget)}"))).Replace("-", string.Empty);
        /// <summary>
        /// Create the field mapping
        /// </summary>
        /// <param name="sourceField">Source field</param>
        /// <param name="targetField">Target field</param>
        public void CreateMap(string sourceField, string targetField)
        {
            if (_fieldMaps.TryGetValue(ProfileKey, out var fieldList))
            {
                fieldList.TryAdd(targetField, sourceField);
            }
            else
            {
                _fieldMaps.Add(ProfileKey, new Dictionary<string, string>() { { targetField, sourceField } });
            }
        }
        /// <summary>
        /// Get this source field name
        /// </summary>
        /// <param name="fieldName">Target field name</param>
        /// <returns>Source field name</returns>
        internal static string GetFieldMap(string fieldName)
        {
            if (_fieldMaps.TryGetValue(ProfileKey, out var fieldList))
            {
                return fieldList.GetValueOrDefault(fieldName);
            }
            return null;
        }
    }
}
