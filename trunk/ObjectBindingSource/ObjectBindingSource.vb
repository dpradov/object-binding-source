Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Design

'# ObjectBindingSource is based in code released by seesharper (http://www.codeproject.com/Members/seesharper) 
'# and licensed under The Code Project Open License (CPOL) (http://www.codeproject.com/info/cpol10.aspx)
'# The original work of seesharper is available in: 
'#    http://www.codeproject.com/KB/cs/ObjectBindingSource.aspx
'# You can also download it from: 
'#    http://code.google.com/p/object-binding-source/downloads/list
'#
'# The modifications made by Daniel Prado Velasco (<dpradov@gmail.com>) are licensed under MIT License,
'# reproduced below (with the prevalence of the restrictions that the original license (CPOL) could impose).
'# Note that all the original work in ObjectBindingSource was written in C#.
'# The main changes to the original class (ObjectBindingSource) are described in: 
'#    http://code.google.com/p/object-binding-source/
'#
'# MIT License:
'#  ======================================================================
'#  Copyright (C)  2010  Daniel Prado Velasco' <dpradov@gmail.com>
'#
'#                         All Rights Reserved
'#
'# Permission is hereby granted, free of charge, to any person
'# obtaining a copy of this software and associated documentation
'# files (the "Software"), to deal in the Software without
'# restriction, including without limitation the rights to use,
'# copy, modify, merge, publish, distribute, sublicense, and/or sell
'# copies of the Software, and to permit persons to whom the
'# Software is furnished to do so, subject to the following
'# conditions:
'#
'# The above copyright notice and this permission notice shall be
'# included in all copies or substantial portions of the Software.
'#
'# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
'# EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
'# OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
'# NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
'# HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
'# WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
'# FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
'# OTHER DEALINGS IN THE SOFTWARE.
'#
'#  ======================================================================
'# URLs:
'#  http://code.google.com/p/object-binding-source/


''' <summary>
''' Extends the standard <see cref="BindingSource"/> to provide
''' support for nested property accessors.
''' </summary>
<ToolboxBitmap(GetType(ObjectBindingSource))> _
Public Class ObjectBindingSource
    Inherits BindingSource

    Public Event CreatingObject(ByVal Sender As Object, ByVal ObjectType As Type, ByRef Obj As Object)

#Region "Private Member Variables"

    Private _BindableNestedProperties As String()
    Private ReadOnly _propertyDescriptors As New List(Of PropertyDescriptor)

    Private _createProperties As Boolean = True
    Private _autoCreateObjects As Boolean = False

    Friend _itemType As Type
    Private _currentItem As Object = Nothing   ' To find, from OnListChanged, which is the deleted object
    
    ' _TypesInNestedProperties:
    ' Collect an array of object types for each nested property. Each array includes the types of each of the 
    ' properties of the path that describes how to access the value of the nested property.
    ' In this array of types it is not included the type corresponding to the object wich is the DataSource,
    ' unless shown elsewhere in that path.
    Private _TypesInNestedProperties As Type()() = Nothing

    
    ' _NestedObjects:
    ' A structure is reserved for each row of the list for the DataSource. That structure includes the object 
    ' of the DataSource associated to that row together with an array of objects for each nested property. 
    ' Each of those array of objects (associated to a nested property in a row) contains the hierarchy of 
    ' objects from the object associated with the list that is bound (but not including) until the last 
    ' property for the path.
    ' Examples:
    ' - If row 0 corresponds to the object Order1, for nested property "Customer.Name" we could have the following hierarchy
    '   of objects: Order1.Customer1.String1.   The only object stored in this case would be: Customer1
    ' - If the property was "Customer.BillingAddress.City" and the underlying object hierarchy to be:
    '   Order1.Customer1.BillingAddress1.String1 then the objects stored would be: 
    '   Customer1 and BillingAddress1, in that order.    
    Private _NestedObjects As New List(Of ObjectsInRow)

    ' We need also to listen to the events PropertyChanged raised for the DataSource's objects itself.
    ' and to know which they are, to unsubscribed when necessary
    Private _DataSourceObjects As New List(Of Object)

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Creates a new instance of the <see cref="ObjectBindingSource"/> component class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        AddHandler DirectCast(Me, ISupportInitializeNotification).Initialized, AddressOf ObjectBindingSource_Initialized
    End Sub

    ''' <summary>
    ''' Creates a new instance of the <see cref="ObjectBindingSource"/> component class.
    ''' </summary>
    ''' <param name="container"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal container As IContainer)
        container.Add(Me)
        InitializeComponent()
        AddHandler DirectCast(Me, ISupportInitializeNotification).Initialized, AddressOf ObjectBindingSource_Initialized
    End Sub

