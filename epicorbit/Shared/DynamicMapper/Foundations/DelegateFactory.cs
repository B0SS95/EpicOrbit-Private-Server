using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DynamicMapper.Foundations {
    static class DelegateFactory {

        public static Func<TSource, object> Get<TSource>(FieldInfo fieldInfo) {
            var sourceParam = Expression.Parameter(typeof(object));
            Expression returnExpression = Expression.Field
            (Expression.Convert(sourceParam, typeof(TSource)), fieldInfo);
            if (!fieldInfo.FieldType.IsClass) {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return (Func<TSource, object>)Expression.Lambda(returnExpression, sourceParam).Compile();
        }

        public static Action<TSource, object> Set<TSource>(FieldInfo fieldInfo) {
            var sourceParam = Expression.Parameter(typeof(object));
            var valueParam = Expression.Parameter(typeof(object));
            var convertedValueExpr = Expression.Convert(valueParam, fieldInfo.FieldType);
            Expression returnExpression = Expression.Assign(Expression.Field
            (Expression.Convert(sourceParam, typeof(TSource)), fieldInfo), convertedValueExpr);
            if (!fieldInfo.FieldType.IsClass) {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            var lambda = Expression.Lambda(typeof(Action<TSource, object>),
                returnExpression, sourceParam, valueParam);
            return (Action<TSource, object>)lambda.Compile();
        }

        public static Func<TSource, object> Get<TSource>(PropertyInfo propertyInfo) {
            var sourceObjectParam = Expression.Parameter(typeof(object));
            Expression returnExpression = Expression.Call(Expression.Convert(sourceObjectParam, typeof(TSource)), propertyInfo.GetMethod);
            if (!propertyInfo.PropertyType.IsClass) {
                returnExpression = Expression.Convert(returnExpression, typeof(object));
            }
            return (Func<TSource, object>)Expression.Lambda(returnExpression, sourceObjectParam).Compile();
        }

        public static Action<TSource, object> Set<TSource>(PropertyInfo propertyInfo) {
            var sourceObjectParam = Expression.Parameter(typeof(object));
            ParameterExpression propertyValueParam;
            Expression valueExpression;
            if (propertyInfo.PropertyType == typeof(object)) {
                propertyValueParam = Expression.Parameter(propertyInfo.PropertyType);
                valueExpression = propertyValueParam;
            } else {
                propertyValueParam = Expression.Parameter(typeof(object));
                valueExpression = Expression.Convert
                (propertyValueParam, propertyInfo.PropertyType);
            }
            return (Action<TSource, object>)Expression.Lambda(Expression.Call
                   (Expression.Convert(sourceObjectParam, typeof(TSource)),
                   propertyInfo.SetMethod, valueExpression),
                    sourceObjectParam, propertyValueParam).Compile();
        }



    }
}
