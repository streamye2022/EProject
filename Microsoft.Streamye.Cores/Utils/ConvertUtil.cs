using System;
using System.Collections.Generic;
using Microsoft.Streamye.Cores.Middleware;

namespace Microsoft.Streamye.Cores.Utils
{
    public class ConvertUtil
    {
        static public T MiddleResultDictsToObject<T>(MiddleResult middleResult) where T : new()
        {
            return DicToObject<T>(middleResult.resultDic);
        }
        
        static public IList<T> MiddleResultListToListObject<T>(MiddleResult middleResult) where T : new()
        {
            return ListToObject<T>(middleResult.resultList);
        }

        public static IList<T> ListToObject<T>(IList<IDictionary<string, object>> list) where T : new()
        {
            IList<T> tList = new List<T>();
            foreach (var dicts in list)
            {
                T entity = DicToObject<T>(dicts);
                tList.Add(entity);
            }

            return tList;
        }

        public static T DicToObject<T>(IDictionary<string, object> dict) where T : new()
        {
            Type mytype = typeof(T);
            var properties = mytype.GetProperties();
            T entity = new T();
            string val = string.Empty;
            object obj = null;

            foreach (var property in properties)
            {
                if (!dict.ContainsKey(property.Name))
                    continue;
                val = Convert.ToString(dict[property.Name]);
                object defaultVal;
                if (property.PropertyType.Name.Equals("String"))
                {
                    defaultVal = "";
                }
                else if (property.PropertyType.Name.Equals("Boolean"))
                {
                    defaultVal = false;
                    val = (val.Equals("1") || val.Equals("on")).ToString();
                }
                else if (property.PropertyType.Name.Equals("Decimal"))
                {
                    defaultVal = 0M;
                }
                else
                {
                    defaultVal = 0;
                }

                if (!property.PropertyType.IsGenericType)
                {
                    obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, property.PropertyType);
                }
                else
                {
                    Type genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                        obj = string.IsNullOrEmpty(val)
                            ? defaultVal
                            : Convert.ChangeType(val, Nullable.GetUnderlyingType(property.PropertyType));
                }

                property.SetValue(entity, obj, null);
            }

            return entity;
        }
    }
}