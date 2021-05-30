using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleMapper
{
    /// <summary>
    /// SimpleMapper
    /// <author>mango</author>
    /// </summary>
    /// <typeparam name="TSource">Source</typeparam>
    /// <typeparam name="TTarget">Target</typeparam>
    public static class SimpleMapper<TSource, TTarget>
    {
        // Cache this Func
        private static readonly Func<TSource, TTarget> _func = null;
        /// <summary>
        /// Transformation lamdba
        /// </summary>
        static SimpleMapper()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "t");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            // Properties transformation
            foreach (var item in typeof(TTarget).GetProperties())
            {
                var propertyInfo = typeof(TSource).GetProperty(SimpleProfiles<TSource, TTarget>.GetFieldMap(item.Name) ?? item.Name);
                // Get the first attribute
                if (item.IsDefined(typeof(SimpleNameAttribute), true))
                {
                    var attributeName = (SimpleNameAttribute)item.GetCustomAttributes(typeof(SimpleNameAttribute), true)[0];
                    propertyInfo = typeof(TSource).GetProperty(attributeName.Name);
                }
                if (propertyInfo != null)
                {
                    MemberExpression property = Expression.Property(parameterExpression, propertyInfo);
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
            }
            // Fields transformation
            foreach (var item in typeof(TTarget).GetFields())
            {
                var fieldInfo = typeof(TSource).GetField(SimpleProfiles<TSource, TTarget>.GetFieldMap(item.Name) ?? item.Name);
                if (fieldInfo != null)
                {
                    MemberExpression property = Expression.Field(parameterExpression, fieldInfo);
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
            }
            // Compile lambda expression
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TTarget)), memberBindingList.ToArray());
            var lambda = Expression.Lambda<Func<TSource, TTarget>>(memberInitExpression, new ParameterExpression[]
            {
                parameterExpression
            });
            _func = lambda.Compile();
        }
        /// <summary>
        /// Mapping entity
        /// </summary>
        /// <param name="t">Source</param>
        /// <returns>Target</returns>
        public static TTarget Map(TSource t)
        {
            return _func(t);
        }
        /// <summary>
        /// Mapping to list entity
        /// </summary>
        /// <param name="sources">Sources</param>
        /// <returns>Targets</returns>
        public static List<TTarget> MapList(List<TSource> sources)
        {
            return sources.Select(t => _func(t)).ToList();
        }
    }
}
