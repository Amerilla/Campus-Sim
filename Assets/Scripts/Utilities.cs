using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

public static class Utilities
{
    public static string RemoveAccents(string input) {
        return string.Concat(input.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
                    .ToLower();

    }
    
    public static T Parse<T>(string input) {
        if (!typeof(T).IsEnum) throw new ArgumentException($"ERROR! Given type \"{typeof(T).Name}\" must be an enum");
        try {
            return (T)Enum.Parse(typeof(T), input);
        } catch {
            throw new Exception($"Input \"{input}\" does not correspond to any {typeof(T).Name} value");
        }
    }
    
    public static T Parse<T>([CanBeNull] string input, T defaultT) {
        if (!typeof(T).IsEnum) throw new ArgumentException($"ERROR! Given type \"{typeof(T).Name}\" must be an enum");
        if (input == null) {
            return defaultT;
        } else {
            try {
                return (T)Enum.Parse(typeof(T), input);
            } catch {
                throw new Exception($"Input \"{input}\" does not correspond to any {typeof(T).Name} value");
            }
        }
    }
    
    public static string ToString<T>(List<T> list, Func<T, string> toString) {
        if (list.Count == 0) {
            return $"List[_EMPTY_]";
        }
        StringBuilder builder = new StringBuilder();
        builder.Append($"List[");
        foreach (var item in list) {
            builder.Append($"{toString(item)}, ");
        }

        builder.Append($"]");
        return builder.ToString();
    }
        
    public static string ToString<K, V>(Dictionary<K, V> dictionary, Func<K, string> toStringK, Func<V, string> toStringV) {
        if (dictionary.Count == 0) {
            return $"List[_EMPTY_]";
        }
            
        StringBuilder builder = new StringBuilder();
        builder.Append($"Dictionary [");
        foreach (var (key, value) in dictionary) {
            builder.Append($"({toStringK(key)} -> {toStringV(value)}), ");
        }
        builder.Append($"]\n");
        return builder.ToString();
    }  
}