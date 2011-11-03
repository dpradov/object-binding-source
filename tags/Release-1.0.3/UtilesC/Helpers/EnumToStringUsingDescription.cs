using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

// Proposal of scraimer (http://stackoverflow.com/users/54491/scraimer) in response to question
// How do I override ToString in C# enums? 
// (http://stackoverflow.com/questions/796607/how-do-i-override-tostring-in-c-enums)

// Tal y como está se muestran bien los valores, pero no se permite la modificación porque se está 
// convirtiendo Enum a String, y luego no se ofrece la conversión desde String a Enum. 
// Faltaría desarrollar la conversión inversa


public class EnumToStringUsingDescription : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return (sourceType.Equals(typeof(Enum)));
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return (destinationType.Equals(typeof(String)));
    }

    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    {
        return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
    {
        if (!destinationType.Equals(typeof(String)))
        {
            throw new ArgumentException("Can only convert to string.", "destinationType");
        }

        if (!value.GetType().BaseType.Equals(typeof(Enum)))
        {
            throw new ArgumentException("Can only convert an instance of enum.", "value");
        }

        string name = value.ToString();
        object[] attrs =
            value.GetType().GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return (attrs.Length > 0) ? ((DescriptionAttribute)attrs[0]).Description : name;
    }
}

