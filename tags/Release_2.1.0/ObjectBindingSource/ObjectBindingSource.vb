Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Design

Imports System.Reflection


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
    <Description("Indicates that a new object will be created, according to the property AutoCreateObjects, when setting the values. It is similar to the event AddingNew, but the object to create can be of any type")> _
    Public Event CreatingObject(ByVal Sender As Object, ByVal ObjectType As Type, ByRef Obj As Object)

    <Category("Nested Binding")> _
    <Description("Informs of a change in the child list associated to the row indicated. Controlling this event we can refresh de UI control the way needed.")> _
    Public Event ListChangedOnChildList(ByVal RowIndex As Integer, ByVal sender As Object, ByVal e As ListChangedEventArgs)

    <Category("Nested Binding")> _
    <Description("Indicates that an error have been detected (example: invalid nested property)")> _
    Public Event NestedError(ByVal Sender As Object, ByVal e As NestedErrorEventArgs)

    <Category("Nested Binding")> _
    Public Event ConfigurationChanged(ByVal sender As Object)

#Region "Shared"
    Private Shared _LastID As Integer = 0  ' First one will be 1

    Private Shared Function GetID() As Integer
        _LastID += 1
        Return _LastID
    End Function

#If DEBUG Then
    Public Shared _Instances As New List(Of WeakReference)

    Public Shared Function NumberInstancesAlive() As Integer
        Dim i = 0
        For Each o In _Instances
            If o.IsAlive Then
                i += 1
            End If
        Next
        Return i
    End Function

    Public Function InstancesAlive() As String
        Dim cad = ""
        For Each o In _Instances
            If o.IsAlive Then
                cad += vbCrLf + o.Target.ToString
            End If
        Next
        Return cad
    End Function

#End If


#End Region

#Region "Private Member Variables"

    Private _ListRaisesItemChangedEvents As Boolean = False
    Private _BindableNestedProperties As String() = Nothing
    Private _autoCreateObjects As Boolean = False

    Private _ID As Integer
    Private _InnerList As IList = Nothing
    Private _itemType As Type = Nothing
    Private _currentItem As Object = Nothing

    Private _PropertyNamesInNestedPaths As List(Of String) = Nothing
    Private _propertyDescriptors As PropertyDescriptorCollection

    Private _iFirstNestedProperty As Integer = -99        ' -1:  There is no nested property   < -1: Not calculated yet


    ' _TypesInNestedProperties:
    ' Collect an array of object types for each nested property. Each array includes the types of each of the 
    ' properties of the path that describes how to access the value of the nested property.
    ' In this array of types it is not included the type corresponding to the object wich is the DataSource,
    ' unless shown elsewhere in that path.
    Private _TypesInNestedProperties As Type()() = Nothing


    ' _HookedObjects:
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
    Private _HookedObjects As List(Of ObjectsInRow) = Nothing


#End Region

    Private ReadOnly Property ID() As String
        Get
            If _ID = 0 Then
                Return "<New>"
            ElseIf _itemType Is Nothing Then
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
    Public Sub New()
        _ID = GetID()
        InitializeComponent()

#If DEBUG Then
        _Instances.Add(New WeakReference(Me))
