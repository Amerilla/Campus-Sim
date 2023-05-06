using System;
using JetBrains.Annotations;

public static class Utilities
{
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
}