#End Region

    Private Function UsingNestedProperties() As Boolean
        Return (_BindableNestedProperties IsNot Nothing) AndAlso (_BindableNestedProperties.Length > 0)
    End Function

    Private Sub CreatePropertyDescriptors()
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, "CreatePropertyDescriptors - Inicio"))

        ' Clear auxiliar structures, including the previous list of property descriptors
        ClearAuxiliarStructures()

        If MyBase.DataSource IsNot Nothing Then
            ' Get the type of object that this bindingsource is bound to            
            _itemType = ListBindingHelper.GetListItemType(MyBase.DataSource, MyBase.DataMember)

            If _itemType IsNot GetType(Object) Then

                ' Add original properties
                Dim originalProperties = TypeDescriptor.GetProperties(_itemType)
                For Each propertyDescriptor As PropertyDescriptor In originalProperties
                    If propertyDescriptor.IsBrowsable Then
                        Dim attributes As Attribute() = New Attribute(propertyDescriptor.Attributes.Count - 1) {}
                        propertyDescriptor.Attributes.CopyTo(attributes, 0)
                        _propertyDescriptors.Add(New CustomPropertyDescriptor(propertyDescriptor.Name, ReflectionHelper.GetPropertyDescriptorFromPath(_itemType, propertyDescriptor.Name), attributes, False))
                    End If
                Next

                ' Check to see if there are any bindable properties defined.
                If UsingNestedProperties() Then
                    For Each nestedPropertyName In _BindableNestedProperties
                        Try
                            ' Get the original propertydescriptor based on the property path in bindableProperty
                            Dim propertyDescriptor As PropertyDescriptor = ReflectionHelper.GetPropertyDescriptorFromPath(_itemType, nestedPropertyName)
                            ' Create a attribute array and make room for one more attribute 
                            Dim attributes As Attribute() = New Attribute((propertyDescriptor.Attributes.Count + 1) - 1) {}
                            ' Copy the original attributes to the custom descriptor
                            propertyDescriptor.Attributes.CopyTo(attributes, 0)
                            ' Create a new attrute preserving information about the original property.
                            attributes((attributes.Length - 1)) = New CustomPropertyAttribute(_itemType, nestedPropertyName, propertyDescriptor)
                            ' Finally add the new custom property descriptor to the list of property descriptors
                            Dim custPropertyDescriptor = New CustomPropertyDescriptor(nestedPropertyName, propertyDescriptor, attributes, _autoCreateObjects)
                            _propertyDescriptors.Add(custPropertyDescriptor)

                            If _autoCreateObjects Then
                                AddHandler custPropertyDescriptor.CreatingObject, AddressOf CustomPropertyDescriptor_CreatingObject
                            End If

                        Catch ex As Exception
                            ' Something is wrong in the property path the property
                            Dim msg As String = String.Format("La propiedad '{0}' no es reconocida: {1}", nestedPropertyName, ex.Message)
                            If Not MyBase.DesignMode Then
                                Console.WriteLine(msg)

                            ElseIf MyBase.DataMember IsNot Nothing Then
                                MessageBox.Show(msg)
                            End If
                        End Try
                    Next

                    PopulateTypesInNestedProperties()
                    PopulateNestedObjects()

                    If DBG.MaxNivelDepuracion > 0 Then
                        Show_TypesInNestedProperties()
                        Show_NestedObjects()
                    End If
                End If

            End If    ' If _itemType IsNot GetType(Object) Then

        End If

        _createProperties = False
        MyBase.ResetBindings(True)

        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, " - Fin", 2))
    End Sub

    Private Sub CustomPropertyDescriptor_CreatingObject(ByVal Sender As Object, ByVal ObjectType As Type, ByRef Obj As Object)
        RaiseEvent CreatingObject(Me, ObjectType, Obj)
    End Sub

    Private Sub ObjectBindingSource_Initialized(ByVal sender As Object, ByVal e As EventArgs)
        _createProperties = True
    End Sub

    Public Overrides Function GetItemProperties(ByVal listAccessors As PropertyDescriptor()) As PropertyDescriptorCollection
        If _createProperties Then
            Me.CreatePropertyDescriptors()
        End If
        If (_propertyDescriptors.Count > 0) Then
            Return New PropertyDescriptorCollection(_propertyDescriptors.ToArray)
        End If
        Return MyBase.GetItemProperties(listAccessors)
    End Function


    ''' <summary>
    ''' Raises the <see cref="BindingSource.DataMemberChanged"/> event.
    ''' </summary>
    ''' <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
    Protected Overrides Sub OnDataMemberChanged(ByVal e As EventArgs)
        MyBase.OnDataMemberChanged(e)
        _createProperties = True
        MyBase.ResetBindings(True)
    End Sub

    ''' <summary>
    ''' Raises the <see cref="BindingSource.DataSourceChanged"/> event.
    ''' </summary>
    ''' <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
    Protected Overrides Sub OnDataSourceChanged(ByVal e As EventArgs)
        If MyBase.DataSource Is Nothing Then
            ClearAuxiliarStructures()
        End If
        MyBase.OnDataSourceChanged(e)
        _createProperties = True
        MyBase.ResetBindings(True)
    End Sub


    ''' <summary>
    ''' Indicates if objects should be automatically created when setting property path values.
    ''' </summary>
    <Category("Data")> _
    Public Property AutoCreateObjects() As Boolean
        Get
            Return _autoCreateObjects
        End Get
        Set(ByVal value As Boolean)
            _autoCreateObjects = value
        End Set
    End Property

    ''' <summary>
    ''' Gets a list containing the bindable properties.
    ''' </summary>
    <Category("Data")> _
    <Editor(GetType(NestedPropertiesEditor), GetType(UITypeEditor))> _
    Public Property BindableNestedProperties() As String()
        Get
            Return _BindableNestedProperties
        End Get
        Set(ByVal value As String())
            _BindableNestedProperties = value
            _createProperties = True
        End Set
    End Property



    Private Structure ObjectsInRow
        Public ObjDataSource As Object
        Public ObjInProperties As ObjectsInNestedProperty()
    End Structure

    Private Structure ObjectsInNestedProperty
        Public Objects As Object()
    End Structure

    Private Structure PropertyAffected
        Public Row As Integer
        Public iNestedProperty As Integer
        Public Position As Integer

        Sub New(ByVal row As Integer, ByVal iNestedProperty As Integer, ByVal nestedLevel As Integer)
            Me.Row = row
            Me.iNestedProperty = iNestedProperty
            Me.Position = nestedLevel
        End Sub
    End Structure


    Private Sub PopulateTypesInNestedProperties()
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("PopulateTypesInNestedProperties [{0} - {1}] - Inicio", MyBase.DataSource, MyBase.DataMember)))

        ' Redimension this structure depending on the number of nested binding properties defined
        ReDim _TypesInNestedProperties(_BindableNestedProperties.Length - 1)

        Dim i As Integer = 0
        Dim propertyTypes As New List(Of Type)

        For Each nestedPropertyName In _BindableNestedProperties
            Try
                propertyTypes.Clear()
                ReflectionHelper.GetPropertyTypesFromPath(_itemType, nestedPropertyName, propertyTypes)
                _TypesInNestedProperties(i) = propertyTypes.ToArray

            Catch ex As Exception
                ' Something is wrong in the property path the property
                ' Esto ya habrá sido identificado desde CreatePropertyDescriptors 
                _TypesInNestedProperties(i) = New Type() {}
            End Try

            i += 1
        Next
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("- Fin"), 3))
    End Sub

    Private Sub PopulateNestedObjects()
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("PopulateNestedObjects [{0} - {1}] - Inicio", MyBase.DataSource, MyBase.DataMember)))

        For Each objDataSource In MyBase.List
            PopulateNestedObjectsInRow(objDataSource)
        Next

        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("- Fin"), 3))
    End Sub

    Private Sub PopulateNestedObjectsInRow(ByVal objDataSource As Object)

        ' Redimension this internal structure depending on the number of nested binding properties defined
        Dim ObjectsInNestedProperties(_BindableNestedProperties.Length - 1) As ObjectsInNestedProperty

        Dim i = 0
        For Each nestedPropertyName In _BindableNestedProperties
            PopulateObjectsInNestedPropertyForRow(objDataSource, nestedPropertyName, ObjectsInNestedProperties(i))
            i += 1
        Next

        Dim objInNestedProperties As New ObjectsInRow
        objInNestedProperties.ObjDataSource = objDataSource
        objInNestedProperties.ObjInProperties = ObjectsInNestedProperties

        _NestedObjects.Add(objInNestedProperties)

        ' También nos hace falta escuchar los eventos PropertyChanged disparados por los propios objetos del DataSource
        If GetType(INotifyPropertyChanged).IsInstanceOfType(objDataSource) Then
            _DataSourceObjects.Add(objDataSource)
            RemoveHandler DirectCast(objDataSource, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
            AddHandler DirectCast(objDataSource, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
            DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Escuchando PropertyChanged en: {0}  [{1} - {2}]", objDataSource, MyBase.DataSource, MyBase.DataMember)))
        End If
    End Sub

    Private Sub ClearAuxiliarStructures()
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("ClearStructuresNestedObjects [{0} - {1}] - Inicio", MyBase.DataSource, MyBase.DataMember)))

        If _propertyDescriptors IsNot Nothing Then
            For Each prop In _propertyDescriptors
                If TypeOf prop Is CustomPropertyDescriptor Then
                    RemoveHandler CType(prop, CustomPropertyDescriptor).CreatingObject, AddressOf CustomPropertyDescriptor_CreatingObject
                End If
            Next
            _propertyDescriptors.Clear()
        End If


        If _NestedObjects IsNot Nothing Then
            For fila = 0 To _NestedObjects.Count - 1
                ClearNestedObjectsInRow(fila)
            Next
            _NestedObjects.Clear()
        End If

        If _DataSourceObjects IsNot Nothing Then
            For Each obj In _DataSourceObjects
                DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("RemoveHandler sobre: {0}", obj)))
                RemoveHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
            Next
            _DataSourceObjects.Clear()
        End If

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("- Fin"), 3))
    End Sub

    Private Sub ClearNestedObjectsInRow(ByVal fila As Integer, Optional ByVal objDataSource As Object = Nothing)
        For iNestedProperty = 0 To _BindableNestedProperties.Length - 1

            For Each obj In _NestedObjects(fila).ObjInProperties(iNestedProperty).Objects
                If GetType(INotifyPropertyChanged).IsInstanceOfType(obj) Then
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("RemoveHandler sobre: {0}", obj)))
                    RemoveHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                End If
            Next
        Next

        If objDataSource IsNot Nothing Then
            _DataSourceObjects.Remove(objDataSource)
            DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("RemoveHandler sobre: {0}", objDataSource)))
            RemoveHandler DirectCast(objDataSource, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
        End If

    End Sub


    Private Sub Show_NestedObjects()
        DBG_SaltoLinea(0)
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("Contenido de _NestedObjects [{0} - {1}]", MyBase.DataSource, MyBase.DataMember)))
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("----------------------------")))
        If _NestedObjects Is Nothing Then Exit Sub

        For fila = 0 To _NestedObjects.Count - 1
            DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("FILA: {0}", fila)))
            For iNestedProperty = 0 To _BindableNestedProperties.Length - 1
                DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("Prop: '{0}'", _BindableNestedProperties(iNestedProperty)), 2))
                For Each obj In _NestedObjects(fila).ObjInProperties(iNestedProperty).Objects
                    DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("{0}", obj), 4))
                Next

            Next

        Next
        DBG_SaltoLinea(0)
    End Sub


    Private Sub Show_TypesInNestedProperties()
        DBG_SaltoLinea(0)
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("Contenido de TypesInNestedProperties [{0} - {1}]", MyBase.DataSource, MyBase.DataMember)))
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("-------------------------------------")))

        For iNestedProperty = 0 To _BindableNestedProperties.Length - 1
            DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("Prop: '{0}'", _BindableNestedProperties(iNestedProperty))))

            Dim propertyTypes = _TypesInNestedProperties(iNestedProperty)
            For i = 0 To propertyTypes.Length - 1
                DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("{0}", propertyTypes(i)), 2))
            Next
        Next
        DBG_SaltoLinea(0)
    End Sub


    Private Sub PopulateObjectsInNestedPropertyForRow(ByVal objDataSource As Object, ByVal nestedPropertyName As String, ByRef objsInNestedProperty As ObjectsInNestedProperty)

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format(" - objDataSource:{0} prop:{1}", objDataSource, nestedPropertyName), 2))

        Dim nestedObjectsInProperty As New List(Of Object)

        Try
            nestedObjectsInProperty.Clear()
            CustomPropertyDescriptor.GetNestedObjectsInstances(objDataSource, nestedPropertyName, nestedObjectsInProperty)
            objsInNestedProperty.Objects = nestedObjectsInProperty.ToArray

            For Each obj In nestedObjectsInProperty
                If GetType(INotifyPropertyChanged).IsInstanceOfType(obj) Then
                    ' TODO: ¿Es más óptimo mantener en una lista o estructura similar los objetos sobre los que ya estamos subscritos y realizar el AddHandler únicamente sobre lo que no lo están ya?
                    RemoveHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                    AddHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Escuchando PropertyChanged en: {0}  [{1} - {2}]", obj, MyBase.DataSource, MyBase.DataMember)))
                End If
            Next

        Catch ex As Exception
            ' Something is wrong in the property path the property
            ' Esto ya habrá sido identificado desde CreatePropertyDescriptors 
            objsInNestedProperty.Objects = New Object() {}
        End Try

    End Sub



    ''' <summary>
    ''' A partir de un objeto sobre el que se nos ha notificado de la modificación de una de sus propiedades
    ''' identificar las propiedades anidadas que pueden verse afectadas. Éstas se devuelven a través de una relación
    ''' de números que corresponden al orden de éstas en _BindingNestedProperties
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ManagePropertiesAffected(ByVal Obj As Object, ByVal PropertyName As String, ByVal NewValue As Object) As Boolean
        Dim iNestedProperty As Integer
        Dim PropertiesAffected As New List(Of PropertyAffected)
        Dim positions As New List(Of Integer)

        ' Buscamos el objeto que ha notificado el cambio sobre los objetos anidados registrados
        For iNestedProperty = 0 To _BindableNestedProperties.Length - 1
            If CheckCanAppearInNestedProperty(Obj, iNestedProperty, PropertyName, positions) Then
                IdentifyPropertiesAffected(Obj, iNestedProperty, positions, PropertiesAffected)
                positions.Clear()
            End If
        Next


        If PropertiesAffected.Count > 0 Then

            Dim PossibleObjectsToDiscard As New List(Of Object)  ' Por no estar tal vez ya en uso

            ' Realizar la subscripción necesaria para los nuevos objetos que surjan tras estos cambios, así como tomar nota de aquellos sobre los
            ' que pueda anularse la misma
            For Each propAfect In PropertiesAffected
                ManagePropertyAffectedInRow(propAfect.Row, propAfect.iNestedProperty, propAfect.Position, PossibleObjectsToDiscard, NewValue)
            Next

            ' Dejamos de escuchar el evento PropertyChanged en aquellos objetos anidados de propiedades afectadas, que dependan de propiedades que hayan
            ' cambiado, siempre y cuando esos objetos no estén en uso desde otras propiedades anidadadas.
            For Each ObjDescartar In PossibleObjectsToDiscard
                If GetType(INotifyPropertyChanged).IsInstanceOfType(ObjDescartar) AndAlso Not IsNestedObjectInUse(ObjDescartar) Then
                    DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("RemoveHandler sobre: {0}", ObjDescartar)))
                    RemoveHandler DirectCast(ObjDescartar, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                End If
            Next

            ' Comunicamos finalmente la modificación de las propiedades afectadas
            ' Las propiedades están identificadas con un índice en base 0 dentro de la relación de propiedades anidadas. Para localizar correctamente el descriptor
            ' tenemos que sumarle el número de propiedades no anidadas
            Dim numPropNotNested = _propertyDescriptors.Count - _BindableNestedProperties.Length
            Dim CurrentItemChanged As Boolean = False
            For Each propAfect In PropertiesAffected
                DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("OnListChanged: Row:{0} Prop:'{1}'", propAfect.Row, _propertyDescriptors(numPropNotNested + propAfect.iNestedProperty).Name)))
                MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, propAfect.Row, _propertyDescriptors(numPropNotNested + propAfect.iNestedProperty)))
                If propAfect.Row = MyBase.Position Then
                    CurrentItemChanged = True
                End If
            Next

            Return CurrentItemChanged
        End If

    End Function

    Private Sub IdentifyPropertiesAffected(ByVal ObjModif As Object, _
                                                        ByVal iNestedProperty As Integer, ByVal positions As List(Of Integer), _
                                                        ByVal PropertiesAffected As List(Of PropertyAffected))
        For row = 0 To _NestedObjects.Count - 1
            Dim objsProperty As ObjectsInNestedProperty = _NestedObjects(row).ObjInProperties(iNestedProperty)
            Dim numObjects = objsProperty.Objects.Length
            Dim objNested As Object

            For Each i In positions
                If i = -1 Then
                    objNested = MyBase.List(row)
                Else
                    If numObjects - 1 < i Then Exit For
                    objNested = objsProperty.Objects(i)
                End If

                If objNested Is ObjModif Then
                    PropertiesAffected.Add(New PropertyAffected(row, iNestedProperty, i))
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Propiedad afectada: fila: {0} Prop: '{1}'", row, _BindableNestedProperties(iNestedProperty)), 3))
                End If

            Next

        Next

    End Sub


    ''' <summary>
    ''' Toma nota de los posibles objetos a descartar (en la escucha de PropertyChanged) y actualiza la estructura _NestedObjects con los nuevos valores
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="iNestedProperty"></param>
    ''' <param name="position">Posicion dentro de la jerarquía de propiedades en una propiedad anidada en donde se encuentra el objeto que ha notificado la modificación</param>
    ''' <param name="PossibleObjectsToDiscard"></param>
    ''' <param name="NewValue"></param>
    ''' <remarks></remarks>
    Private Sub ManagePropertyAffectedInRow(ByVal row As Integer, ByVal iNestedProperty As Integer, ByVal position As Integer, ByVal PossibleObjectsToDiscard As List(Of Object), ByVal NewValue As Object)

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("ManagePropertyAffectedInRow. Row:{0} Position:{1}", row, position)))

        ' Si el objeto que notifica la modificación de la propiedad no es el último en la jerarquía de objetos
        ' anidados asociados a la propiedad, esto es, la propiedad modificada no es la que se está mostrando directamente
        ' en la propiedad, puede ser necesario realizar más acciones, en el caso de que ese objeto y los que le sigan en el path
        ' ya no estén en uso. 
        ' En este función nos limitaremos a añadir esos objetos a la lista PosiblesObjetosADescartar

        ' Ej: Si la propiedad anidada es "Cliente.Nombre" ([Order].Cliente.Nombre)  -> 2 posiciones
        ' => _TypesInNestedProperties(iNestedProperty) guardará los tipos de 'Cliente' y 'Nombre'. 
        ' El objeto final (que dispara eventos) será el correspondiente a Cliente
        ' El número de posiciones se mide en TypesInNestedProperties. Ahora, en _NestedObjects, en la estructura correspondiente a la fila y propiedad anidada analizada
        ' puede haber como máximo un número menos de objetos pues el último elemento de la cadena no se incluye pues no nos sirve ('Nombre' en este ejemplo)
        Dim numMaxObjects = _TypesInNestedProperties(iNestedProperty).Length - 1
        Dim IsFinalObject As Boolean = (position = numMaxObjects - 1)    ' La posición puede ser 0 (o incluso -1 si el objeto modificado es el correspondiente al datasource)

        If Not IsFinalObject Then
            DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("No es objeto Final"), 2))
            Dim objsProp As ObjectsInNestedProperty = _NestedObjects(row).ObjInProperties(iNestedProperty)


            ' Recorremos los objetos anidados almacenados en la propiedad analizada, comenzando por el primer objeto afectado, esto es, por el siguiente a aquel que
            ' ha generado la notificación.
            ' Ej: [Order1.] "Cliente.Nombre"  Si posicion = -1 => el objeto modificado es Order1 sobre su propiedad Cliente
            For i = position + 1 To objsProp.Objects.Length - 1
                Dim obj = objsProp.Objects(i)
                If Not PossibleObjectsToDiscard.Contains(obj) Then PossibleObjectsToDiscard.Add(obj)
                DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("Posible objeto a descartar: {0}", obj), 2))
            Next

            ' Actualizamos nuestra estructura con los nuevos objetos para esta propiedad
            If position = numMaxObjects - 2 Then   ' -1 correspondería al 
                ' Si el objeto modificado es el inmediatamente anterior al objeto final tan sólo tendremos que indicar cuál es el nuevo objeto final, que tendremos en NewValue
                _NestedObjects(row).ObjInProperties(iNestedProperty).Objects(position + 1) = NewValue
                If GetType(INotifyPropertyChanged).IsInstanceOfType(NewValue) Then
                    RemoveHandler DirectCast(NewValue, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                    AddHandler DirectCast(NewValue, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Escuchando PropertyChanged en: {0}  [{1} - {2}]", NewValue, MyBase.DataSource, MyBase.DataMember)))
                End If
            Else
                PopulateObjectsInNestedPropertyForRow(_NestedObjects(row).ObjDataSource, _BindableNestedProperties(iNestedProperty), _NestedObjects(row).ObjInProperties(iNestedProperty))
            End If

        Else
            ' Si el objeto es final entonces no es necesario modificar la estructura de objetos en esta propiedad anidada
        End If

    End Sub



    Private Function IsNestedObjectInUse(ByVal Obj As Object) As Boolean

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("IsNestedObjectInUse: {0}", Obj)))

        ' Para no tener que mirar dentro de la jerarquía de objetos 'nested' correspondiente a cada propiedad
        ' excluiremos aquellas propiedades en las que ninguno de esos objetos sea de un tipo compatible
        Dim posiciones As New List(Of Integer)

        For iNestedProperty = 0 To _BindableNestedProperties.Length - 1
            posiciones.Clear()
            If Not CheckCanAppearInNestedProperty(Obj, iNestedProperty, Nothing, posiciones) Then Continue For

            For fila = 0 To _NestedObjects.Count - 1
                Dim objsPropiedad As ObjectsInNestedProperty = _NestedObjects(fila).ObjInProperties(iNestedProperty)
                Dim numObjetos = objsPropiedad.Objects.Length

                ' Miramos en las únicas posiciones en las que podría aparecer
                For Each i In posiciones
                    If numObjetos - 1 < i Then Exit For
                    If objsPropiedad.Objects(i) Is Obj Then
                        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format(" IsNestedObjectInUse -> TRUE"), 2))
                        Return True
                    End If
                Next

            Next
        Next
        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format(" IsNestedObjectInUse -> FALSE"), 2))
        Return False

    End Function

    ''' <summary>
    ''' Comprobar si el objeto puede aparecer en la propiedad anidada indicada, atendiendo al tipo del mismo y de la propiedad sobre la que se ha notificado
    ''' el cambio. Devuelve la lista de posiciones en la propiedad anidada, en donde podría aparecer
    ''' </summary>
    ''' <param name="Obj"></param>
    ''' <param name="iNestedProperty"></param>
    ''' <param name="PropertyModifiedName"></param>
    ''' <param name="Posiciones"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckCanAppearInNestedProperty(ByVal Obj As Object, _
                                                       ByVal iNestedProperty As Integer, ByVal PropertyModifiedName As String, _
                                                       ByVal Posiciones As IList(Of Integer)) As Boolean

        DBG.Foo(DBG_ChkNivel(2) AndAlso DBG.Log(2, String.Format("CheckPuedeAparecerEnNestedObjects: Obj={0} Prop='{1}'", Obj, _BindableNestedProperties(iNestedProperty)), 2))


        Dim result As Boolean = False
        Dim nestedProperty As String() = _BindableNestedProperties(iNestedProperty).Split("."c)
        Dim propertyTypes = _TypesInNestedProperties(iNestedProperty)


        If Obj.GetType Is _itemType Then
            DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Es un objeto del tipo del DataSource"), 3))
            If nestedProperty(0) = PropertyModifiedName Then
                Posiciones.Add(-1)
                result = True
            End If
        End If

        ' El tipo de la última propiedad del path realmente no nos interesa
        For i = 0 To propertyTypes.Length - 2
            If (PropertyModifiedName Is Nothing Or nestedProperty(i + 1) = PropertyModifiedName) AndAlso propertyTypes(i).IsInstanceOfType(Obj) Then
                Posiciones.Add(i)
                result = True
            End If
        Next

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Resultado: {0} posiciones", Posiciones.Count), 3))
        Return result
    End Function


    Protected Sub NestedObject_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        Try
            ' Son propiedades claramente incorrectas, porque no se permite ninguna con ese nombre. Pero las podemos recibir desde ClienteNucleo porque generándolas 
            ' podemos controlar si queremos lanzar el evento OnListChanged o no desde la clase BindingListEntidades(of T) (ver su método OnListChanged)
            ' OnListChanged 
            If e.PropertyName = "<<" Or e.PropertyName = ">>" Then Exit Sub

            Dim numNestedProperties As Integer
            Dim CurrentItemChanged As Boolean = False

            DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("NestedObject_PropertyChanged: {0}: Prop:{1} [{2} - {3}]", sender, e.PropertyName, MyBase.DataSource, MyBase.DataMember)))
            Dim NewValue As Object = DynamicAccessorFactory.GetDynamicAccessor(sender.GetType).GetPropertyValue(sender, e.PropertyName)
            DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("New Value: {0}", NewValue), 2))

            If UsingNestedProperties() Then
                numNestedProperties = _BindableNestedProperties.Length
                CurrentItemChanged = ManagePropertiesAffected(sender, e.PropertyName, NewValue)
            Else
                numNestedProperties = 0
            End If

            ' Aparte pueden verse afectadas aquellas propiedades no anidadas que estén mostrando el objeto que ha sido modificado (con la ayuda de
            ' un desplegable o similar)
            Dim numPropNoAnidadas = _propertyDescriptors.Count - numNestedProperties
            Dim DynamicAccessor As IDynamicAccessor = DynamicAccessorFactory.GetDynamicAccessor(_itemType)
            Dim objType As Type = sender.GetType

            For i = 0 To numPropNoAnidadas - 1
                Dim prop = _propertyDescriptors(i)
                If prop.PropertyType Is objType Then
                    For fila = 0 To MyBase.List.Count - 1
                        Dim valor = DynamicAccessor.GetPropertyValue(MyBase.List(fila), prop.Name)
                        If valor Is sender Then
                            DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("OnListChanged: fila:{0} prop:'{1}'", fila, prop.Name)))
                            MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, fila, prop))
                            If fila = MyBase.Position Then
                                CurrentItemChanged = True
                            End If
                        End If
                    Next
                End If
            Next

            ' O también si el objeto que notifica el cambio es del DataSource y la propiedad modificada está siendo mostrada 
            ' En nuestro ejemplo, hacemos por código: Order1.Customer = <cliente3>
            If objType Is _itemType AndAlso GetType(INotifyPropertyChanged).IsInstanceOfType(NewValue) Then
                Dim fila = MyBase.IndexOf(sender)
                For i = 0 To numPropNoAnidadas - 1
                    Dim prop = _propertyDescriptors(i)
                    Dim valor = DynamicAccessor.GetPropertyValue(MyBase.List(fila), prop.Name)
                    If valor Is NewValue Then
                        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("OnListChanged: fila:{0} prop:'{1}'", fila, prop.Name)))
                        MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, fila, prop))
                        If fila = MyBase.Position Then
                            CurrentItemChanged = True
                        End If
                    End If
                Next
            End If

            If CurrentItemChanged Then
                MyBase.OnCurrentItemChanged(New EventArgs())
            End If

        Catch ex As Exception
            DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("ERROR on OnListChanged: {0}: Prop:{1} [{2} - {3}  {4}]", sender, e.PropertyName, MyBase.DataSource, MyBase.DataMember, DBG.MensajeError(ex))))
        End Try
    End Sub


    Protected Overrides Sub OnListChanged(ByVal E As ListChangedEventArgs)
        If UsingNestedProperties() Then
            Select Case E.ListChangedType
                Case ListChangedType.ItemDeleted
                    DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("OnListChanged [ItemDeleted]: {0} - {1}", E.NewIndex, _currentItem)))

                    For i = 0 To _NestedObjects.Count - 1
                        If _NestedObjects(i).ObjDataSource Is _currentItem Then
                            ClearNestedObjectsInRow(i, _currentItem)
                            _NestedObjects.RemoveAt(i)
                            Exit For
                        End If
                    Next

                Case ListChangedType.ItemAdded
                    DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("OnListChanged [ItemAdded]: {0}", MyBase.List(E.NewIndex))))
                    PopulateNestedObjectsInRow(MyBase.List(E.NewIndex))


                Case ListChangedType.Reset
                    If _NestedObjects.Count > 0 AndAlso MyBase.List.Count = 0 Then
                        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("OnListChanged [Reset (List cleared)]")))
                        CreatePropertyDescriptors()
                    End If

            End Select
        End If

        MyBase.OnListChanged(E)
    End Sub

    Protected Overrides Sub OnCurrentChanged(ByVal e As System.EventArgs)
        _currentItem = MyBase.Current
        MyBase.OnCurrentChanged(e)
    End Sub

    Public Shadows Sub ResetBindings(ByVal metadaChanged As Boolean)
        CreatePropertyDescriptors()
        MyBase.ResetBindings(metadaChanged)
    End Sub

    Private Sub CleanUP()
        ClearAuxiliarStructures()
    End Sub


End Class