#End If

    End Sub

    ''' <summary>
    ''' Creates a new instance of the <see cref="ObjectBindingSource"/> component class.
    ''' </summary>
    Public Sub New(ByVal container As IContainer)
        _ID = GetID()
        container.Add(Me)
        InitializeComponent()
        AddHandler DirectCast(Me, ISupportInitializeNotification).Initialized, AddressOf BindingSource_Initialized

#If DEBUG Then
        _Instances.Add(New WeakReference(Me))
#End If
    End Sub

    ''' <summary>
    ''' Makes the component to reconsider the configuration. 
    ''' This could be necessary if any of the properties of the ObjectBindingSource components used as reference
    ''' (included in RelatedObjectBindingSource) have been changed and ConsiderChildsOnlyInCurrent = False.
    ''' This way we make sure that all the nested binding source are recreated.
    ''' </summary>
    Private Sub Reconfigure(Optional ByVal MetadataChanged As Boolean = True, Optional ByVal ConfigurationChanged As Boolean = False)
        CreatePropertyDescriptors(True)
        WireObjects(True)

        If ConfigurationChanged Then
            OnConfigurationChanged()
        End If

        If MetadataChanged Then
            OnListChanged(New ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, 0))
        End If
    End Sub

    Private Sub BindingSource_Initialized(ByVal sender As Object, ByVal e As EventArgs)
        CreatePropertyDescriptors()
        WireObjects()
    End Sub

    Public Function IsInitialized() As Boolean
        Return DirectCast(Me, ISupportInitializeNotification).IsInitialized
    End Function

#End Region

    Public Overrides Function ToString() As String
        Dim numRelOBS = 0
        If _RelatedObjectBindingSources IsNot Nothing Then numRelOBS = _RelatedObjectBindingSources.Length
        Dim numNestedOBS = 0
        If _NestedBindingSources IsNot Nothing Then numNestedOBS = _NestedBindingSources.Count
        Dim numItems = 0
        If MyBase.List IsNot Nothing Then numItems = MyBase.List.Count

        Return String.Format("{0} ({1}) nºNestedProp:{2} nºRelOBS:{3} nºNestedOBS:{4} nºItems:{5}", _ID, _itemType, NumNestedProperties, numRelOBS, numNestedOBS, numItems)
    End Function

#If DEBUG Then

    Public Sub ShowConfiguration(Optional ByVal indent As Integer = 0)
        Dim cad = New String(" "c, 2 * indent) + Me.ToString
        DBG.Log(0, cad)
        If _iFirstNestedProperty >= 0 Then
            DBG.Log(0, "NESTED PROPERTIES", 1 + indent)
            For i = _iFirstNestedProperty To _propertyDescriptors.Count - 1
                DBG.Log(0, CType(_propertyDescriptors(i), CustomPropertyDescriptor).PropertyPath, 2 + indent)
            Next
        End If

        If _RelatedObjectBindingSources IsNot Nothing Then
            DBG.Log(0, "RELATED oBS", 1 + indent)
            For Each oBS In _RelatedObjectBindingSources
                DBG.Log(0, oBS.ID, 2 + indent)
            Next
        End If

        If _NestedBindingSources IsNot Nothing Then
            DBG.Log(0, "NESTED oBS", 1 + indent)
            For Each nbs In _NestedBindingSources
                Dim obs = nbs.Key
                DBG.Log(0, String.Format("-> Row:{0} Prop:{1}", MyBase.List.IndexOf(nbs.Value.ObjRow), nbs.Value.PropertyDesc), 2 + indent)
                obs.ShowConfiguration(3 + indent)
            Next
        End If
    End Sub

#End If

    ''' <summary>
    ''' Indicates if the component must offer additional support for changes in properties. Base BindingSource communicate changes in properties of items
    ''' of the datasource list (if that items implements INotifyPropertyChanged). If it is necessary to notify changes in other properties, such as nested
    ''' properties then this property, 'NotifyPropertyChanges' must be set to True. [Default: False]
    ''' </summary>
    ''' <remarks>
    ''' If set to True then many objects will be hooked to its PropertyChanged event. If this is not necessary then keeping to False will avoid having
    ''' to inspect certain properties of the datasource objects. If the datasource list has properties expensive to obtain, this way we will avoid that
    ''' calculation, unless needed.
    ''' </remarks>
    <Category("Data")> _
    <Description("Indicates if the component must offer additional support for changes in properties, through the event PropertyChanged of nested objects.")> _
    Public Property NotifyPropertyChanges() As Boolean
        Get
            Return _NotifyPropertyChanges
        End Get
        Set(ByVal value As Boolean)
            If value <> _NotifyPropertyChanges Then
                If Not value And IsInitialized() Then
                    UnwireObjects(True)
                End If
                _NotifyPropertyChanges = value

                If IsInitialized() Then
                    Reconfigure(True, True)
                End If
            End If
        End Set
    End Property
    Private _NotifyPropertyChanges As Boolean = False


    Private Function NestedPropertiesDefined() As Boolean
        Return (_BindableNestedProperties IsNot Nothing) AndAlso (_BindableNestedProperties.Length > 0)
    End Function

    Private Function IndexLastNonNestedProperty() As Integer
        If _iFirstNestedProperty >= 0 Then
            Return _iFirstNestedProperty - 1
        Else
            Return _propertyDescriptors.Count - 1
        End If
    End Function

    Private Function OriginalPropertiesKeepsEqual(ByVal NewOriginalProps As PropertyDescriptorCollection) As Boolean
        If _propertyDescriptors Is Nothing OrElse _propertyDescriptors.Count < NewOriginalProps.Count Then Return False

        For i = 0 To IndexLastNonNestedProperty()
            If Not NewOriginalProps(i).Equals(_propertyDescriptors(i)) Then
                Return False
            End If
        Next

        Return True
    End Function


    Private Sub CreatePropertyDescriptors(Optional ByVal Force As Boolean = False)

        Dim originalItemProperties = MyBase.GetItemProperties(Nothing)    ' Get the original properties. Rely in base BindingSource (and this one in ListBindingHelper)
        Dim newItemType = ListBindingHelper.GetListItemType(MyBase.List)  ' Get the type of object that this bindingsource is bound to

        If Not Force And newItemType Is _itemType AndAlso OriginalPropertiesKeepsEqual(originalItemProperties) Then
            Exit Sub   ' No changes
        End If

        UnwireObjects(False)

        _propertyDescriptors = originalItemProperties
        _itemType = newItemType

        _iFirstNestedProperty = _propertyDescriptors.Count  ' En principio

        If NestedPropertiesDefined() AndAlso _itemType IsNot GetType(Object) Then
            DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] CreatePropertyDescriptors - BEGIN", ID)))

            Dim props(_propertyDescriptors.Count - 1) As PropertyDescriptor
            originalItemProperties.CopyTo(props, 0)
            _propertyDescriptors = New PropertyDescriptorCollection(props)

            For Each nestedPropertyPath In _BindableNestedProperties
                Try
                    ' Get the original propertydescriptor based on the property path in bindableProperty
                    Dim propertyDescriptor = ReflectionHelper.GetPropertyDescriptorFromPath(_itemType, nestedPropertyPath)

                    Dim attributes As Attribute() = New Attribute((propertyDescriptor.Attributes.Count + 1) - 1) {}                      ' Create a attribute array and make room for one more attribute 

                    propertyDescriptor.Attributes.CopyTo(attributes, 0)                                                                  ' Copy the original attributes to the custom descriptor
                    attributes((attributes.Length - 1)) = New CustomPropertyAttribute(_itemType, nestedPropertyPath, propertyDescriptor) ' Copy the original attributes to the custom descriptor

                    Dim custPropertyDescriptor = New CustomPropertyDescriptor(nestedPropertyPath, propertyDescriptor, attributes, _autoCreateObjects)
                    _propertyDescriptors.Add(custPropertyDescriptor)

                    If _autoCreateObjects Then
                        AddHandler custPropertyDescriptor.CreatingObject, AddressOf CustomPropertyDescriptor_CreatingObject
                    End If

                Catch ex As Exception    ' Something is wrong in the property path
                    Dim msg As String = String.Format("[{0}] Property '{1}' is not recognized: {2}", ID, nestedPropertyPath, ex.Message)
                    If Not MyBase.DesignMode Then
                        DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, "ERROR in CreatePropertyDescriptors: " + msg))
                        OnNestedError(New NestedErrorEventArgs(ErrorType.PropertyUnrecognized, nestedPropertyPath, Nothing, ex))

                    ElseIf MyBase.DataMember IsNot Nothing Then
                        MessageBox.Show(msg)
                    End If
                End Try
            Next

            If _propertyDescriptors.Count = _iFirstNestedProperty Then
                _iFirstNestedProperty = -1       ' There is no valid nested property
            End If

            PopulateTypesInNestedProperties()
            CheckNonNestedPropertiesToSupervise()
            CheckChildListsToConsider()
            PopulateAssociatedOBS()
            FillPropertyNamesInNestedPaths()

            DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] CreatePropertyDescriptors - END", ID)))

        Else
            CheckNonNestedPropertiesToSupervise()
            _iFirstNestedProperty = -1
        End If

    End Sub

    Private Sub CustomPropertyDescriptor_CreatingObject(ByVal Sender As Object, ByVal ObjectType As Type, ByRef Obj As Object)
        RaiseEvent CreatingObject(Me, ObjectType, Obj)
    End Sub


    Public Overrides Function GetItemProperties(ByVal listAccessors As PropertyDescriptor()) As PropertyDescriptorCollection
        If listAccessors Is Nothing OrElse listAccessors.Length = 0 Then
            If _propertyDescriptors Is Nothing Then
                Me.CreatePropertyDescriptors()
            End If
            Return _propertyDescriptors

        Else
            Dim obs As ObjectBindingSource = GetAssociatedOBS(listAccessors(0))
            If obs IsNot Nothing Then
                If listAccessors.Length > 1 Then
                    Dim newList(listAccessors.Length - 2) As PropertyDescriptor
                    Array.Copy(listAccessors, 1, newList, 0, newList.Length)
                    Return obs.GetItemProperties(newList)
                Else
                    Return obs.GetItemProperties(Nothing)
                End If

            Else
                Return MyBase.GetItemProperties(listAccessors)
            End If
        End If

    End Function


    ''' <summary>
    ''' Indicates if objects should be automatically created when setting property path values.
    ''' </summary>
    <Category("Data")> _
    <Description("Indicates if objects should be automatically created when setting property path values")> _
    Public Property AutoCreateObjects() As Boolean
        Get
            Return _autoCreateObjects
        End Get
        Set(ByVal value As Boolean)
            If _autoCreateObjects <> value Then
                _autoCreateObjects = value
                If IsInitialized() Then
                    Reconfigure(True, True)
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a list containing the bindable nested properties
    ''' </summary>
    <Category("Data")> _
    <Description("List containing the bindable nested properties")> _
    <Editor(GetType(NestedPropertiesEditor), GetType(UITypeEditor))> _
    Public Property BindableNestedProperties() As String()
        Get
            Return _BindableNestedProperties
        End Get
        Set(ByVal value As String())
            If IsInitialized() Then
                UnwireObjects(False)  ' Pregunta por UsingNestedProperties(), que depende de BindableNestedProperties
            End If

            _iFirstNestedProperty = -99         ' (< -1: Not calculated yet) Cuando se invoque a CreatePropertyDescriptors se calculará
            _BindableNestedProperties = value

            If IsInitialized() Then
                Reconfigure(True, True)
            End If
        End Set
    End Property

    Private Function NumNestedProperties() As Integer
        If _propertyDescriptors Is Nothing Or _iFirstNestedProperty < 0 Then Return 0

        Return _propertyDescriptors.Count - _iFirstNestedProperty
    End Function

    Private Function ConsiderNonNestedProperties() As Boolean
        Return (_NonNestedPropertiesToSupervise IsNot Nothing AndAlso _NonNestedPropertiesToSupervise.Length > 0)
    End Function


    Private Structure ObjectsInRow
        Public ObjRow As Object
        Public ObjInProperties As ObjectsInNestedProperty()
    End Structure

    Private Structure ObjectsInNestedProperty
        Public Objects As Object()
    End Structure

    Private Structure LocationAffected
        Public iNestedProperty As Integer
        Public Position As Integer
    End Structure


    Private Sub PopulateTypesInNestedProperties()
        If _iFirstNestedProperty < 0 Then Exit Sub

        ' Redimension this structure depending on the number of nested binding properties defined (and valid)
        ReDim _TypesInNestedProperties(NumNestedProperties() - 1)
        Dim propertyTypes As New List(Of Type)
        Dim propPath As String = Nothing

        For i = 0 To NumNestedProperties() - 1
            Try
                propertyTypes.Clear()
                propPath = CType(_propertyDescriptors(_iFirstNestedProperty + i), CustomPropertyDescriptor).PropertyPath
                ReflectionHelper.GetPropertyTypesFromPath(_itemType, propPath, propertyTypes)
                _TypesInNestedProperties(i) = propertyTypes.ToArray

            Catch ex As Exception   ' Something is wrong in the property path
                OnNestedError(New NestedErrorEventArgs(ErrorType.PropertyUnrecognized, propPath, Nothing, ex))
                _TypesInNestedProperties(i) = New Type() {}
            End Try
        Next

#If DEBUG Then
        If DBG.MaxDebugLevel >= 3 Then
            Show_TypesInNestedProperties()
        End If
#End If

    End Sub

    ''' <summary>
    ''' Gets or sets non nested properties (implementing INotifyPropertyChanged) that must be listened 
    ''' in order to detect changes in its properties.
    ''' </summary>
    ''' <remarks>
    ''' This could be interesting when those properties could be used in controls like combo, and the modification of one of their properties 
    ''' could affect the value showed as display member.
    ''' Only non nested properties implementing INotifyPropertyChanged that are explicitly defined here will be considered. Otherwise, 
    ''' perhaps many properties should be queried at propertyChanged events, and ocassionally the queries could be expensive.
    ''' </remarks>
    <Category("Data")> _
    <Description("Gets or sets non nested properties (implementing INotifyPropertyChanged) that must be listened " + _
                 "in order to detect changes in its properties.")> _
    Public Property NonNestedPropertiesToSupervise() As String()
        Get
            Return _NonNestedPropertiesToSupervise
        End Get
        Set(ByVal value As String())
            If IsInitialized() Then
                UnwireObjects(False)
            End If

            If value IsNot Nothing AndAlso value.Length = 0 Then
                value = Nothing
            End If
            _NonNestedPropertiesToSupervise = value


            If IsInitialized() Then
                If _NotifyPropertyChanges Then
                    Reconfigure(False, True)
                Else
                    If Me.DesignMode Then CheckNonNestedPropertiesToSupervise()
                End If
            End If
        End Set
    End Property
    Private _NonNestedPropertiesToSupervise As String() = Nothing


    Private Sub CheckNonNestedPropertiesToSupervise()
        If _NonNestedPropertiesToSupervise Is Nothing OrElse MyBase.DataSource Is Nothing Then Exit Sub

        Dim auxList As New List(Of String)

        For Each nnProp In _NonNestedPropertiesToSupervise
            Dim prop = _propertyDescriptors.Find(nnProp, True)
            If prop Is Nothing OrElse Not GetType(INotifyPropertyChanged).IsAssignableFrom(prop.PropertyType) Then
                Dim cadErr = String.Format("[{0}] ERROR setting NonNestedPropertiesToSupervise: '{1}' is not a valid property (It will be ignored)", ID, nnProp)
                DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, cadErr))
                If Me.DesignMode Then
                    MsgBox(cadErr, MsgBoxStyle.Exclamation, "ObjectBindingSource")
                    auxList.Add(nnProp)
                End If
            Else
                auxList.Add(prop.Name)
            End If
        Next

        _NonNestedPropertiesToSupervise = auxList.ToArray
    End Sub


    Private Function IsConfigured() As Boolean
        If _InnerList Is MyBase.List AndAlso (_iFirstNestedProperty >= -1 And _HookedObjects IsNot Nothing) Then   ' _iFirstNestedProperty = -1: No nested property, but analyzed
            Return True
        End If

        Return False
    End Function

    Private Sub WireObjects(Optional ByVal Force As Boolean = False)

        If Not Force And IsConfigured() Then Exit Sub

        UnwireObjects(True)

        _InnerList = MyBase.List


        If TypeOf List Is IRaiseItemChangedEvents Then
            _ListRaisesItemChangedEvents = TryCast(List, IRaiseItemChangedEvents).RaisesItemChangedEvents
        Else
            _ListRaisesItemChangedEvents = TypeOf _InnerList Is IBindingList
        End If

        If _InnerList Is Nothing OrElse _InnerList.Count = 0 Then
            Exit Sub
        End If

        If Not NotifyPropertyChanges Then Exit Sub



        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] WireObjects  - BEGIN", ID)))

        If _iFirstNestedProperty >= 0 Or ConsiderNonNestedProperties() Then
            If _HookedObjects Is Nothing Then
                _HookedObjects = New List(Of ObjectsInRow)()
            End If

            For Each objRow In _InnerList
                HookObjectsInRow(objRow)
            Next
        End If


        If Not _ListRaisesItemChangedEvents AndAlso GetType(INotifyPropertyChanged).IsAssignableFrom(_itemType) Then
            For Each objRow As INotifyPropertyChanged In _InnerList
                AddHandler objRow.PropertyChanged, AddressOf ItemList_PropertyChanged
                DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Hook ItemList.PropertyChanged: {0}", objRow), 2))
            Next
        End If

        If NotifyChangesInNestedPropertiesFromChildlists Then
            CreateNestedBindingSources()
        End If

        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] WireObjects  -  END", ID)))

#If DEBUG Then
        If DBG.MaxDebugLevel >= 3 Then
            Show_HookedObjects()
        End If
#End If
    End Sub

    Private Sub HookObjectsInRow(ByVal objRow As Object)
        _HookObjectsInRow(objRow, -1, False, False)
    End Sub

    Private Sub HookObjectsInRow(ByVal objRow As Object, ByVal NewIndex As Integer, _
                                 Optional ByVal ReplaceInHookedObjects As Boolean = False, _
                                 Optional ByVal HookItemList As Boolean = True)
        _HookObjectsInRow(objRow, NewIndex, ReplaceInHookedObjects, HookItemList)
    End Sub

    Private Sub _HookObjectsInRow(ByVal objRow As Object, ByVal Index As Integer, ByVal ReplaceInHookedObjects As Boolean, ByVal HookItemList As Boolean)

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("HookObjectsInRow. Index:{0} Obj:{1}", Index, objRow), 2))

        If HookItemList AndAlso Not _ListRaisesItemChangedEvents Then
            If TypeOf objRow Is INotifyPropertyChanged Then
                AddHandler CType(objRow, INotifyPropertyChanged).PropertyChanged, AddressOf ItemList_PropertyChanged
                DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Hook ItemList.PropertyChanged: {0}", objRow), 3))
            End If
        End If



        If _iFirstNestedProperty < 0 AndAlso Not ConsiderNonNestedProperties() Then Exit Sub


        ' Redimension this internal structure depending on the number of nested binding properties defined and considering
        ' the necessity of supervising objects in non nested properties
        Dim n = NumNestedProperties() - 1
        If ConsiderNonNestedProperties() Then n += 1
        Dim ObjectsInNestedProperties(n) As ObjectsInNestedProperty

        Dim i = 0
        If _iFirstNestedProperty >= 0 Then
            For iProp = _iFirstNestedProperty To _propertyDescriptors.Count - 1
                Dim propPath = CType(_propertyDescriptors(iProp), CustomPropertyDescriptor).PropertyPath
                HookObjectsInNestedPropertyForRow(objRow, propPath, ObjectsInNestedProperties(i))
                i += 1
            Next
        End If

        If ConsiderNonNestedProperties() Then
            Dim ObjInNonNestedPropertiesToSupervise(_NonNestedPropertiesToSupervise.Length - 1) As Object

            Dim k = 0
            For Each prop In _NonNestedPropertiesToSupervise
                Dim Obj = TryCast(_propertyDescriptors(prop).GetValue(objRow), INotifyPropertyChanged)
                If Obj IsNot Nothing And (Obj IsNot objRow) Then     ' Obj IsNot objRow -> Ignoramos propiedades tipo Self
                    RemoveHandler Obj.PropertyChanged, AddressOf NestedObject_PropertyChanged
                    AddHandler Obj.PropertyChanged, AddressOf NestedObject_PropertyChanged
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Hook ObjInNonNestedProp.PropertyChanged on: {0}", Obj), 3))
                End If
                ObjInNonNestedPropertiesToSupervise(k) = Obj
                k += 1
            Next
            ObjectsInNestedProperties(i).Objects = ObjInNonNestedPropertiesToSupervise
        End If


        Dim ObsInRow = New ObjectsInRow With {.ObjRow = objRow, .ObjInProperties = ObjectsInNestedProperties}
        If Index >= 0 Then
            If ReplaceInHookedObjects Then
                _HookedObjects(Index) = ObsInRow
            Else
                _HookedObjects.Insert(Index, ObsInRow)
            End If
        Else
            _HookedObjects.Add(ObsInRow)
        End If
    End Sub



    Private Sub UnwireObjects(ByVal PreservePropertyDescriptors As Boolean)
        If _InnerList Is Nothing Or Not NotifyPropertyChanges Then Exit Sub

        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] UnhookObjects - BEGIN", ID)))

        If Not PreservePropertyDescriptors And _iFirstNestedProperty >= 0 Then
            For i = 0 To NumNestedProperties() - 1
                RemoveHandler CType(_propertyDescriptors(_iFirstNestedProperty + i), CustomPropertyDescriptor).CreatingObject, AddressOf CustomPropertyDescriptor_CreatingObject
            Next
            If _PropertyNamesInNestedPaths IsNot Nothing Then
                _PropertyNamesInNestedPaths.Clear()
            End If
        End If


        If _HookedObjects IsNot Nothing Then
            For RowIndex = 0 To _HookedObjects.Count - 1
                UnhookObjectsInRow(RowIndex, False, False, False)
            Next
            _HookedObjects.Clear()
        End If

        If Not _ListRaisesItemChangedEvents And _InnerList IsNot Nothing AndAlso GetType(INotifyPropertyChanged).IsAssignableFrom(_itemType) Then
            For Each obj In _InnerList
                DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Unhook ItemList.PropertyChanged: {0}", obj), 2))
                RemoveHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf ItemList_PropertyChanged
            Next
        End If

        If NotifyChangesInNestedPropertiesFromChildlists Then
            RemoveNestedBindingSources()
        End If

        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] UnhookObjects - END", ID)))
    End Sub

    Private Sub UnhookObjectsInRow(ByVal RowIndex As Integer, ByVal CheckObjectsNotInUse As Boolean, _
                                   Optional ByVal RemoveFromHookedObjects As Boolean = True, _
                                   Optional ByVal UnhookItemList As Boolean = True)

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("UnhookObjectsInRow. Index:{0}", RowIndex), 2))

        Dim N = NumNestedProperties() - 1
        If ConsiderNonNestedProperties() Then N += 1

        Dim objsConsidered As New List(Of Object)

        For i = 0 To N
            For Each obj In _HookedObjects(RowIndex).ObjInProperties(i).Objects
                If obj Is Nothing Then Continue For
                If objsConsidered.Contains(obj) Then Continue For
                objsConsidered.Add(obj)
                If TypeOf obj Is INotifyPropertyChanged And (Not CheckObjectsNotInUse OrElse Not IsObjectInUse(obj, RowIndex)) Then
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Unhook NestedObject.PropertyChanged: {0}", obj), 3))
                    RemoveHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                End If
            Next
        Next

        If UnhookItemList AndAlso Not _ListRaisesItemChangedEvents Then
            Dim obj = _HookedObjects(RowIndex).ObjRow
            If TypeOf obj Is INotifyPropertyChanged Then
                DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Unhook ItemList.PropertyChanged: {0}", obj), 3))
                RemoveHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf ItemList_PropertyChanged
            End If
        End If

        If RemoveFromHookedObjects Then
            _HookedObjects.RemoveAt(RowIndex)
        End If
    End Sub

#If DEBUG Then

    Public Sub Show_HookedObjects()
        DBG_SaltoLinea(0)
        DBG.Log(0, String.Format("[{0}] Content of _HookedObjects [DataSource:[{1}] DataMember:[{2}]]", ID, MyBase.DataSource, MyBase.DataMember))
        DBG.Log(0, "----------------------------")
        If _HookedObjects Is Nothing Then Exit Sub

        Dim i As Integer

        For row = 0 To _HookedObjects.Count - 1
            DBG.Log(0, String.Format("ROW: {0}", row))

            For i = 0 To NumNestedProperties() - 1
                DBG.Log(0, String.Format("Prop: '{0}'", CType(_propertyDescriptors(_iFirstNestedProperty + i), CustomPropertyDescriptor).PropertyPath), 2)
                For Each obj In _HookedObjects(row).ObjInProperties(i).Objects
                    DBG.Log(0, String.Format("-> {0}", obj), 4)
                Next
            Next

            If ConsiderNonNestedProperties() Then
                Dim obs = _HookedObjects(row).ObjInProperties(i).Objects
                i = 0
                For Each prop In _NonNestedPropertiesToSupervise
                    If obs(i) IsNot Nothing And obs(i) IsNot _HookedObjects(row).ObjRow Then
                        DBG.Log(0, String.Format("NonNested Prop: '{0}' -> {1}", _propertyDescriptors(prop).Name, obs(i)), 2)
                    End If
                    i += 1
                Next
            End If
        Next

        DBG_SaltoLinea(0)
    End Sub


    Public Sub Show_TypesInNestedProperties()
        DBG_SaltoLinea(0)
        DBG.Log(0, String.Format("[{0}] Content of TypesInNestedProperties [{1} - {2}]", ID, MyBase.DataSource, MyBase.DataMember))
        DBG.Log(0, "-------------------------------------")

        For i = 0 To NumNestedProperties() - 1
            DBG.Log(0, String.Format("Prop: '{0}'", CType(_propertyDescriptors(_iFirstNestedProperty + i), CustomPropertyDescriptor).PropertyPath))

            Dim propertyTypes = _TypesInNestedProperties(i)
            For j = 0 To propertyTypes.Length - 1
                DBG.Log(0, String.Format("{0}", propertyTypes(j)), 2)
            Next
        Next
        DBG_SaltoLinea(0)
    End Sub

#End If


    Private Sub HookObjectsInNestedPropertyForRow(ByVal objRow As Object, ByVal propertyPath As String, _
                                                  ByRef objsInNestedProperty As ObjectsInNestedProperty)

        DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Prop:{0}", propertyPath), 3))

        Try
            objsInNestedProperty.Objects = CustomPropertyDescriptor.GetNestedObjectsInstances(objRow, propertyPath)

            For Each obj In objsInNestedProperty.Objects
                If TypeOf obj Is INotifyPropertyChanged Then
                    RemoveHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                    AddHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Hook NestedObject.PropertyChanged: {0}", obj), 4))
                End If
            Next

        Catch ex As Exception
            OnNestedError(New NestedErrorEventArgs(ErrorType.ErrorTraversingPropertyPath, propertyPath, objRow, ex))
            objsInNestedProperty.Objects = New Object() {}
        End Try

    End Sub



    ''' <summary>
    ''' A partir de un objeto sobre el que se nos ha notificado de la modificación de una de sus propiedades (o todas si PropertyName es Nothing o String.Empty)
    ''' identificar las propiedades anidadas que pueden verse afectadas. Éstas se devuelven a través de una relación
    ''' de números que corresponden al orden de éstas en _BindingNestedProperties
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ManagePropertiesAffected(ByVal ModifiedObj As Object, ByVal propName As String, ByVal NewValue As Object, _
                                         ByRef rowsAffected As List(Of Integer))

        Dim positions As New List(Of Integer)
        Dim TypeObj As Type = ModifiedObj.GetType

        Dim PossibleObjectsToUnhook As New ArrayList  ' Por no estar tal vez ya en uso

        ' Buscamos el objeto que ha notificado el cambio dentro de los objetos anidados registrados
        For kNestedProperty = 0 To NumNestedProperties() - 1
            ' Lo primero a hacer es determinar si en la propiedad anidada podría aparecer este objeto, en base a su tipo, y de las
            ' propiedades que ella se utilizan. Y en caso de que sí, en qué posiciones.
            If CheckCanAppearInNestedProperty(TypeObj, kNestedProperty, propName, positions) Then
                ' Recorremos todos los objetos de la lista base, mirando en los objetos de esta propiedad modificada (anidada)
                ' y únicamente en la posición o posiciones en las que podría aparecer.
                For RowIndex = 0 To _HookedObjects.Count - 1
                    Dim objsInProperty As Object() = _HookedObjects(RowIndex).ObjInProperties(kNestedProperty).Objects
                    Dim obj As Object

                    For Each i In positions
                        If i = -1 Then
                            obj = _HookedObjects(RowIndex).ObjRow
                        Else
                            obj = objsInProperty(i)
                        End If

                        If obj Is ModifiedObj Then
                            If Not rowsAffected.Contains(RowIndex) Then rowsAffected.Add(RowIndex)
                            ManagePropertyAffectedInRow(RowIndex, kNestedProperty, i, NewValue, PossibleObjectsToUnhook)

                            DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Property affected: Row: {0} Prop: '{1}'", RowIndex, CType(_propertyDescriptors(_iFirstNestedProperty + kNestedProperty), CustomPropertyDescriptor).PropertyPath), 2))
                            Exit For ' Es suficiente con identificar el elemento más arriba en la cadena de objetos anidados de esta propiedad
                        End If
                    Next

                Next

                positions.Clear()
            End If
        Next


        If rowsAffected.Count > 0 Then

            ' Dejamos de escuchar el evento PropertyChanged en aquellos objetos anidados de propiedades afectadas, que dependan de propiedades que hayan
            ' cambiado, siempre y cuando esos objetos no estén en uso desde otras propiedades anidadas.
            For Each obj In PossibleObjectsToUnhook
                If Not IsObjectInUse(obj) Then
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Unhook NestedObject.PropertyChanged: {0}", obj), 2))
                    RemoveHandler DirectCast(obj, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                End If
            Next

            ' Comunicamos finalmente la modificación de las propiedades afectadas
            ' Sólo es necesario que notifiquemos un cambio por fila, aunque puedan ser varias las propiedades modificadas
            Dim CurrentItemChanged As Boolean = False
            For Each RowIndex In rowsAffected
                DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("=> MyBase.OnListChanged: Row:{0}", RowIndex), 2))
                MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, RowIndex))
            Next
        End If

    End Sub


    ''' <summary>
    ''' Toma nota de los posibles objetos a descartar (en la escucha de PropertyChanged) y actualiza la estructura _NestedObjects con los nuevos valores
    ''' </summary>
    ''' <param name="position">Posicion dentro de la jerarquía de propiedades en una propiedad anidada en donde se encuentra el objeto que ha notificado la modificación</param>
    Private Sub ManagePropertyAffectedInRow(ByVal RowIndex As Integer, ByVal kNestedProperty As Integer, ByVal position As Integer, ByVal NewValue As Object, ByVal PossibleObjectsToUnhook As ArrayList)

        DBG.Foo(DBG_ChkNivel(4) AndAlso DBG.Log(4, String.Format("[{0}] ManagePropertyAffectedInRow. Row:{1} Position:{2}", ID, RowIndex, position), 3))

        ' Si el objeto que notifica la modificación de la propiedad no es el último en la jerarquía de objetos
        ' anidados asociados a la propiedad, esto es, la propiedad modificada no es la que se está mostrando directamente
        ' en la propiedad, puede ser necesario realizar más acciones, en el caso de que ese objeto y los que le sigan en el path
        ' ya no estén en uso. 
        ' En este función nos limitaremos a añadir esos objetos a la lista PosiblesObjetosADescartar

        ' Ej: Si la propiedad anidada es "Cliente.Nombre" ([Order].Cliente.Nombre)  -> 2 posiciones
        ' => _TypesInNestedProperties(iNestedProperty) guardará los tipos de 'Cliente' y 'Nombre'. 
        ' El objeto final (que dispara eventos) será el correspondiente a Cliente
        ' El número de posiciones se mide en TypesInNestedProperties. Ahora, en _HookedObjects, en la estructura correspondiente a la fila y propiedad anidada analizada
        ' puede haber como máximo un número menos de objetos pues el último elemento de la cadena no se incluye pues no nos sirve ('Nombre' en este ejemplo)
        Dim numMaxObjects = _TypesInNestedProperties(kNestedProperty).Length - 1
        Dim IsFinalObject As Boolean = (position = numMaxObjects - 1)    ' La posición puede ser 0 (o incluso -1 si el objeto modificado es el correspondiente al datasource)

        If Not IsFinalObject Then
            Dim objsProp As ObjectsInNestedProperty = _HookedObjects(RowIndex).ObjInProperties(kNestedProperty)

            ' Recorremos los objetos anidados almacenados en la propiedad analizada, comenzando por el primer objeto afectado, esto es, por el siguiente a aquel que
            ' ha generado la notificación.
            ' Ej: [Order1.] "Cliente.Nombre"  Si posicion = -1 => el objeto modificado es Order1 sobre su propiedad Cliente
            For i = position + 1 To objsProp.Objects.Length - 1
                Dim obj = objsProp.Objects(i)
                If obj IsNot Nothing AndAlso Not PossibleObjectsToUnhook.Contains(obj) Then
                    PossibleObjectsToUnhook.Add(obj)
                    DBG.Foo(DBG_ChkNivel(4) AndAlso DBG.Log(4, String.Format("Possible object to discard: {0}", obj), 3))
                End If
            Next

            ' Actualizamos nuestra estructura con los nuevos objetos para esta propiedad
            If position = numMaxObjects - 2 Then   ' -1 correspondería al 
                ' Si el objeto modificado es el inmediatamente anterior al objeto final tan sólo tendremos que indicar cuál es el nuevo objeto final, que tendremos en NewValue
                _HookedObjects(RowIndex).ObjInProperties(kNestedProperty).Objects(position + 1) = NewValue
                If TypeOf NewValue Is INotifyPropertyChanged Then
                    RemoveHandler DirectCast(NewValue, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                    AddHandler DirectCast(NewValue, INotifyPropertyChanged).PropertyChanged, AddressOf NestedObject_PropertyChanged
                    DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("Hook NestedObject.PropertyChanged: {0}", NewValue), 3))
                End If
            Else
                Dim propPath = CType(_propertyDescriptors(_iFirstNestedProperty + kNestedProperty), CustomPropertyDescriptor).PropertyPath
                HookObjectsInNestedPropertyForRow(_HookedObjects(RowIndex).ObjRow, propPath, _HookedObjects(RowIndex).ObjInProperties(kNestedProperty))
            End If

        Else
            ' Si el objeto es final entonces no es necesario modificar la estructura de objetos en esta propiedad anidada
        End If

    End Sub


    Private Function IsObjectInUse(ByVal Obj As Object, Optional ByVal IgnoreRow As Integer = -1) As Boolean

        ' Para no tener que mirar dentro de la jerarquía de objetos 'nested' correspondiente a cada propiedad
        ' excluiremos aquellas propiedades en las que ninguno de esos objetos sea de un tipo compatible
        Dim positions As New List(Of Integer)

        For kNestedProperty = 0 To NumNestedProperties() - 1
            positions.Clear()
            If Not CheckCanAppearInNestedProperty(Obj.GetType, kNestedProperty, Nothing, positions) Then Continue For

            For RowIndex = 0 To _HookedObjects.Count - 1
                If RowIndex = IgnoreRow Then Continue For

                Dim objsInProperty As Object() = _HookedObjects(RowIndex).ObjInProperties(kNestedProperty).Objects
                Dim o As Object

                ' Miramos en las únicas posiciones que deben ser consideradas
                For Each i In positions
                    If i < 0 Then
                        o = _HookedObjects(RowIndex).ObjRow
                    Else
                        o = objsInProperty(i)
                    End If

                    If o Is Obj Then
                        DBG.Foo(DBG_ChkNivel(4) AndAlso DBG.Log(4, String.Format("[{0}] IsObjectInUse ({1}) ->TRUE", ID, Obj), 2))
                        Return True
                    End If
                Next
            Next
        Next

        If ConsiderNonNestedProperties() Then
            Dim iNonNestedProps = NumNestedProperties()    ' Último elemento en _HookedObjects(row).ObjInProperties
            For RowIndex = 0 To _HookedObjects.Count - 1
                If RowIndex = IgnoreRow Then Continue For
                For Each o In _HookedObjects(RowIndex).ObjInProperties(iNonNestedProps).Objects
                    If o Is Obj Then
                        DBG.Foo(DBG_ChkNivel(4) AndAlso DBG.Log(4, String.Format("[{0}] IsObjectInUse ({1}) ->TRUE", ID, Obj), 2))
                        Return True
                    End If
                Next
            Next
        End If

        DBG.Foo(DBG_ChkNivel(4) AndAlso DBG.Log(4, String.Format("[{0}] IsObjectInUse ({1}) ->FALSE", ID, Obj), 2))
        Return False

    End Function


    ''' <summary>
    ''' Carga en _PropertyNamesInNestedPaths los nombres de las propiedades que se escuchan para atender las modificaciones en 
    ''' propiedades anidadas.
    ''' </summary>
    ''' <remarks>
    ''' Se usa omo ayuda para intentar discriminar más rápidamente qué cambios en qué propiedades podemos ignorar tranquilamente
    ''' y cuáles no. No podemos simplemente cargar una estructura como un diccionario por ejemplo con las propiedades que escuchamos
    ''' de los diferentes tipos, porque no podremos hacer una búsqueda de los tipos por igualdad, ya que aunque una propiedad sea del tipo
    ''' T el objeto asignado a la misma no tiene por qué ser T sino cualquier tipo que pueda ser asignado a T.
    ''' </remarks>
    Private Sub FillPropertyNamesInNestedPaths()
        If _iFirstNestedProperty < 0 Then Exit Sub

        If _PropertyNamesInNestedPaths Is Nothing Then
            _PropertyNamesInNestedPaths = New List(Of String)
        End If

        For i = _iFirstNestedProperty To _propertyDescriptors.Count - 1
            Dim propDesc = CType(_propertyDescriptors(i), CustomPropertyDescriptor)

            For Each prop In propDesc.PropertyPath.Split("."c)
                If Not _PropertyNamesInNestedPaths.Contains(prop) Then
                    _PropertyNamesInNestedPaths.Add(prop)
                End If
            Next
        Next

        _PropertyNamesInNestedPaths.Sort()
    End Sub

    ''' <summary>
    ''' Comprobar si el objeto puede aparecer en la propiedad anidada indicada, atendiendo al tipo del mismo y de la propiedad sobre la que se ha notificado
    ''' el cambio. Devuelve la lista de posiciones en la propiedad anidada, en donde podría aparecer
    ''' </summary>
    ''' <remarks>La propiedad puede venir a Nothing, indicando que todas (o cualquiera..) las propiedades han cambiado</remarks>
    Private Function CheckCanAppearInNestedProperty(ByVal TypeObj As Type, _
                                                    ByVal kNestedProperty As Integer, ByVal PropertyModifiedName As String, _
                                                    ByVal Positions As IList(Of Integer)) As Boolean

        DBG.Foo(DBG_ChkNivel(4) AndAlso DBG.Log(4, String.Format("[{0}] CheckCanAppearInNestedProperty: TypeObj={1} Prop='{2}'", ID, TypeObj, CType(_propertyDescriptors(_iFirstNestedProperty + kNestedProperty), CustomPropertyDescriptor).PropertyPath), 2))


        Dim result As Boolean = False

        Dim PropertiesInPath As String() = CType(_propertyDescriptors(_iFirstNestedProperty + kNestedProperty), CustomPropertyDescriptor).PropertyPath.Split("."c)
        Dim propertyTypes = _TypesInNestedProperties(kNestedProperty)

        If TypeObj Is _itemType Then
            DBG.Foo(DBG_ChkNivel(5) AndAlso DBG.Log(5, String.Format("Same type as the items of inner list"), 3))
            If (PropertyModifiedName Is Nothing) Or PropertiesInPath(0) = PropertyModifiedName Then
                Positions.Add(-1)
                result = True
            End If
        End If

        ' El tipo de la última propiedad del path realmente no nos interesa
        For i = 0 To propertyTypes.Length - 2
            If (PropertyModifiedName Is Nothing Or PropertiesInPath(i + 1) = PropertyModifiedName) AndAlso propertyTypes(i).IsAssignableFrom(TypeObj) Then
                Positions.Add(i)
                result = True
            End If
        Next

        DBG.Foo(DBG_ChkNivel(4) AndAlso DBG.Log(4, String.Format("Result: {0} positions", Positions.Count), 3))
        Return result
    End Function


    ''' <remarks>
    ''' La propiedad recibida puede venir a Nothing (El evento PropertyChanged puede indicar que todas las propiedades del objeto 
    ''' han cambiado utilizando null (Nothing en Visual Basic) o String.Empty como el nombre de propiedad en PropertyChangedEventArgs.)
    ''' 
    ''' Aunque se llama NestedObject_... y lo normal será que se escuchen cambios sobre propiedades de objetos incluidos en los paths de
    ''' propiedades anidadas, también puede atenderse cambios de algunos objetos que a lo mejor no aparecen en esas propiedades anidadas
    ''' (o también sí) pero sí en alguna de las otras propiedades, e implementa INotifyPropertyChanged
    ''' </remarks>
    Protected Sub NestedObject_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        Try
            Dim objType As Type = sender.GetType
            Dim NewValue As Object = Nothing
            Dim rowsAffected As New List(Of Integer)


            DBG.Foo(DBG_ChkNivel(2) AndAlso DBG.Log(2, String.Format("[{0}] NestedObject_PropertyChanged: Object:[{1}] Prop:'{2}'", ID, sender, e.PropertyName)))


            ' Gestionar la posible modificación que pueda afectar a una propiedad anidada
            '-----------------------------
            Dim considerNestedChange = (_iFirstNestedProperty >= 0)
            Dim propertyIndiv = (e IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.PropertyName))

            If considerNestedChange And propertyIndiv Then
                considerNestedChange = _PropertyNamesInNestedPaths.Contains(e.PropertyName)
                'NewValue = TypeDescriptor.GetProperties(objType).Find(e.PropertyName, True).GetValue(sender)
                NewValue = DynamicAccessorFactory.GetDynamicAccessor(objType).GetPropertyValue(sender, e.PropertyName)
            End If
            If considerNestedChange Then
                ManagePropertiesAffected(sender, e.PropertyName, NewValue, rowsAffected)
            End If



            ' Aparte pueden verse afectadas aquellas propiedades no anidadas que estén mostrando el objeto que ha sido modificado (con la ayuda de
            ' un desplegable o similar)
            ' De hecho podría no estarse utilizando ninguna propiedad anidada. En ese caso si hemos entrado aquí será porque nos hemos subscrito
            ' a PropertyChanged para determinados objetos, en propiedades no anidadas y que implementan INotifyPropertyChanged 
            '----------------------------------------------------
            If ConsiderNonNestedProperties() Then
                Dim Properties As New List(Of String)
                For Each prop In _NonNestedPropertiesToSupervise
                    If _propertyDescriptors(prop).PropertyType.IsAssignableFrom(objType) Then
                        Properties.Add(prop)
                    End If
                Next

                If Properties.Count > 0 Then
                    For RowIndex = 0 To MyBase.List.Count - 1
                        If rowsAffected.Contains(RowIndex) Then Continue For ' Ya se ha informado de cambios en esta fila
                        For Each prop In Properties
                            If sender Is _propertyDescriptors(prop).GetValue(MyBase.List(RowIndex)) Then
                                DBG.Foo(DBG_ChkNivel(2) AndAlso DBG.Log(2, String.Format("[{0}] => MyBase.OnListChanged: Row:{1} (Change in object of non nested property:{2})", ID, RowIndex, prop), 2))
                                rowsAffected.Add(RowIndex)
                                MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, RowIndex))
                                Exit For      ' Ya hemos indicado que ha cambiado esa fila. No es necesario seguir buscando otras propiedades
                            End If
                        Next
                    Next
                End If
            End If

        Catch ex As Exception
            DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("[0] ERROR on OnListChanged: {1}: Prop:{2}  {3}]", ID, sender, e.PropertyName, DBG.MessageError(ex))))
            OnNestedError(New NestedErrorEventArgs(ErrorType.ErrorOnPropertyChanged, e.PropertyName, sender, ex))
        End Try
    End Sub


    Protected Overrides Sub OnListChanged(ByVal E As ListChangedEventArgs)
        Select Case E.ListChangedType
            Case ListChangedType.ItemChanged
                If NotifyPropertyChanges Then
                    DBG.Foo(DBG_ChkNivel(2) AndAlso DBG.Log(2, String.Format("[{0}] OnListChanged [ItemChanged]: {1}", ID, E.NewIndex)))
                    ItemChanged(E.NewIndex, MyBase.List(E.NewIndex), Nothing, True)
                End If

            Case ListChangedType.ItemDeleted
                If NotifyPropertyChanges Then
                    DBG.Foo(DBG_ChkNivel(2) AndAlso DBG.Log(2, String.Format("[{0}] OnListChanged [ItemDeleted]: {1}", ID, E.NewIndex)))
                    If (_iFirstNestedProperty >= 0 Or ConsiderNonNestedProperties()) AndAlso _HookedObjects IsNot Nothing AndAlso _HookedObjects.Count > E.NewIndex Then
                        Dim ItemDeleted = _HookedObjects(E.NewIndex).ObjRow
                        UnhookObjectsInRow(E.NewIndex, True)

                        If NotifyChangesInNestedPropertiesFromChildlists Then
                            RemoveNestedBindingSources(ItemDeleted)
                        End If
                    End If
                End If

            Case ListChangedType.ItemAdded
                If NotifyPropertyChanges AndAlso E.NewIndex <= MyBase.Count - 1 Then
                    Dim ItemAdded = MyBase.List(E.NewIndex)
                    DBG.Foo(DBG_ChkNivel(2) AndAlso DBG.Log(2, String.Format("[{0}] OnListChanged [ItemAdded]: {1}", ID, ItemAdded)))
                    If Not IsConfigured() Then
                        WireObjects(True)

                    Else
                        HookObjectsInRow(ItemAdded, E.NewIndex)
                        If NotifyChangesInNestedPropertiesFromChildlists And Not ConsiderChildsOnlyInCurrent Then
                            CreateNestedBindingSources(ItemAdded)
                        End If
                    End If
                End If

            Case ListChangedType.Reset
                DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] OnListChanged [Reset]", ID)))
                WireObjects()

            Case ListChangedType.PropertyDescriptorChanged
                DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] OnListChanged [PropertyDescriptorChanged]", ID)))
                CreatePropertyDescriptors()
        End Select


        ' Nota: Si hacemos un cambio sobre un item de una lista IBindingList recibiremos aquí un ListChangedType.ItemChanged donde
        ' PropertyDescriptor no es Nothing sino que hará referencia a la propiedad modificada. En ese caso si tenemos propiedades
        ' anidadas probablemente no serán refrescadas, pues muchos controles (ej. DataGridView) sólo refrescarán los campos que muestren
        ' esa propiedad.
        ' Si la propiedad modificada viene de un IList simplemente, ya seamos nosotros desde ObjectBindingSource, ya el propio BindingSource
        ' base, recibiremos Nothing como PropertyChanged.

        DBG.Foo(DBG_ChkNivel(2) AndAlso DBG.Log(2, String.Format("=> MyBase.OnListChanged {1}", ID, E.ListChangedType), 2))
        If E.ListChangedType = ListChangedType.ItemChanged And E.PropertyDescriptor IsNot Nothing _
            AndAlso (_iFirstNestedProperty >= 0 AndAlso TypeOf MyBase.List(E.NewIndex) Is INotifyPropertyChanged) Then
            MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, E.NewIndex))
        Else
            MyBase.OnListChanged(E)
        End If
    End Sub


    Private Sub ItemList_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        ' Si la lista genera eventos ListChanged (podremos saberlo si la lista implementa IRaiseItemChangedEvents,
        ' consultando la propiedad RaisesItemChangedEvents. Si no lo implementa, entonces se considera que los generará
        ' si implementa IBindingList (no basta con que implemente IList).
        ' Aun en el caso de que la lista no genere esos eventos de modificación la implementación de BindingSource (de
        ' la que este componente hereda) hará que se escuchen los cambios a las propiedades del item actual, Current,
        ' en el caso de que éste implemente INotifyPropertyChanged. Esto es así porque el BindingSource se subscribirá a 
        ' dichos eventos.
        ' Este componente ObjectBindingSource ampliará esta escucha no sólo al item actual sino a todos los demás items
        ' de la lista.

        ItemChanged(MyBase.IndexOf(sender), sender, e.PropertyName, False)
    End Sub

    Private Sub ItemChanged(ByVal RowIndex As Integer, ByVal sender As Object, ByVal PropertyName As String, ByVal InvokedByOnListChanged As Boolean)

        UnhookObjectsInRow(RowIndex, True, False)
        HookObjectsInRow(sender, RowIndex, True)

        If Not InvokedByOnListChanged Then
            DBG.Foo(DBG_ChkNivel(2) AndAlso DBG.Log(2, String.Format("[{0}] => MyBase.OnListChanged: Row:{1}", ID, RowIndex)))
            MyBase.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, RowIndex))
        End If


        ' Si estamos haciendo uso de componentes ObjectBindingSource anidados para poder notificar cambios en propiedades
        ' anidadas de listas hijas, deberemos comprobar si alguna de estas listas ha sido reemplazada por otra. Si es así la
        ' eliminaremos y volveremos a establecer.

        If NotifyChangesInNestedPropertiesFromChildlists AndAlso _
           (Not ConsiderChildsOnlyInCurrent OrElse RowIndex = MyBase.Position) Then

            If String.IsNullOrEmpty(PropertyName) Then
                If RemoveNestedBindingSources(sender) Then
                    CreateNestedBindingSources(sender)
                End If

            Else
                Dim prop = _propertyDescriptors.Find(PropertyName, False)
                If prop IsNot Nothing AndAlso (GetAssociatedOBS(prop) IsNot Nothing) Then
                    Dim NewValue = TypeDescriptor.GetProperties(_itemType).Find(PropertyName, True).GetValue(sender)
                    If RemoveNestedBindingSources(sender, prop, NewValue) Then  ' Sólo se eliminará si realmente la lista es otra, no si a esta lista se le han añadido o eliminado elementos
                        CreateNestedBindingSources(sender, prop)
                    End If
                End If
            End If

        End If
    End Sub

    Protected Overrides Sub OnCurrentChanged(ByVal e As System.EventArgs)
        If _currentItem Is MyBase.Current Then Exit Sub

        Dim oldCurrent = _currentItem
        _currentItem = MyBase.Current

        If NotifyPropertyChanges Then
            If Not IsConfigured() Then
                WireObjects()

            Else
                If NotifyChangesInNestedPropertiesFromChildlists And ConsiderChildsOnlyInCurrent Then
                    RemoveNestedBindingSources(oldCurrent)
                    CreateNestedBindingSources(_currentItem)
                End If
            End If
        End If

        MyBase.OnCurrentChanged(e)
    End Sub


    Protected Overridable Sub OnConfigurationChanged()
        RaiseEvent ConfigurationChanged(Me)
    End Sub


    Protected Overridable Sub OnNestedError(ByVal e As NestedErrorEventArgs)
        RaiseEvent NestedError(Me, e)
    End Sub


    Private Sub CleanUP()
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] DISPOSE: CleanUP", ID)))
        UnwireObjects(False)
        UnWireRelatedObjectBindingSources()
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
        Public PropertyDesc As PropertyDescriptor
    End Structure

    ' Internal array with the nested ObjectBindingSource components that manage the PropertyDescriptors of one
    ' property associated to a child list. This components are used as templates to the new ones that will be dinamically 
    ' created and referenced in _NestedBindingSources
    Private _RelatedObjectBindingSources As ObjectBindingSource() = New ObjectBindingSource() {}
    Private _AssociatedOBS As Dictionary(Of PropertyDescriptor, ObjectBindingSource) = Nothing

    ' References each of the ObjectBindingSource component that provide support for changes in nested property accessors in child lists,
    ' via the listening of INotifyPropertyChanges.
    ' This components will be created only if the property 'NotifyChangesInNestedPropertiesFromChildlists' is set to True.
    ' Otherwise, it will only be used the components in _RelatedBindingSource, to facilitate the property accesors.
    Private _NestedBindingSources As New Dictionary(Of ObjectBindingSource, NestedRelatedBindingInf)


    ''' <summary>
    ''' Gets or sets the related/nested components ObjectBindingSource that offer the PropertyDescriptors for the properties
    ''' associated to child lists. The properties will be associated automatically from the type of the list item.
    ''' </summary>
    ''' <remarks>
    ''' This way the listAccesors provided can also expose nested properties.
    ''' This is useful with UI controls that show not only the properties of the datasource objects itself, but also can show in
    ''' another level (for example, 'bands' in Infragistics's UltraGrid) the properties of the child objects
    ''' This components will be used as templates to the new ones that will be dinamically created and referenced 
    ''' in _NestedBindingSources if the property NotifyChangesInNestedPropertiesFromChildlists is set to True.
    ''' </remarks>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    <Category("Data")> _
    <Description("Related/nested components ObjectBindingSource that offer the PropertyDescriptors for the properties associated to child lists." + _
                 "The properties will be associated automatically from the type of the list item.")> _
    <Editor(GetType(SelectionObjectsBindingSourcesEditor), GetType(UITypeEditor))> _
    Public Property RelatedObjectBindingSources() As ObjectBindingSource()
        Get
            Return _RelatedObjectBindingSources
        End Get

        Set(ByVal value As ObjectBindingSource())
            If IsInitialized() Then RemoveNestedBindingSources()

            UnWireRelatedObjectBindingSources()

            _RelatedObjectBindingSources = value

            WireRelatedObjectBindingSources()

            If IsInitialized() Then
                PopulateAssociatedOBS()
                If NotifyPropertyChanges And NotifyChangesInNestedPropertiesFromChildlists Then
                    CreateNestedBindingSources()
                End If
            End If
        End Set
    End Property


    Private Sub UnWireRelatedObjectBindingSources()
        If _RelatedObjectBindingSources Is Nothing Then Exit Sub

        For Each rBS In _RelatedObjectBindingSources
            RemoveHandler rBS.ConfigurationChanged, AddressOf RelatedOBS_ConfigurationChanged
        Next
    End Sub

    Private Sub WireRelatedObjectBindingSources()
        If _RelatedObjectBindingSources Is Nothing Then Exit Sub

        For Each rBS In _RelatedObjectBindingSources
            AddHandler rBS.ConfigurationChanged, AddressOf RelatedOBS_ConfigurationChanged
        Next
    End Sub


    Private Function GetAssociatedOBS(ByVal prop As PropertyDescriptor) As ObjectBindingSource
        If _AssociatedOBS Is Nothing Then Return Nothing

        Dim oBS As ObjectBindingSource = Nothing
        _AssociatedOBS.TryGetValue(prop, oBS)
        Return oBS
    End Function


    Private Sub PopulateAssociatedOBS()
        If _RelatedObjectBindingSources Is Nothing Or _propertyDescriptors Is Nothing Then Exit Sub

        If _AssociatedOBS Is Nothing Then
            _AssociatedOBS = New Dictionary(Of PropertyDescriptor, ObjectBindingSource)
        Else
            _AssociatedOBS.Clear()
        End If

        For Each oBS In _RelatedObjectBindingSources
            Dim obsItemType As Type = ListBindingHelper.GetListItemType(oBS.DataSource, oBS.DataMember)

            For Each prop As PropertyDescriptor In _propertyDescriptors
                ' Si se ha limitado las propiedades de tipo lista sobre las que deben escucharse los cambios, comprobaremos si esta propiedad
                ' está en esa relación.
                If _ChildListsToConsider IsNot Nothing AndAlso Array.IndexOf(_ChildListsToConsider, prop.Name) < 0 Then Continue For

                If _ChildListsToConsider Is Nothing AndAlso Not GetType(IList).IsAssignableFrom(prop.PropertyType) Then Continue For

                If ListBindingHelper.GetListItemType(prop.PropertyType) Is obsItemType Then
                    _AssociatedOBS.Add(prop, oBS)
                End If
            Next

        Next
    End Sub


    Private Sub CreateNestedBindingSources()
        If _AssociatedOBS Is Nothing Or Not NotifyPropertyChanges Then Exit Sub

        If ConsiderChildsOnlyInCurrent Then
            CreateNestedBindingSources(_currentItem)
        Else
            For Each objRow In _InnerList
                CreateNestedBindingSources(objRow)
            Next
        End If
    End Sub

    Private Sub CreateNestedBindingSources(ByVal objRow As Object, Optional ByVal propDesc As PropertyDescriptor = Nothing)
        If objRow Is Nothing Or _AssociatedOBS Is Nothing Or Not NotifyPropertyChanges Then Exit Sub

        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] CreateNestedBindingSources: {1}", ID, objRow)))

        For Each prop As PropertyDescriptor In _propertyDescriptors
            If Not (propDesc Is Nothing OrElse prop Is propDesc) Then Continue For
            Dim refOBS = GetAssociatedOBS(prop)
            If refOBS Is Nothing Then Continue For

            Dim list = prop.GetValue(objRow)
            If list IsNot Nothing Then
                Dim oBS = New ObjectBindingSource()

                DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] Created New NestedBindingSource: {1}", ID, oBS.ID)))

                _NestedBindingSources.Add(oBS, New NestedRelatedBindingInf With {.ObjRow = objRow, .PropertyDesc = prop})

                DBG.Foo(DBG_ChkNivel(3) AndAlso DBG.Log(3, String.Format("[{0}] Listening Changes on: {1}", ID, list)))
                AddHandler oBS.ListChanged, AddressOf ChildList_ListChanged
                AddHandler oBS.ListChangedOnChildList, AddressOf ChildList_ListChangedOnChildList


                DirectCast(oBS, ISupportInitialize).BeginInit()
                With oBS
                    .AutoCreateObjects = refOBS.AutoCreateObjects
                    .NotifyChangesInNestedPropertiesFromChildlists = refOBS.NotifyChangesInNestedPropertiesFromChildlists
                    .BindableNestedProperties = refOBS.BindableNestedProperties
                    .RelatedObjectBindingSources = refOBS.RelatedObjectBindingSources
                    .ChildListsToConsider = refOBS.ChildListsToConsider
                    .ConsiderChildsOnlyInCurrent = refOBS.ConsiderChildsOnlyInCurrent
                    .NotifyPropertyChanges = refOBS.NotifyPropertyChanges
                    .NonNestedPropertiesToSupervise = refOBS.NonNestedPropertiesToSupervise
                    .DataSource = list
                End With
                DirectCast(oBS, ISupportInitialize).EndInit()

            End If

        Next

    End Sub


    ''' <summary>
    ''' Indicates if the component must offer support for changes in nested properties of child lists, via the listening
    ''' of INotifyPropertyChanges.
    ''' </summary>
    ''' <remarks>If set to True then it will be created new ObjectBindingSource components to manage that changes (one for each child
    ''' list to supervise). And also, changes in non nested properties of this child lists will be notified even if that lists don't
    ''' implement IBindingList.
    ''' Otherwise, it will only be used the components in _RelatedBindingSource to facilitate the property accesors.
    ''' </remarks>
    <Category("Data")> _
    <Description("Indicates if the component must offer support for changes in nested properties of child lists, via the listening of INotifyPropertyChanges.")> _
    Public Property NotifyChangesInNestedPropertiesFromChildlists() As Boolean
        Get
            Return _NotifyChangesFromChildLists
        End Get
        Set(ByVal value As Boolean)
            If value <> _NotifyChangesFromChildLists Then
                _NotifyChangesFromChildLists = value
                If Not IsInitialized() Then Exit Property

                If value Then
                    CreateNestedBindingSources(_currentItem)
                Else
                    RemoveNestedBindingSources()
                End If
            End If
        End Set
    End Property
    Private _NotifyChangesFromChildLists As Boolean = False


    ''' <summary>
    ''' Gets or sets the list properties (implementing IList) for which nested components (ObjectBindingSource) must
    ''' be created in order to detect changes in nested properties of that child lists. If not set, then a component
    ''' will be created for each list property (if NotifyChangesFromChildLists is set to True).
    ''' </summary>
    ''' <remarks>
    ''' This property will be ignored if <see cref="NotifyChangesInNestedPropertiesFromChildlists"/> is set to false.
    ''' 
    ''' In addition, there must be an ObjectBindingSource component, in <see cref="RelatedObjectBindingSources"/>, that has the same type
    ''' of list item.
    ''' </remarks>
    <Category("Data")> _
    <Description("List properties for which nested components (ObjectBindingSource) must be created in order to detect changes " + _
                 "in nested properties of that child lists. If not set, then a component will be created for each list property (if NotifyChangesFromChildLists is set to True).")> _
    Public Property ChildListsToConsider() As String()
        Get
            Return _ChildListsToConsider
        End Get
        Set(ByVal value As String())
            If value IsNot Nothing AndAlso value.Length = 0 Then
                value = Nothing
            End If
            _ChildListsToConsider = value

            If IsInitialized() Then
                If NotifyPropertyChanges And NotifyChangesInNestedPropertiesFromChildlists Then
                    Reconfigure(False, True)
                Else
                    If Me.DesignMode Then CheckChildListsToConsider()
                End If
            End If
        End Set
    End Property
    Private _ChildListsToConsider As String() = Nothing


    Private Sub CheckChildListsToConsider()
        If _ChildListsToConsider Is Nothing Then Exit Sub

        Dim auxList As New List(Of String)

        For Each clProp In _ChildListsToConsider
            Dim prop = _propertyDescriptors.Find(clProp, True)
            If prop Is Nothing OrElse Not GetType(IList).IsAssignableFrom(prop.PropertyType) Then
                Dim cadErr = String.Format("[{0}] ERROR setting ChildListsToConsider: '{1}' is not a valid list property (It will be ignored)", ID, clProp)
                DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, cadErr))
                If Me.DesignMode Then
                    MsgBox(cadErr, MsgBoxStyle.Exclamation, "ObjectBindingSource")
                    auxList.Add(clProp)
                End If
            Else
                auxList.Add(prop.Name)
            End If
        Next

        _ChildListsToConsider = auxList.ToArray
    End Sub


    ''' <summary>
    ''' Indicates that, in case that the component must offer support for changes in nested properties of child lists
    ''' (see <see cref="NotifyChangesInNestedPropertiesFromChildlists"/> then this changes will only be supervised for
    ''' the child lists of the current record (default behaviour). 
    ''' If set to false then changes in child lists from all records will be supervised.
    ''' </summary>
    ''' <remarks>
    ''' Depending of this property the number of nested ObjectBindingSource components will vary with the number of 
    ''' items in the list.
    ''' </remarks>
    <Category("Data")> _
    <Description("Indicates that, in case that the component must offer support for changes in nested properties of child lists " + _
     "then this changes will only be supervised for the child lists of the current record. If set to false then changes in child lists from all records will be supervised.")> _
    Public Property ConsiderChildsOnlyInCurrent() As Boolean
        Get
            Return _ConsiderChildsOnlyInCurrent
        End Get
        Set(ByVal value As Boolean)
            If _ConsiderChildsOnlyInCurrent = value Then Exit Property

            _ConsiderChildsOnlyInCurrent = value

            If _NotifyPropertyChanges And _NotifyChangesFromChildLists And IsInitialized() Then
                CreatePropertyDescriptors()
                WireObjects(True)
                OnListChanged(New ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, 0))
            End If

        End Set
    End Property
    Private _ConsiderChildsOnlyInCurrent As Boolean = True


    Private Sub RemoveNestedBindingSources()
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] RemoveNestedBindingSources", ID)))
        If _NestedBindingSources IsNot Nothing Then
            For Each oBS In _NestedBindingSources.Keys
                RemoveHandler oBS.ListChanged, AddressOf ChildList_ListChanged
                RemoveHandler oBS.ListChangedOnChildList, AddressOf ChildList_ListChangedOnChildList
                oBS.Dispose()
            Next
            _NestedBindingSources.Clear()
        End If
    End Sub

    Private Function RemoveNestedBindingSources(ByVal objRow As Object, Optional ByVal propDesc As PropertyDescriptor = Nothing, Optional ByVal NewValue As Object = Nothing) As Boolean
        If objRow Is Nothing Then Return False

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

        For Each oBS In toRemove
            DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] Removed and Disposed NestedBindingSource({1})", ID, oBS), 1))
            _NestedBindingSources.Remove(oBS)
            RemoveHandler oBS.ListChanged, AddressOf ChildList_ListChanged
            RemoveHandler oBS.ListChangedOnChildList, AddressOf ChildList_ListChangedOnChildList
            oBS.Dispose()
        Next
        Return result
    End Function


    ''' <summary>
    ''' Escucharemos cambios sobre los componentes en RelatedObjectBindingSource, por si se han modificado alguna de sus propiedades, para que se tenga 
    ''' en cuenta y se refleje sobre los distintos componentes anidados.
    ''' </summary>
    Private Sub RelatedOBS_ConfigurationChanged(ByVal sender As Object)
        DBG.Foo(DBG_ChkNivel(1) AndAlso DBG.Log(1, String.Format("[{0}] RelatedOBS_ConfigurationChanged", ID)))
        Reconfigure(True, True)
    End Sub

    Private Sub ChildList_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
        ChangeOnChildList(sender, e)
    End Sub

    Private Sub ChildList_ListChangedOnChildList(ByVal rowChild As Integer, ByVal sender As Object, ByVal e As ListChangedEventArgs)
        ChangeOnChildList(sender, e)
    End Sub

    Private Sub ChangeOnChildList(ByVal sender As Object, ByVal e As ListChangedEventArgs)
        Dim relatedBindingInf As NestedRelatedBindingInf = Nothing
        Dim oBS = CType(sender, ObjectBindingSource)

        If _NestedBindingSources.TryGetValue(oBS, relatedBindingInf) Then
            Dim RowIndex = MyBase.List.IndexOf(relatedBindingInf.ObjRow)
            DBG.Foo(DBG_ChkNivel(2) AndAlso DBG.Log(2, String.Format("[{0}] ChangeOnChildList: {1}  (Row:{2})", ID, oBS.ID, RowIndex)))
            If RowIndex >= 0 Then
                OnListChangedOnChildList(RowIndex, Me, e)
            Else
                DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("[{0}] ERROR on ChangeOnChildList. Object not found: {1}", CType(sender, ObjectBindingSource).ID, relatedBindingInf.ObjRow)))
            End If

        Else
            DBG.Foo(DBG_ChkNivel(0) AndAlso DBG.Log(0, String.Format("[{0}] ERROR on ChangeOnChildList. ObjectBindingSource not found in _NestedBindingSources", CType(sender, ObjectBindingSource).ID)))
        End If

    End Sub


    Protected Sub OnListChangedOnChildList(ByVal RowIndex As Integer, ByVal sender As Object, ByVal e As ListChangedEventArgs)
        If MyBase.RaiseListChangedEvents Then
            RaiseEvent ListChangedOnChildList(RowIndex, sender, e)
        End If
    End Sub

#End Region

#Region "Fix problem Disposing BindingSource"

    ' BindingSource disposing and related BindingSources (http://connect.microsoft.com/VisualStudio/feedback/details/434798/bindingsource-disposing-and-related-bindingsources)
    '----------------------------------------
    ' When BindingSource gets disposed, the related BindingSources do not get disposed.
    ' Also, BindingSource.DataSource is set to another BindingSource, the assigned (right-hand side) should register
    ' the assignee as a related BindingSource. This seems reasonable given the functionality of GetRelatedCurrencyManager
    ' method. That also goes to the constructor which takes a datasource and datamember - it should register as related
    ' BindingSource.
    '
    '  All in all, there is no mechanism to handle DataSource disposal. BindingSource should either:
    ' - dispose if DataSource is disposed
    ' - provide a hook so the user can determine whether to dispose or simply sever DataSource
    '
    ' Right now, it's not easy tracking them. 

    ' This is sort of a workaround - I say that since it only works if somehow GetRelatedCurrencyManager was called to create the child BindingSource.
    ' That seems to be the only time the relationship is tracked. For example, the constructor does not create this relationship, nor does DataSource 
    ' property.

    ' BindingSource on Disposing does not reset lastCurrentItem field (https://connect.microsoft.com/VisualStudio/feedback/details/434746/bindingsource-on-disposing-does-not-reset-lastcurrentitem-field)
    '---------------------------------------
    ' When BindingSource gets disposed, lastCurrentItem field does not get set to null.
    ' The problem is that it may hold a reference to an object and prevents that object from being garbage-collected. There are cases 
    ' where the references are cyclical and it is in those cases, that gets the BindingSource and the anything that these BindingSources
    ' are bound to (via lastCurrentItem), pinned.

    ' The only time lastCurrentItem gets set to null is when Current changes (via ParentCurrencyManager_CurrentItemChanged). My workaround
    ' is to create a subclass of BindingSource and override Dispose to set DataSource to null. This way, events are still hooked up and the
    ' above handler gets called, setting lastCurrentItem to null. Then base.Dispose will unhook events as before. This only works if 
    ' DataSource implements ICurrencyManagerProvider but many do not, such as a simple List. So it's a workaround that only works for certain 
    ' scenarios. 


    ''' <summary> 
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing AndAlso (Not Me.components Is Nothing)) Then
            CleanUP()
            FixBindingSourceDispose(Me)
            Me.components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub FixBindingSourceDispose(ByVal bs As BindingSource)
        Dim relatedBindingSourcesField As FieldInfo = _
                      GetType(BindingSource).GetField("relatedBindingSources", BindingFlags.Instance Or BindingFlags.NonPublic)
        Dim relatedBindingSources = CType(relatedBindingSourcesField.GetValue(bs), Dictionary(Of String, BindingSource))
        If relatedBindingSources IsNot Nothing Then
            For Each key As String In relatedBindingSources.Keys
                Dim bsRelated = relatedBindingSources(key)
                FixBindingSourceDispose(bsRelated)
                bsRelated.Dispose()
            Next
        End If

        Dim lastCurrentItemField As FieldInfo = GetType(BindingSource).GetField("lastCurrentItem", BindingFlags.Instance Or BindingFlags.NonPublic)
        If (lastCurrentItemField IsNot Nothing) Then
            lastCurrentItemField.SetValue(bs, Nothing)
        End If

    End Sub


#End Region

End Class

Public Enum ErrorType
    PropertyUnrecognized = 0
    ErrorOnPropertyChanged = 1
    ErrorTraversingPropertyPath = 2
End Enum

Public Class NestedErrorEventArgs
    Inherits EventArgs

    Public ErrorType As ErrorType
    Public Exception As Exception
    Public PropertyPath As String
    Public [Object] As Object

    Public Sub New(ByVal ErrorType As ErrorType, ByVal PropertyPath As String, ByVal Obj As Object, ByVal Exception As Exception)
        Me.ErrorType = ErrorType
        Me.PropertyPath = PropertyPath
        Me.Object = Obj
        Me.Exception = Exception
    End Sub

End Class