using System;

// Nota: Código incluido en el descargable que 'seeSharper' ofrece como ejemplo en el artículo "Nested Binding"
// http://www.codeproject.com/KB/cs/ObjectBindingSource.aspx


/// <summary>
/// Factory class used to create new instances of the <seealso cref="DynamicAccessor"/> class.
/// </summary>
/// <remarks>
/// The factory class will only create a DynamicAccessor once for each type.
/// If the request for a type is made twice, the cached DynamicAccessor will be returned.
/// </remarks>
public static class DynamicAccessorFactory
{
    /// <summary>
    /// A collection of DynamicAccessors indexed by type
    /// </summary>
    private readonly static HybridCollection<Type, IDynamicAccessor> _DynamicAccessors = new HybridCollection<Type, IDynamicAccessor>();

    /// <summary>
    /// Returns a reference to a DynamicAccessor for the requested type represented by <seealso cref="IDynamicAccessor"/>.
    /// </summary>
    /// <param name="type">The for to get the DynamicAccessor</param>
    /// <returns><see cref="DynamicAccessor"/></returns>
    public static IDynamicAccessor GetDynamicAccessor(Type type)
    {
        if (!_DynamicAccessors.Contains(type))
            _DynamicAccessors.Add(type,new DynamicAccessor(type));

        return _DynamicAccessors[type];
    }
}
