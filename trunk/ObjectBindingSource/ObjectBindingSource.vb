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

    <Category("Nested Binding")> _
    Public Event CreatingObject(ByVal Sender As Object, ByVal ObjectType As Type, ByRef Obj As Object)

    <Category("Nested Binding")> _
    Public Event ListChangedOnChildList(ByVal fila As Integer, ByVal sender As Object, ByVal e As ListChangedEventArgs)

#Region "Shared"
    Private Shared _LastID As Integer = -1

    Private Shared Function GetID() As Integer
        _LastID += 1
        Return _LastID
    End Function

#End Region

#Region "Private Member Variables"

    Private _BindableNestedProperties As String()
    Private ReadOnly _propertyDescriptors As New List(Of PropertyDescriptor)

    Private _createProperties As Boolean = True
    Private _autoCreateObjects As Boolean = False

    Private _itemType As Type
    Private _listImplementsIBindingList As Boolean
    Private _currentItem As Object = Nothing   ' To find, from OnListChanged, which is the deleted object

    Private _ID As Integer

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

    Private ReadOnly Property ID() As String
        Get
            'Return ListBindingHelper.GetList(MyBase.DataSource, MyBase.DataMember).GetType().ToString
            If _itemType Is Nothing Then
                Return _ID.ToString
            Else
                Return String.Format("{0} ({1})", _ID, _itemType)
            End If
        End Get
    End Property


#Region "Constructors"

    ''' <summary>
    ''' Creates a new instance of the <see cref="ObjectBindingSource"/> component class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        _ID = GetID()
        InitializeComponent()
        AddHandler DirectCast(Me, ISupportInitializeNotification).Initialized, AddressOf ObjectBindingSource_Initialized
    End Sub

    ''' <summary>
    ''' Creates a new instance of the <see cref="ObjectBindingSource"/> component class.
    ''' </summary>
    ''' <param name="container"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal container As IContainer)
        _ID = GetID()
        container.Add(Me)
        InitializeComponent()
        AddHandler DirectCast(Me, ISupportInitializeNotification).Initialized, AddressOf ObjectBindingSource_Initialized
    End Sub

