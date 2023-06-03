using Application.Dto;
using Application.Queries;
using Application.Reflection;
using Azure.Core;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Reflection
{
    public static class StartForLoopForReflection
    {
        public static IQueryable<T> StartFiltering<T>(this IQueryable<T> items, PropertyInfo[] properties, PageSearch request)
        {
           
            foreach (var property in properties)
            {
                var propertyInfo = request.GetType().GetProperty(property.Name);
                var propertyValue = propertyInfo.GetValue(request);

                if (propertyValue != null)
                {
                    var methodName = $"FilterBy{property.Name}";
                    var methodInfo = typeof(FilterFunctions).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);

                    if (methodInfo != null)
                    {
                        items = (IQueryable<T>)methodInfo.Invoke(null, new object[] { items, propertyValue });
                    }
                }
            }
            return items;
        }
    }
}
