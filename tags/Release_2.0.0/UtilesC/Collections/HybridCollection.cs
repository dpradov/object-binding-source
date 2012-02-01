using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

//# Corresponds to code released by seesharper (http://www.codeproject.com/Members/seesharper) 
//# and licensed under The Code Project Open License (CPOL) (http://www.codeproject.com/info/cpol10.aspx)
//# The original work of seesharper is available in: 
//#    http://www.codeproject.com/KB/cs/ObjectBindingSource.aspx
//# You can also download it from: 
//#    http://code.google.com/p/object-binding-source/downloads/list

[Serializable]
public class HybridCollection<TKey, TValue> : IDictionary<TKey,TValue>,IEnumerable<TValue>
{
    private Dictionary<TKey, TValue> _dictionary;

    public HybridCollection()
    {
        _dictionary = new Dictionary<TKey, TValue>();
    }

    private HybridCollection(Dictionary<TKey, TValue> dictionary)
    {
        _dictionary = dictionary;
    }

    public HybridCollection<TKey, TValue> Clone()
    {
        return new HybridCollection<TKey, TValue>(_dictionary);
    }
    

    public TValue this[TKey key]
    {
        get
        {
            if (_dictionary.ContainsKey(key))
                return _dictionary[key];
            else
                return default(TValue);
        }
        set
        {
            if (_dictionary.ContainsKey(key))
                _dictionary[key] = value;
            else
                _dictionary.Add(key,value);
        }
    }

    public bool Contains(TKey key)
    {
        return _dictionary.ContainsKey(key);
    }

    public bool Contains<T>(T value) where T : TValue, IKeyProvider<TKey>
    {
        return Contains(value.Key);
    }

    

    public void Add(TKey key, TValue value)
    {
        if (!_dictionary.ContainsKey(key))
            _dictionary.Add(key, value);
    }

    public void Add(object item)
    {
        if (!typeof(IKeyProvider<TKey>).IsAssignableFrom(item.GetType()))
        {
            throw new ArgumentException("Object has to implement IKeyProvider<TKey>", "item");
        }
        else
            if (!_dictionary.ContainsKey(((IKeyProvider<TKey>)item).Key))
            {
                _dictionary.Add(((IKeyProvider<TKey>)item).Key,(TValue)item);
            }
    }
    
    
    public void Add<T>(T value) where T : TValue, IKeyProvider<TKey>
    {
        Add(value.Key, value);

    }

    public void Remove(TKey key)
    {
        if (_dictionary.ContainsKey(key))
            _dictionary.Remove(key);
    }

    public void Remove<T>(T value) where T : TValue, IKeyProvider<TKey>
    {
        Remove(value.Key);
    }

    public void Clear()
    {
        _dictionary.Clear();
    }

    public int Count
    {
        get
        {
            return _dictionary.Count;
        }
    }


   

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return _dictionary.Values.GetEnumerator();
    }

    #endregion

    //#region ISerializable Members

    //public void GetObjectData(SerializationInfo info, StreamingContext context)
    //{
    //    info.AddValue("Dictionary", _dictionary, typeof(Dictionary<TKey, TValue>));
    //}

    //#endregion

    #region IDictionary<TKey,TValue> Members


    public bool ContainsKey(TKey key)
    {
        return _dictionary.ContainsKey(key);
    }

    public ICollection<TKey> Keys
    {
        get { return _dictionary.Keys; }
    }

    bool IDictionary<TKey, TValue>.Remove(TKey key)
    {
        return _dictionary.Remove(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        return _dictionary.TryGetValue(key, out value);
    }

    public ICollection<TValue> Values
    {
        get { return _dictionary.Values; }
    }

    #endregion

    #region ICollection<KeyValuePair<TKey,TValue>> Members

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Add(item);
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Contains(item);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(array,arrayIndex);
    }

    public bool IsReadOnly
    {
        get { return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).IsReadOnly; }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Remove(item);
    }

    #endregion

    #region IEnumerable<KeyValuePair<TKey,TValue>> Members

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
        return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).GetEnumerator();
    }

    #endregion





    #region IEnumerable<TValue> Members

    public IEnumerator<TValue> GetEnumerator()
    {
        return _dictionary.Values.GetEnumerator();
    }

    #endregion
}
