using System;
using System.Collections.Generic;
using System.Text;

public class OrderLine : BusinessBase
{
    private Product _product;
    private int _quantity;
    private List<DetailOrderLine> _details = new List<DetailOrderLine>();

    public OrderLine()
    {
    }

    public OrderLine(Product product, int quantity)
    {
        _product = product;
        _quantity = quantity;
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

    public int Quantity
    {
        get { return _quantity; }
        set
        {
            _quantity = value;
            OnPropertyChanged("Quantity");
        }
    }

    public List<DetailOrderLine> Details
    {
        get { return _details; }
    }

    public List<DetailOrderLine> OtherDetails
    {
        get { return _details; }
    }

    public override string ToString()
    {
        return String.Format("<OrderLine: [{0} -> {1}]>", _product,_quantity );
    }

}
