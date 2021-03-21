using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;


public static class EnumExtension
{
    private class EnumCache
    {
        public EnumCache()
        {
            StringToEnum = new Dictionary<string, object>();
            EnumToString = new Dictionary<object, string>();
        }

        public Dictionary<string, object> StringToEnum { get; }

        public Dictionary<object, string> EnumToString { get; }
    }

    static EnumExtension()
    {
        EnumsCache = new ConcurrentDictionary<Type, EnumCache>();
    }

    public static bool HasEnumVal<T>(this string description)
        where T : struct
    {
        Debug.Assert(typeof(T).IsEnum);
        EnumCache cache = GetCache(typeof(T));
        return null != cache.StringToEnum.TryGetValue(description);
    }

    public static T ToEnumVal<T>(this string description)
        where T : struct
    {
        Debug.Assert(typeof(T).IsEnum);
        EnumCache cache = GetCache(typeof(T));

        object value;
        if (!cache.StringToEnum.TryGetValue(description, out value))
            throw new Exception($"Enum element with description \"{description}\" not found in enum \"{typeof(T).Name}\"");

        return (T)value;
    }

    public static string ToDescriptionVal(this Enum enumItem)
    {
        EnumCache cache = GetCache(enumItem.GetType());

        string value;
        if (!cache.EnumToString.TryGetValue(enumItem, out value))
            throw new Exception($"Enum element \"{enumItem}\" has no description attribute in enum \"{enumItem.GetType().Name}\"");

        return value;
    }

    private static EnumCache GetCache(Type type)
    {
        EnumCache cache;
        if (EnumsCache.TryGetValue(type, out cache))
            return cache;

        cache = new EnumCache();
        foreach (FieldInfo fieldInfo in type.GetFields())
        {
            DescriptionAttribute attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
            if (null == attribute)
                continue;

            if (cache.StringToEnum.ContainsKey(attribute.Description))
                throw new Exception($"Enum \"{type.Name}\" has more than 1 element with equal description \"{attribute.Description}\"");

            object enumValue = fieldInfo.GetValue(null);
            cache.StringToEnum.Add(attribute.Description, enumValue);
            cache.EnumToString.Add(enumValue, attribute.Description);
        }

        EnumsCache.TryAdd(type, cache);
        return cache;
    }

    private static readonly ConcurrentDictionary<Type, EnumCache> EnumsCache;
}
