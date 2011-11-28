using System;
using System.Collections.Generic;
using System.Text;

public interface IKeyProvider<T>
{
    T Key { get; }
}
