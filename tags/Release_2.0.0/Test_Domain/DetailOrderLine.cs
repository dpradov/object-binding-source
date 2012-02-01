using System;
using System.Collections.Generic;
using System.Text;

public class DetailOrderLine : BusinessBase
{
    private int _detail;
    private Customer _recomendedBy;
    private int _aux;

    public DetailOrderLine()
    {
    }

    public DetailOrderLine(Customer recomendedBy)
    {
        _detail = new Random().Next(1000);
        _recomendedBy = recomendedBy;
    }

    public Customer RecomendedBy
    {
        get { return _recomendedBy; }
        set
        {
            _recomendedBy = value;
            OnPropertyChanged("RecomendedBy");
        }
    }

    public int NotShownProperty
    {
        get { return _aux;  }
        set
        {
            _aux = value;
            OnPropertyChanged("NotShownProperty");
        }
    }


    public override string ToString()
    {
        return String.Format("<DetailOrderLine: [{0} -> {1}]>", _detail, _recomendedBy);
    }

}
