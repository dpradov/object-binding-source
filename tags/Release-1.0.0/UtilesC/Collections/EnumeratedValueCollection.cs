using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

// Proposal of peSHlr (http://stackoverflow.com/users/50846/peshir) in response to question
// How do I override ToString in C# enums? 
// (http://stackoverflow.com/questions/796607/how-do-i-override-tostring-in-c-enums)

public class EnumeratedValueCollection<T> : ReadOnlyCollection<EnumeratedValue<T>>
{
    public EnumeratedValueCollection()
        : base(ListConstructor()) { }
    public EnumeratedValueCollection(Func<T, bool> selection)
        : base(ListConstructor(selection)) { }
    public EnumeratedValueCollection(Func<T, string> format)
        : base(ListConstructor(format)) { }
    public EnumeratedValueCollection(Func<T, bool> selection, Func<T, string> format)
        : base(ListConstructor(selection, format)) { }
    internal EnumeratedValueCollection(IList<EnumeratedValue<T>> data)
        : base(data) { }

    internal static List<EnumeratedValue<T>> ListConstructor()
    {
        return ListConstructor(null, null);
    }

    internal static List<EnumeratedValue<T>> ListConstructor(Func<T, string> format)
    {
        return ListConstructor(null, format);
    }

    internal static List<EnumeratedValue<T>> ListConstructor(Func<T, bool> selection)
    {
        return ListConstructor(selection, null);
    }

    internal static List<EnumeratedValue<T>> ListConstructor(Func<T, bool> selection, Func<T, string> format)
    {
        if (null == selection) selection = (x => true);
        if (null == format) format = (x => GetDescription(x));
        var result = new List<EnumeratedValue<T>>();
        foreach (T value in System.Enum.GetValues(typeof(T)))
        {
            if (selection(value))
            {
                string description = format(value);
                result.Add(new EnumeratedValue<T>(value, description));
            }
        }
        return result;
    }

    public bool Contains(T value)
    {
        return (Items.FirstOrDefault(item => item.Value.Equals(value)) != null);
    }

    public EnumeratedValue<T> this[T value]
    {
        get
        {
            return Items.First(item => item.Value.Equals(value));
        }
    }

    public string Describe(T value)
    {
        return this[value].Description;
    }

    static string GetDescription(T x)
    {
        string name = x.ToString();
        object[] attrs = x.GetType().GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return (attrs.Length > 0) ? ((DescriptionAttribute)attrs[0]).Description : name;
    }
}



[System.Diagnostics.DebuggerDisplay("{Value} ({Description})")]
public class EnumeratedValue<T>
{
    private T value;
    private string description;
    internal EnumeratedValue(T value, string description)
    {
        this.value = value;
        this.description = description;
    }
    public T Value { get { return this.value; } }
    public string Description { get { return this.description; } }
}
