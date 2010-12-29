Imports System.ComponentModel

'#-------------------
'# File rewritten in vb.net from its original code in C#, of seesharper
'# (http://www.codeproject.com/Members/seesharper) as available in: 
'#    http://www.codeproject.com/KB/cs/ObjectBindingSource.aspx
'# You can also download it from: 
'#    http://code.google.com/p/object-binding-source/downloads/list
'#
'# The modifications made by Daniel Prado Velasco (<dpradov@gmail.com>) are licensed under MIT License
'# included in the file LICENSE.txt, with the prevalence of the restrictions that the original license
'# could impose.
'# Changes to the original class includes:
'#  - New event: CreatingObject, raised from the method 'GetNestedObjectInstance', that has been changed from 
'#    <private share> to <protected friend>
'#  - Added new method: GetNestedObjectsInstances
'#
'# -----------------
'# URLs:
'#  http://code.google.com/p/object-binding-source/



''' <summary>
''' Implements a <see cref="TypeDescriptor"/> describing a nested/custom property.
''' </summary>
Public Class CustomPropertyDescriptor
    Inherits PropertyDescriptor

    Public Event CreatingObject(ByVal Sender As Object, ByVal ObjectType As Type, ByRef Obj As Object)

#Region "Member Variables"
    Private _autoCreateObjects As Boolean
    Private ReadOnly _originalPropertyDescriptor As PropertyDescriptor
    Private ReadOnly _propertyPath As String
#End Region

#Region "Constructors"

    ''' <summary>
    ''' Initializes a new instance of the CustomPropertyDescriptor class.
    ''' </summary>
    ''' <param name="propertyPath"></param>
    ''' <param name="originalPropertyDescriptor"></param>
    ''' <param name="attrs"></param>
    ''' <param name="autoCreateObjects"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal propertyPath As String, ByVal originalPropertyDescriptor As PropertyDescriptor, ByVal attrs As Attribute(), ByVal autoCreateObjects As Boolean)
        MyBase.New(propertyPath.Replace(".", "_"), attrs)

        _propertyPath = propertyPath
        _originalPropertyDescriptor = originalPropertyDescriptor
        _autoCreateObjects = autoCreateObjects
    End Sub

#End Region

    Public Overrides Function CanResetValue(ByVal component As Object) As Boolean        
        Dim instance As Object = GetNestedObjectInstance(component, _propertyPath, False)
        If instance IsNot Nothing Then
            Return _originalPropertyDescriptor.CanResetValue(instance)
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Gets the type of the component this property is bound to.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property ComponentType() As Type
        Get
            Return _originalPropertyDescriptor.ComponentType
        End Get
    End Property

    ''' <summary>
    ''' Gets the current value of the property on a component
    ''' </summary>
    ''' <param name="component">The component with the property for which to retrieve the value.</param>
    ''' <returns>The value of a property for a given component.</returns>
    Public Overrides Function GetValue(ByVal component As Object) As Object
        Dim instance As Object = GetNestedObjectInstance(component, _propertyPath, False)
        If instance IsNot Nothing Then
            Return DynamicAccessorFactory.GetDynamicAccessor(instance.GetType).GetPropertyValue(instance, _originalPropertyDescriptor.Name)
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Sets the value of the component to a different value.
    ''' </summary>
    ''' <param name="component">The component with the property value that is to be set.</param>
    ''' <param name="value">The new value.</param>
    Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
        Dim instance As Object = GetNestedObjectInstance(component, _propertyPath, _autoCreateObjects)
        If instance IsNot Nothing Then
            DynamicAccessorFactory.GetDynamicAccessor(instance.GetType).SetPropertyValue(instance, _originalPropertyDescriptor.Name, value)
        End If
    End Sub


    Protected Friend Function GetNestedObjectInstance(ByVal component As Object, ByVal propertyPath As String, ByVal autoCreate As Boolean) As Object
        Dim propertyName As String

        If propertyPath.Contains(".") Then
            propertyName = propertyPath.Substring(0, propertyPath.IndexOf("."))
        Else
            Return component
        End If

        Dim dynamicAccessor As IDynamicAccessor = DynamicAccessorFactory.GetDynamicAccessor(component.GetType)
        Dim value As Object = dynamicAccessor.GetPropertyValue(component, propertyName)
        If value Is Nothing Then
            If Not autoCreate Then
                Return value
            Else
                Dim descriptor As PropertyDescriptor = ReflectionHelper.GetPropertyDescriptorFromPath(component.GetType(), propertyName)
                RaiseEvent CreatingObject(Me, descriptor.PropertyType, value)
                If value Is Nothing Then
                    value = Activator.CreateInstance(descriptor.PropertyType)
                End If
                dynamicAccessor.SetPropertyValue(component, propertyName, value)
            End If
        End If
        Return GetNestedObjectInstance(value, propertyPath.Substring((propertyPath.IndexOf(".") + 1)), autoCreate)
    End Function


    ''' <summary>
    ''' Gets a value indicating whether this property is read-only.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property IsReadOnly() As Boolean
        Get
            Return _originalPropertyDescriptor.IsReadOnly
        End Get
    End Property

    ''' <summary>
    ''' Gets the type of the property.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property PropertyType() As Type
        Get
            Return _originalPropertyDescriptor.PropertyType
        End Get
    End Property


    Public Overrides Sub ResetValue(ByVal component As Object)
        Dim instance As Object = GetNestedObjectInstance(component, _propertyPath, False)
        If instance IsNot Nothing Then
            _originalPropertyDescriptor.ResetValue(instance)
        End If
    End Sub

    ''' <summary>
    ''' Determines a value indicating whether the value of this property needs to be persisted. 
    ''' </summary>
    ''' <param name="component">The component with the property to be examined for persistence</param>
    ''' <returns>false</returns>
    Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
        Return False
    End Function


    ''' <summary>
    ''' It returns the relation of objects associated with each of the properties included in the specified path
    ''' for the root object provided ('component' parameter), not including the ends.
    ''' The order of the returned objects will be the same as that of the properties in the path provided.
    ''' The first of these objects corresponds to the first property on the path over the root object (component),
    ''' the second object corresponds to the resulting of applying the second property to the previous object, 
    ''' and so on.
    ''' </summary>
    ''' <param name="component"></param>
    ''' <param name="propertyPath"></param>
    ''' <remarks>
    ''' The list of objects may not reach the last property of the specified path if any of these returns null.
    ''' </remarks>
    Protected Friend Shared Sub GetNestedObjectsInstances(ByVal component As Object, ByVal propertyPath As String, ByVal nestedObjects As List(Of Object))
        Dim propertyName As String

        If nestedObjects Is Nothing Then
            nestedObjects = New List(Of Object)
        End If

        If propertyPath.Contains(".") Then
            propertyName = propertyPath.Substring(0, propertyPath.IndexOf("."))

            Dim dynamicAccessor As IDynamicAccessor = DynamicAccessorFactory.GetDynamicAccessor(component.GetType)
            Dim value As Object = dynamicAccessor.GetPropertyValue(component, propertyName)
            nestedObjects.Add(value)
            If value IsNot Nothing Then
                GetNestedObjectsInstances(value, propertyPath.Substring((propertyPath.IndexOf(".") + 1)), nestedObjects)
            End If
        End If

    End Sub

End Class

