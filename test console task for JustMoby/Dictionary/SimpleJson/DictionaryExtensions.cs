using System;
using System.Collections.Generic;
using System.Linq;


public static class DictionaryExtensions
{
	public static TValue TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
	{
		TValue value;
		return !dictionary.TryGetValue(key, out value) ? default(TValue) : value;
	}

	public static TValue TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
	{
		TValue value;
		return !dictionary.TryGetValue(key, out value) ? default(TValue) : value;
	}

	public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
	{
		TValue value;
		return !dictionary.TryGetValue(key, out value) ? default(TValue) : value;
	}

	public static TValue TryGetValue<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary, TKey key)
	{
		TValue value;
		return !dictionary.TryGetValue(key, out value) ? default(TValue) : value;
	}

	public static void RemoveAll<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> condition)
	{
		foreach (KeyValuePair<TKey, TValue> current in dictionary.Where(condition).ToList())
		{
			dictionary.Remove(current.Key);
		}
	}

	public static void RemoveAll<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> condition)
	{
		foreach (KeyValuePair<TKey, TValue> current in dictionary.Where(condition).ToList())
		{
			dictionary.Remove(current.Key);
		}
	}
}
