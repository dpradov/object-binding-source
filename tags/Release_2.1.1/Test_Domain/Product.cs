using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;

//[TypeConverter(typeof(EnumToStringUsingDescription))]
public enum ProductType
{
    [Description("Type UNO")]        Type_1 = 0,
    [Description("Type DOS")]        Type_2 = 1
}

public class Product : BusinessBase
{
    private int _productId;
    private string _name;
    private double _unitPrice;
    private ProductType _type;
    private string _otherProperty;
    
    public Product()
    {
    }

    public Product(int productId, string name, double unitPrice)
    {
        _productId = productId;
        _name = name;
        _unitPrice = unitPrice;
        _otherProperty = "Other-" + name;
    }


    public int ProductId
    {
        get { return _productId; }
        set
        {
            _productId = value;
            OnPropertyChanged("ProductId");
        }
    }

    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            OnPropertyChanged("Name");
        }
    }

    public double UnitPrice
    {
        get { return _unitPrice; }
        set
        {
            _unitPrice = value;
            OnPropertyChanged("UnitPrice");
        }
    }

    public ProductType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            OnPropertyChanged("Type");
        }
    }

    public string OtherProperty
    {
        get { return _otherProperty; }
        set
        {
            _otherProperty = value;
            OnPropertyChanged("OtherProperty");
        }
    }

    public void ChangeMultiple()
    { 
        _unitPrice = 50;
        _name += "^^**";
        _otherProperty += "**";
        OnPropertyChanged(null);
    }

    public Product Self
    {            
        get
        {
            return this;
        }
    }

    public override string ToString()
    {
        return String.Format("Product:{0}-{1}", _productId , _name );
    }


}
