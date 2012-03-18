using System;
using System.Collections.Generic;
using System.Text;


public class OtherItem : BusinessBase
{
    private int _otherItemId;
    private string _name;
    private Product _product;

    public OtherItem(int otherItemId, string name, Product product)
    {
        _otherItemId = otherItemId;
        _name = name;
        _product = product;
    }


    public int OtherItemId
    {
        get { return _otherItemId; }
        set
        {
            _otherItemId = value;
            OnPropertyChanged("OtherItemId");
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

    public Product Product
    {
        get { return _product; }
        set
        {
            _product = value;
            OnPropertyChanged("Product");
        }
    }

    public OtherItem Self
    {
        get
        {
            return this;
        }
    }

    public override string ToString()
    {
        return String.Format("OtherItem:{0}-{1}", _otherItemId, _name);
    }


}