#End Region

    Private Function UsingNestedProperties() As Boolean
        Return (_BindableNestedProperties IsNot Nothing) AndAlso (_BindableNestedProperties.Length > 0)
    End Function

    Private Sub CreatePropertyDescriptors()
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] CreatePropertyDescriptors - Inicio >>>>", ID)))

        _createProperties = False

        ' Clear auxiliar structures, including the previous list of property descriptors
        ClearAuxiliarStructures()

        If MyBase.DataSource IsNot Nothing Then
            ' Get the type of object that this bindingsource is bound to            
            _itemType = ListBindingHelper.GetListItemType(MyBase.DataSource, MyBase.DataMember)
            _listImplementsIBindingList = GetType(IBindingList).IsInstanceOfType(ListBindingHelper.GetList(MyBase.DataSource, MyBase.DataMember))

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
                            Dim msg As String = String.Format("[{0}] La propiedad '{1}' no es reconocida: {2}", ID, nestedPropertyName, ex.Message)
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

        MyBase.ResetBindings(True)

        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] CreatePropertyDescriptors - Fin <<<<", ID)))
    End Sub

    Private Sub CustomPropertyDescriptor_CreatingObject(ByVal Sender As Object, ByVal ObjectType As Type, ByRef Obj As Object)
        RaiseEvent CreatingObject(Me, ObjectType, Obj)
    End Sub

    Private Sub ObjectBindingSource_Initialized(ByVal sender As Object, ByVal e As EventArgs)
        _createProperties = True
    End Sub

    Public Overrides Function GetItemProperties(ByVal listAccessors As PropertyDescriptor()) As PropertyDescriptorCollection
        If listAccessors Is Nothing Then
            If _createProperties Then
                Me.CreatePropertyDescriptors()
            End If
            If (_propertyDescriptors.Count > 0) Then
                Return New PropertyDescriptorCollection(_propertyDescriptors.ToArray)
            End If

        Else
            Dim oBS As ObjectBindingSource = GetRelatedObjectBindingSource(listAccessors(0))
            If oBS IsNot Nothing Then
                Return oBS.GetItemProperties(Nothing)
            End If
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
    ''' Gets or sets a list containing the bindable nested properties
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
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] PopulateTypesInNestedProperties [{1} - {2}] - Inicio >>>>", ID, MyBase.DataSource, MyBase.DataMember)))

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
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] PopulateTypesInNestedProperties - Fin <<<<", ID)))
    End Sub

    Private Sub PopulateNestedObjects()
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] PopulateNestedObjects  - Inicio >>>>", ID)))

        For Each objDataSource In MyBase.List
            PopulateNestedObjectsInRow(objDataSource)
        Next

        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] PopulateNestedObjects  -  Fin <<<<", ID)))
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

        ' We will subscribe also to the event PropertyChanged raised for the DataSource objects itself. That way we can offer 
        ' ListChanged events ford datasources implementing IList but not IBindingList
        If GetType(INotifyPropertyChanged).IsInstanceOfType(objDataSource) Then
            _DataSourceObjects.Add(objDataSource)
            RemoveHandler DirectCast(objDataSource, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
            AddHandler DirectCast(objDataSource, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
            DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] Escuchando PropertyChanged en: {1}", ID, objDataSource)))
        End If

        ' Treatment of any nested BindingSource, for use with hierarchical controls as Infragistics's UltraGrid
        If NotifyChangesInNestedPropertiesFromChildlists Then
            CreateNestedBindingSources(objDataSource)
        End If
    End Sub

    Private Sub ClearAuxiliarStructures()
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] ClearStructuresNestedObjects - Inicio >>>>", ID)))

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
                DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] RemoveHandler sobre: {1}", ID, obj)))
                RemoveHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
            Next
            _DataSourceObjects.Clear()
        End If

        RemoveNestedBindingSources()

        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] ClearStructuresNestedObjects - Fin <<<<", ID)))
    End Sub

    Private Sub ClearNestedObjectsInRow(ByVal fila As Integer, Optional ByVal objDataSource As Object = Nothing)
        For iNestedProperty = 0 To _BindableNestedProperties.Length - 1

            For Each obj In _NestedObjects(fila).ObjInProperties(iNestedProperty).Objects
                If GetType(INotifyPropertyChanged).IsInstanceOfType(obj) Then
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] RemoveHandler sobre: {1}", ID, obj)))
                    RemoveHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                End If
            Next
        Next

        If objDataSource IsNot Nothing Then
            _DataSourceObjects.Remove(objDataSource)
            DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] RemoveHandler sobre: {1}", ID, objDataSource)))
            RemoveHandler DirectCast(objDataSource, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged

            ' Treatment of any nested BindingSource, for use with hierarchical controls as Infragistics's UltraGrid
            If NotifyChangesInNestedPropertiesFromChildlists Then
                RemoveNestedBindingSources(objDataSource)
            End If
        End If

    End Sub


    Private Sub Show_NestedObjects()
        DBG_SaltoLinea(0)
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("[{0}] Contenido de _NestedObjects [{1} - {2}]", ID, MyBase.DataSource, MyBase.DataMember)))
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
        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("[{0}] Contenido de TypesInNestedProperties [{1} - {2}]", ID, MyBase.DataSource, MyBase.DataMember)))
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
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] Escuchando PropertyChanged en: {1}", ID, obj)))
                End If
            Next

        Catch ex As Exception
            ' Something is wrong in the property path the property
            ' Esto ya habrá sido identificado desde CreatePropertyDescriptors 
            objsInNestedProperty.Objects = New Object() {}
        End Try

    End Sub



    ''' <summary>
    ''' A partir de un objeto sobre el que se nos ha notificado de la modificación de una de sus propiedades (o todas si PropertyName es Nothing o String.Empty)
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
                    DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] RemoveHandler sobre: {0}", ID, ObjDescartar)))
                    RemoveHandler DirectCast(ObjDescartar, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                End If
            Next

            ' Comunicamos finalmente la modificación de las propiedades afectadas
            ' Las propiedades están identificadas con un índice en base 0 dentro de la relación de propiedades anidadas. Para localizar correctamente el descriptor
            ' tenemos que sumarle el número de propiedades no anidadas
            Dim numPropNotNested = _propertyDescriptors.Count - _BindableNestedProperties.Length
            Dim CurrentItemChanged As Boolean = False
            For Each propAfect In PropertiesAffected
                DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] OnListChanged: Row:{1} Prop:'{2}'", ID, propAfect.Row, _propertyDescriptors(numPropNotNested + propAfect.iNestedProperty).Name)))
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
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] Propiedad afectada: fila: {1} Prop: '{2}'", ID, row, _BindableNestedProperties(iNestedProperty)), 3))
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

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] ManagePropertyAffectedInRow. Row:{1} Position:{2}", ID, row, position)))

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
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] Escuchando PropertyChanged en: {0}", ID, NewValue)))
                End If
            Else
                PopulateObjectsInNestedPropertyForRow(_NestedObjects(row).ObjDataSource, _BindableNestedProperties(iNestedProperty), _NestedObjects(row).ObjInProperties(iNestedProperty))
            End If

        Else
            ' Si el objeto es final entonces no es necesario modificar la estructura de objetos en esta propiedad anidada
        End If

    End Sub



    Private Function IsNestedObjectInUse(ByVal Obj As Object) As Boolean

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] IsNestedObjectInUse: {1}", ID, Obj)))

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
    ''' <remarks>La propiedad puede venir a Nothing, indicando que todas (o cualquiera..) las propiedades han cambiado</remarks>
    Private Function CheckCanAppearInNestedProperty(ByVal Obj As Object, _
                                                       ByVal iNestedProperty As Integer, ByVal PropertyModifiedName As String, _
                                                       ByVal Posiciones As IList(Of Integer)) As Boolean

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] CheckPuedeAparecerEnNestedObjects: Obj={1} Prop='{2}'", ID, Obj, _BindableNestedProperties(iNestedProperty)), 2))


        Dim result As Boolean = False
        Dim nestedProperty As String() = _BindableNestedProperties(iNestedProperty).Split("."c)
        Dim propertyTypes = _TypesInNestedProperties(iNestedProperty)


        If Obj.GetType Is _itemType Then
            DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Es un objeto del tipo del DataSource"), 3))
            If (PropertyModifiedName Is Nothing) Or nestedProperty(0) = PropertyModifiedName Then
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


    ' La propiedad recibida puede venir a Nothing
    ' (El evento PropertyChanged puede indicar que todas las propiedades del objeto han cambiado utilizando null (Nothing en Visual Basic) o String.Empty como el nombre de propiedad en PropertyChangedEventArgs.)
    Protected Sub NestedObject_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        Try
            Dim numNestedProperties As Integer
            Dim CurrentItemChanged As Boolean = False
            Dim NewValue As Object = Nothing
            Dim PropertyName As String = Nothing

            If Not (e Is Nothing OrElse e.PropertyName = String.Empty) Then
                PropertyName = e.PropertyName
                NewValue = DynamicAccessorFactory.GetDynamicAccessor(sender.GetType).GetPropertyValue(sender, PropertyName)
            End If

            DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] NestedObject_PropertyChanged ({1}) Object:{2}  New value:{3}", ID, e.PropertyName, sender, NewValue)))

            If UsingNestedProperties() Then
                numNestedProperties = _BindableNestedProperties.Length
                CurrentItemChanged = ManagePropertiesAffected(sender, PropertyName, NewValue)
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
                            DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] OnListChanged: Row:{1} Prop:'{2}'", ID, fila, prop.Name)))
                            MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, fila, prop))
                            If fila = MyBase.Position Then
                                CurrentItemChanged = True
                            End If
                        End If
                    Next
                End If
            Next

            ' Si el origen de datos del BindingSource implementa IBindingList y no sólo IList entonces el componente
            ' generará los eventos ListChanged comunicados por la lista IBindingList. Pero si no lo implementa y sólo
            ' implementa IList p.ej sólo indicará el evento ListChanged si el tipo base implementa INotifyPropertyChanged 
            ' y el objeto que ha sido modificado se encuentra en la fila activa (probablemente porque el componente 
            ' BindingSource en estos casos se subscribe a INotifyPropertyChanged pero únicamente sobre CurrentItem
            ' Con los IBindingList el componente no tiene más que escuchar el evento ListChanged y relanzarlo)
            If Not _listImplementsIBindingList And objType Is _itemType Then
                Dim fila = MyBase.IndexOf(sender)
                For i = 0 To numPropNoAnidadas - 1
                    Dim prop = _propertyDescriptors(i)
                    If prop.Name = e.PropertyName Then
                        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] OnListChanged: Row:{1} Prop:'{2}'", ID, fila, prop.Name)))
                        MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, fila, prop))
                        If fila = MyBase.Position Then
                            CurrentItemChanged = True
                        End If
                        Exit For
                    End If
                Next
            End If

            ' Si estamos haciendo uso de componentes ObjectBindingSource anidados para poder notificar cambios en propiedades
            ' anidadas de listas hijas, deberemos comprobar si esta lista ha sido reemplazada por otra. Si es así la
            ' eliminaremos y volveremos a establecer.
            If NotifyChangesInNestedPropertiesFromChildlists AndAlso objType Is _itemType Then
                For Each prop In _propertyDescriptors
                    If prop.Name = e.PropertyName Then
                        If GetRelatedObjectBindingSource(prop) IsNot Nothing Then
                            If RemoveNestedBindingSources(sender, prop, NewValue) Then  ' Sólo se eliminará si realmente la lista es otra, no si a esta lista se le han añadido o eliminado elementos
                                CreateNestedBindingSources(sender, prop)
                            End If
                        End If
                        Exit For
                    End If
                Next
            End If

            If CurrentItemChanged Then
                MyBase.OnCurrentItemChanged(New EventArgs())
            End If

        Catch ex As Exception
            DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("[0] ERROR on OnListChanged: {1}: Prop:{2}  {3}]", ID, sender, e.PropertyName, DBG.MensajeError(ex))))
        End Try
    End Sub


    Protected Overrides Sub OnListChanged(ByVal E As ListChangedEventArgs)
        If UsingNestedProperties() Then
            Select Case E.ListChangedType
                Case ListChangedType.ItemDeleted
                    DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] OnListChanged [ItemDeleted]: {1} - {2}", ID, E.NewIndex, _currentItem)))

                    For i = 0 To _NestedObjects.Count - 1
                        If _NestedObjects(i).ObjDataSource Is _currentItem Then
                            ClearNestedObjectsInRow(i, _currentItem)
                            _NestedObjects.RemoveAt(i)
                            Exit For
                        End If
                    Next

                Case ListChangedType.ItemAdded
                    DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] OnListChanged [ItemAdded]: {1}", ID, MyBase.List(E.NewIndex))))
                    PopulateNestedObjectsInRow(MyBase.List(E.NewIndex))


                Case ListChangedType.Reset
                    If _NestedObjects.Count > 0 AndAlso MyBase.List.Count = 0 Then
                        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] OnListChanged [Reset (List cleared)]", ID)))
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
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] DISPOSE: CleanUP *******", ID)))
        ClearAuxiliarStructures()
    End Sub

