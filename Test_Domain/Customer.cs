using System;
using System.Collections.Generic;
using System.Text;

public class Customer : BusinessBase
{
    private int _customerId;
    private string _name;
    private Address _address;
    private int _age;
    private Product _favoriteProduct;


    public Customer()
    {
    }

    public Customer(int customerId, string name, Address address)
    {
        _customerId = customerId;
        _name = name;
        _address = address;
    }


    public int CustomerId
    {
        get { return _customerId; }
        set
        {
            _customerId = value;
            OnPropertyChanged("CustomerId");
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


    public Address BillingAddress
    {
        get { return _address; }
        set
        {
            _address = value;
            OnPropertyChanged("BillingAddress");
        }
    }

    public int Age
    {
        get { return _age; }
        set
        {
            _age = value;
            OnPropertyChanged("Age");
        }
    }

    public Product FavoriteProduct
    {
        get { return _favoriteProduct; }
        set
        {
            _favoriteProduct = value;
            OnPropertyChanged("FavoriteProduct");
        }
    }

    public Customer Self
    {
        get
        {
            return this;
        }
    }

    public override string ToString()
    {
        return String.Format("<Customer:{0}>", _name);
    }

}
