using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

public class SimpleClass
{
    public SimpleClass(int id, string display)
    {
        this.id = id;
        this.display = display;
    }

    //[Bindable(true)]
    [Browsable(true)]
    public int id;

    [Browsable(true)]
    //[Bindable(true)]
    public string display;

    public int MyProperty
    {
        get { return id; }
    }
}
