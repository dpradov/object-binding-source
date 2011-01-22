using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

//# Corresponds to code released by seesharper (http://www.codeproject.com/Members/seesharper) 
//# and licensed under The Code Project Open License (CPOL) (http://www.codeproject.com/info/cpol10.aspx)
//# The original work of seesharper is available in: 
//#    http://www.codeproject.com/KB/cs/ObjectBindingSource.aspx
//# You can also download it from: 
//#    http://code.google.com/p/object-binding-source/downloads/list
//#
//# Changes to the original class includes:
//#  - Added new method: GetPropertyTypesFromPath
//#
//# -----------------
//# URLs:
//#  http://code.google.com/p/object-binding-source/


/// <summary>
/// Contains helper functions related to reflection
/// </summary>
public static class ReflectionHelper
{
    /// <summary>
    /// Searches for a property in the given property path
    /// </summary>
    /// <param name="rootType">The root/starting point to start searching</param>
    /// <param name="propertyPath">The path to the property. Ex Customer.Address.City</param>
    /// <returns>A <see cref="PropertyInfo"/> describing the property in the property path.</returns>
    public static PropertyInfo GetPropertyFromPath(Type rootType,string propertyPath)
    {
        if (rootType == null)
            throw new ArgumentNullException("rootType");
        
        Type propertyType = rootType;
        PropertyInfo propertyInfo = null;
        string[] pathElements = propertyPath.Split(new char[1] { '.' });
        for (int i = 0; i < pathElements.Length; i++)
        {
            propertyInfo = propertyType.GetProperty(pathElements[i], BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null)
            {
                propertyType = propertyInfo.PropertyType;
            }
            else
            {
                throw new ArgumentOutOfRangeException("propertyPath",propertyPath,"Invalid property path");
            }
        }
        return propertyInfo;
    }


    public static PropertyDescriptor GetPropertyDescriptorFromPath(Type rootType, string propertyPath)
    {
        string propertyName;
        bool lastProperty = false;
        if (rootType == null)
            throw new ArgumentNullException("rootType");

        if (propertyPath.Contains("."))
            propertyName = propertyPath.Substring(0, propertyPath.IndexOf("."));
        else
        {
            propertyName = propertyPath;
            lastProperty = true;
        }

        PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(rootType)[propertyName];
        if (propertyDescriptor == null)
            throw new ArgumentOutOfRangeException("propertyPath", propertyPath, string.Format("Invalid property path for type '{0}' ",rootType.Name));


        if (!lastProperty)
            return GetPropertyDescriptorFromPath(propertyDescriptor.PropertyType, propertyPath.Substring(propertyPath.IndexOf(".") + 1));
        else
            return propertyDescriptor;               
        
    }


    public static void GetPropertyTypesFromPath(Type rootType, string propertyPath, List<Type> propertyTypes)
    {
        string propertyName;
        bool lastProperty = false;
        if (rootType == null)
            throw new ArgumentNullException("rootType");

        if (propertyTypes == null)
            propertyTypes = new List<Type>();

        if (propertyPath.Contains("."))
            propertyName = propertyPath.Substring(0, propertyPath.IndexOf("."));
        else
        {
            propertyName = propertyPath;
            lastProperty = true;
        }

        PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(rootType)[propertyName];
        if (propertyDescriptor == null)
            throw new ArgumentOutOfRangeException("propertyPath", propertyPath, string.Format("Invalid property path for type '{0}' ", rootType.Name));

        propertyTypes.Add(propertyDescriptor.PropertyType);

        if (!lastProperty)
            GetPropertyTypesFromPath(propertyDescriptor.PropertyType, propertyPath.Substring(propertyPath.IndexOf(".") + 1), propertyTypes);
    }
  
}