#Region "Related/nested ObjectBindingSource components"

    ' Related/nested ObjectBindingSource components
    ' We can have components associated to some list properties, so that the method GetItemProperties can return the
    ' PropertyDescriptorCollection with the objects PropertyDescriptor indicated in that component.
    ' This way that listAccesors can also expose nested properties.
    ' This is useful with UI controls that show not only the properties of the datasource objects itself, but also can show in
    ' another level (for example, 'bands' in Infragistics's UltraGrid) the properties of the child objects 

    Private Structure NestedRelatedBindingInf
        Public ObjRow As Object
        Public PropertyDesc As CustomPropertyDescriptor
        Sub New(ByVal objRow As Object, ByVal propDesc As CustomPropertyDescriptor)
            Me.ObjRow = objRow
            Me.PropertyDesc = propDesc
        End Sub
    End Structure

    ' Internal array with the nested ObjectBindingSource components that manage the PropertyDescriptors of one
    ' property associated to a child list. This components are used as templates to the new ones that will be dinamically 
    ' created and referenced in _NestedBindingSources
    Private _RelatedObjectBindingSources As ObjectBindingSource() = New ObjectBindingSource() {}

    ' References each of the ObjectBindingSource component that provide support for changes in nested property accessors in child lists,
    ' via the listening of INotifyPropertyChanges.
    ' This components will be created only if the property 'NotifyChangesInNestedPropertiesFromChildlists' is set to True.
    ' Otherwise, it will only be used the components in _RelatedBindingSource, to facilitate the property accesors.
    Private _NestedBindingSources As New Dictionary(Of ObjectBindingSource, NestedRelatedBindingInf)


    ''' <summary>
    ''' Gets or sets the related/nested components ObjectBindingSource that offer the PropertyDescriptors for the properties
    ''' associated to child lists. The properties will be associated automatically from the type of the list item.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' This way the listAccesors provided can also expose nested properties.
    ''' This is useful with UI controls that show not only the properties of the datasource objects itself, but also can show in
    ''' another level (for example, 'bands' in Infragistics's UltraGrid) the properties of the child objects
    ''' This components will be used as templates to the new ones that will be dinamically created and referenced 
    ''' in _NestedBindingSources if the property NotifyChangesInNestedPropertiesFromChildlists is set to True.
    ''' </remarks>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    <Category("Data")> _
    <Editor(GetType(SelectionObjectsBindingSourcesEditor), GetType(UITypeEditor))> _
    Public Property RelatedObjectBindingSources() As ObjectBindingSource()
        Get
            Return _RelatedObjectBindingSources
        End Get

        Set(ByVal value As ObjectBindingSource())
            RemoveNestedBindingSources()
            _RelatedObjectBindingSources = value
        End Set
    End Property


    ''' <summary>
    ''' Gets the related ObjectBindingSource corresponding to the propertyDescriptor indicated
    ''' </summary>
    Private Function GetRelatedObjectBindingSource(ByVal prop As PropertyDescriptor) As ObjectBindingSource
        If _RelatedObjectBindingSources Is Nothing Then Return Nothing

        If CType(prop, CustomPropertyDescriptor).PropertyType.GetInterface("IList") IsNot Nothing Then

            Dim typeProperty As Type = prop.PropertyType.GetProperty("Item").PropertyType  ' Otra forma de obtener el tipo de los items de una lista: Type type = list.GetType.GetGenericArguments(0)
            For Each obs In _RelatedObjectBindingSources
                Dim obsItemType As Type = ListBindingHelper.GetListItemType(obs.DataSource, obs.DataMember)
                If obsItemType Is typeProperty Then
                    Return obs
                End If
            Next

        End If

        Return Nothing
    End Function


    Private Sub CreateNestedBindingSources(ByVal objectRow As Object, Optional ByVal propDesc As PropertyDescriptor = Nothing)
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] CreateNestedBindingSources: {1}", ID, objectRow)))

        For Each prop In _propertyDescriptors
            If Not (propDesc Is Nothing OrElse prop Is propDesc) Then Continue For
            Dim refObjBindingSrc = GetRelatedObjectBindingSource(prop)
            If refObjBindingSrc Is Nothing Then Continue For

            Dim list = prop.GetValue(objectRow)
            If list IsNot Nothing Then

                Dim objBindingSrc = CType(Activator.CreateInstance(GetType(ObjectBindingSource)), ObjectBindingSource)  ' Create a new ObjectBindingSource

                DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] Created New NestedBindingSource", ID)))

                objBindingSrc.BindableNestedProperties = refObjBindingSrc.BindableNestedProperties
                objBindingSrc.AutoCreateObjects = refObjBindingSrc.AutoCreateObjects
                objBindingSrc.DataSource = list

                objBindingSrc.CreatePropertyDescriptors()
                DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] Escuchando ListChanged en: {1}", ID, list)))

                RemoveHandler objBindingSrc.ListChanged, AddressOf ChildList_ListChanged
                AddHandler objBindingSrc.ListChanged, AddressOf ChildList_ListChanged

                _NestedBindingSources.Add(objBindingSrc, New NestedRelatedBindingInf(objectRow, CType(prop, CustomPropertyDescriptor)))
            End If

        Next

    End Sub


    ''' <summary>
    ''' Indicates if the component must offer support for changes in nested properties of child lists, via the listening
    ''' of INotifyPropertyChanges.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>If set to True then it will be created new ObjectBindingSource components to manage that changes (one for each child
    ''' list to supervise). And also, changes in non nested properties of this child lists will be notified even if that lists don't
    ''' implement IBindingList.
    ''' Otherwise, it will only be used the components in _RelatedBindingSource, to facilitate the property accesors.
    ''' </remarks>
    <Category("Data")> _
    Public Property NotifyChangesInNestedPropertiesFromChildlists() As Boolean
        Get
            Return _NotifyChangesInNestedPropertiesFromChildlists
        End Get
        Set(ByVal value As Boolean)
            If value <> _NotifyChangesInNestedPropertiesFromChildlists Then
                _NotifyChangesInNestedPropertiesFromChildlists = value

                If value Then
                    For Each objRow In MyBase.List
                        CreateNestedBindingSources(objRow)
                    Next
                Else
                    RemoveNestedBindingSources()
                End If
            End If
        End Set
    End Property
    Private _NotifyChangesInNestedPropertiesFromChildlists As Boolean = False


    ''' <summary>
    ''' Controls whether to raise or not a ListChange event on changes over child lists (The ListChange events refer to the parent
    ''' row and the property associated to the child list)
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' Realmente puede no aportar gran cosa comunicar el cambio en la fila padre si la lista hija no implementa IBindingList, porque 
    ''' el control sólo actualizará la primera fila de cada lista de hijos y sólo si el padre es el registro activo (al menos es el 
    ''' comportamiento observado con listas que implementan IList). De hecho esto lo hará incluso aunque no comuniquemos este evento
    ''' OnListChanged (al menos es lo observado con UltraGrid v6.2)
    ''' => Puede ser más útil comunicar que ha habido un cambio y que se controle el evento y se actúe como haga falta.
    ''' Es por ello que se ofrece el evento ListChangedOnChildList(ByVal fila As Integer, ByVal sender As Object, ByVal e As ListChangedEventArgs)
    ''' De todas maneras aún puede interesar notificar el cambio vía OnListChanged, lo que se hará únicamente si esta propiedad se establece 
    ''' a True
    ''' </remarks>
    <Category("Behavior")> _
    Public Property NotifyListChangesFromNestedBindingSources() As Boolean
        Get
            Return _NotifyListChangesFromNestedBindingSources
        End Get
        Set(ByVal value As Boolean)
            _NotifyListChangesFromNestedBindingSources = value
        End Set
    End Property
    Private _NotifyListChangesFromNestedBindingSources As Boolean = False


    Private Sub RemoveNestedBindingSources()
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] RemoveNestedBindingSources", ID)))
        If _NestedBindingSources IsNot Nothing Then
            For Each objBS In _NestedBindingSources.Keys
                objBS.Dispose()
            Next
            _NestedBindingSources.Clear()
        End If
    End Sub

    Private Function RemoveNestedBindingSources(ByVal objRow As Object, Optional ByVal propDesc As PropertyDescriptor = Nothing, Optional ByVal NewValue As Object = Nothing) As Boolean
        Dim result = False
        Dim toRemove As New List(Of ObjectBindingSource)
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] RemoveNestedBindingSources({1})", ID, objRow)))
        For Each obsInf In _NestedBindingSources
            If obsInf.Value.ObjRow Is objRow Then
                If propDesc Is Nothing OrElse (obsInf.Value.PropertyDesc Is propDesc AndAlso ListBindingHelper.GetList(obsInf.Key.DataSource, obsInf.Key.DataMember) IsNot NewValue) Then
                    toRemove.Add(obsInf.Key)
                    result = True
                End If
            End If
        Next
        For Each obs In toRemove
            DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] Remove and Dispose NestedBindingSource({1})", ID, obs), 1))
            _NestedBindingSources.Remove(obs)
            obs.Dispose()
        Next
        Return result
    End Function

    Private Sub ChildList_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
        DBG.Foo(DBG_ChkNivel(2) AndAlso DBG.Log(2, String.Format("[{0}] ChildList_ListChanged: {1}", ID, sender)))

        Dim relatedBindingInf As NestedRelatedBindingInf = Nothing
        Dim objBindingSrc As ObjectBindingSource = CType(sender, ObjectBindingSource)
        If _NestedBindingSources.TryGetValue(objBindingSrc, relatedBindingInf) Then
            Dim fila = MyBase.List.IndexOf(relatedBindingInf.ObjRow)
            If fila >= 0 Then
                If _NotifyListChangesFromNestedBindingSources Then
                    MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, fila, relatedBindingInf.PropertyDesc))
                    'MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, fila))
                End If
                RaiseEvent ListChangedOnChildList(fila, sender, e)

            Else
                DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("[{0}] ERROR en ChildList_ListChanged. Objeto no localizado: {1}", CType(sender, ObjectBindingSource).ID, relatedBindingInf.ObjRow)))
            End If
        Else
            DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("[{0}] ERROR en ChildList_ListChanged. ObjectBindingSource no encontrado en _NestedBindingSources", CType(sender, ObjectBindingSource).ID)))
        End If

    End Sub

#End Region

End Class
