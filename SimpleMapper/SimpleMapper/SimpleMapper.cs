using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SimpleMapper
{
    /// <summary>
    /// SimpleMapper
    /// <author>mango</author>>
    /// </summary>
    /// <typeparam name="TSource">Source</typeparam>
    /// <typeparam name="TTarget">Target</typeparam>
    public class SimpleMapper<TSource, TTarget>
    {
        //cache this Func
        private static readonly Func<TSource, TTarget> _func = null;
        /// <summary>
        /// transformation lamdba
        /// </summary>
        static SimpleMapper()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            //Properties transformation
            foreach (var item in typeof(TTarget).GetProperties())
            {
                var propertyInfo = typeof(TSource).GetProperty(item.Name);
                //Attribute first
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
            //Fields transformation
            foreach (var item in typeof(TTarget).GetFields())
            {
                var fieldInfo = typeof(TSource).GetField(item.Name);
                if (fieldInfo != null)
                {
                    MemberExpression property = Expression.Field(parameterExpression, fieldInfo);
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TTarget)), memberBindingList.ToArray());
            var lambda = Expression.Lambda<Func<TSource, TTarget>>(memberInitExpression, new ParameterExpression[]
            {
                parameterExpression
            });
            _func = lambda.Compile();//Compile lambda expression
        }
        /// <summary>
        /// Mapping object
        /// </summary>
        /// <param name="t">Source</param>
        /// <returns>Target</returns>
        public static TTarget Map(TSource t)
        {
            return _func(t);
        }
    }
}
