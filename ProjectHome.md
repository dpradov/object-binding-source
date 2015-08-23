**ObjectBindingSource**
> .NET Component derived from BindingSource that offers nested property binding and manages the PropertyChanged events of nested objects. It's possible to select the nested properties in design time exploring the browsable properties of the type of the BindingSource.
> Supports controls that can present, and optionally edit, both flat data (containing a single set of rows and columns) as well as hierarchical data. An example of that kind of controls is the [Infragistics UltraGrid](http://www.infragistics.com/dotnet/netadvantage/winforms/wingrid.aspx#Overview) control.

_Last version_: **2.1.1** - date: 30/mar/2012

  * Fixes several bugs from the previous version  **(recommended update)**
> [ChangeLog](http://code.google.com/p/object-binding-source/wiki/ChangeLog)


<br />
<br />
# Introduction #
In his article [Nested Property Binding](http://www.codeproject.com/KB/cs/ObjectBindingSource.aspx), resharper describes a component he has developed for the following reason:

> <<
> _I'm currently in the process of developing an object relation mapper for use with Microsoft SQL Server. During this process, I've been faced with a number of challenges related to the fact that I will be working with objects rather than datatables. If you've ever tried binding a datagrid to a list of objects, I'm sure you have come across the problem where you wanted to display properties that are not part of the object type itself._

> _Example: You have a list of orders, but you would like to display the customer's name and billing address along with the properties belonging to the Order type._

> _This is normally referred to as nested property binding. Many people create special view objects when this becomes a necessity. I wanted a smoother solution and I wanted it design time. The component I've created derives from Bindingsource and is called ObjectBindingSource._
> >>


<br />

I was looking for something like that component. I thought initially that it could be available in the embedded BindingSource component, but it isn't.
I have developed a framework to be used in smart client applications that wraps data inside DataSets (data corresponding to the data model of the application, that moves among layers, including a webservice layer and persistence through Enterprise Library) and expose it as custom entity objects and collections, with the help of entity factories.
Once of the things I missed was precisely what Seesharper indicates at the beginning of his article, the possibility of databind not only to the properties of the object itself but also the properties of any of the objects accesible through that properties.

The component of SeeSharper works ok. However I have done a small number of changes to that code, like not using a `BindingList(Of BindableProperty)` where `BindableProperty` is just a serializable class with a string property. I have preferred to use just a String array. I have renamed it `_BindableNestedProperties` instead of `_bindableProperties`. Among other benefits the form designer will now show the nested properties defined in the ObjectBindingSource component as clear strings.

For example, instead of:

```

this.ordersBindingSource.BindableProperties.Add(
((System.Core.ComponentModel.BindableProperty)
(resources.GetObject("ordersBindingSource.BindableProperties"))));
this.ordersBindingSource.BindableProperties.Add(
((System.Core.ComponentModel.BindableProperty)
(resources.GetObject("ordersBindingSource.BindableProperties1"))));
...
this.ordersBindingSource.BindableProperties.Add(
((System.Core.ComponentModel.BindableProperty)
(resources.GetObject("ordersBindingSource.BindableProperties8"))));

```

and resources like the following (in .resx):

```

</value>
  </data>
  <data name="orderlinesBindingSource.BindableProperties3"
        mimetype="application/x-microsoft.net.object.binary.base64">
    <value>
        AAEAAAD/////AQAAAAAAAAAMAgAAAEJTeXN0ZW0uQ29yZSwgVmVyc2lvbj0xLjAuMC4wLCBDdWx0dXJl
        PW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGwFAQAAACtTeXN0ZW0uQ29yZS5Db21wb25lbnRNb2Rl
        bC5CaW5kYWJsZVByb3BlcnR5AQAAAAVfbmFtZQECAAAABgMAAAARUHJvZHVjdC5Vbml0UHJpY2UL
</value>

```

for something like (in Designer):
```

     this.ordersBindingSource.BindableNestedProperties = new string[] {
         "Customer.Name",
         "Customer.BillingAddress.StreetAddress",
         "Customer.BillingAddress.City",
         "DeliveryAddress.StreetAddress",
         "DeliveryAddress.City"};

```

<br />

Another change: in the method `CreatePropertyDescriptors` I don't clear the collection of nested bindable properties. The following sentence gave me problems because it did not distinguish between design time and executing time and so I found many times that the properties I had defined had dissapeared.

```
         //Something is wrong in the property path of one or more properties
         _bindableProperties.Clear();                                      
```

Also, this component exposes only the _browsable_ properties, like the original BindingSource does.

<br />
<br />

**INotifyPropertyChanged management**

But the importants changes are related with one of the comments that someone did to that component:

> <<
> _nice stuff! But after a couple of time using your solution I realize the ObjectBindingSource ignores the `PropertyChanged` event of nested objects._

> _E.g. I've got a class 'Foo' with two properties named 'Name' and 'Bar'. 'Name' is a string an 'Bar' reference an instance of class 'Bar', which has a 'Name' property of type string too and both classes implements `INotifyPropertyChanged`._

> _With your binding source reading and writing with both properties ('Name' and '`Bar_Name`') works fine but the `PropertyChanged` event works only for the 'Name' property, because the binding source listen only for events of 'Foo'._

> _One workaround is to retrigger the `PropertyChanged` event in the appropriate class (here 'Foo'). What's very unclean! The other approach would be to extend ObjectBindingSource so that all owner of nested property which implements `INotifyPropertyChanged` get used for receive changes, but how?_
> >>

<br />

I have done the second: extend the ObjectBindingSource to listen to the `NotifyPropertyChanged` event of the objects implementing that interface that are involved in the nested properties, like the class 'Bar' of the comment. The component subscribes or unsubscribes from events depending on the object to be or not in use from any of the properties.

I have used a structure that saves the object hierarchy involved in the distinct nested properties, to detect when a change in a property forces to listen to a new object and to stop listening to the older one: `_NestedObjects`. Of course, the component will raise the corresponding `ListChanged` events.
To limit the search of objects inside that structure to the properties that can host the affected objects, I have included an array of Types associated to the nested properties: `_TypesInNestedProperties`.

<br />

**AutoCreateObjects and CreatingObject event**

The component raises a new event, `CreatingObject` that indicates that a new object will be created, according to the property `AutoCreateObjects`, when setting the values. It is similar to the event `AddingNew`, but the object to create can be of any type. The new event has the following signature:
```
Public Event CreatingObject(ByVal Sender As Object, ByVal ObjectType As Type, ByRef Obj As Object)
```
It allows us to create a custom object of the type indicated in `ObjectType`. If we don't set the parameter 'Obj' then a new object of that type will be created automatically by reflection.

<br />

I have also used in the test solution some pointers on the form of `WeakReference` to verify that the objects can be recollected from GC, ensuring that the component frees correctly all the used resources, including eventhandlers. To further verify it I have used ANTS memory profiler.

<br />

**Design time exploring of the nested properties**

For convenience I have added a form editor that let us to select the properties in design time exploring the browsable properties of the type of the BindingSource. When you expand a property it will show the browsable properties corresponding to the type of that property, and so on.

<br />

**Final notes**

The component ObjectBindingSource itself have been rewriten in VB. The auxiliary classes, like `DynamicAccessor` or `HybridCollection` are maintened in C#, in an auxiliary dll, `UtilesC` (`ToolsC`).
With the help of Reflector anyone could convert to C#, if needed.

The name of the methods and variable are written in english, but there are some comments in spanish. I have not traslated all that comments, sorry for it. But with a free program like Lingoes you can easily translate to your language :)

<br /><br />

**Support for controls that can manage hierarchical data**

With version 1.0.1 the component supports controls that can present, and optionally edit, both flat data (containing a single set of rows and columns) as well as hierarchical data. An example of that kind of controls is the Infragistics UltraGrid control.

Have been resolved with the help of related (nested) ObjectBindingSource components.

We can have ObjectBindingSource components associated to some or all of the properties that references a list of values (e.g: order -> !orderLines), so that the method `GetItemProperties` can return for the `listAccesors` the `PropertyDescriptors` indicated in that components. This way that `listAccesors` can also expose nested properties.

> This is useful with UI controls that show not only the properties of the  datasource objects itself, but also can show in another level (for example, 'bands' in Infragistics's UltraGrid) the properties of the child objects.


The component includes new properties and events:

  * RelatedObjectBindingSources
```
Public Property RelatedObjectBindingSources() As ObjectBindingSource()
```

> Gets or sets the related/nested components ObjectBindingSource that offer the `PropertyDescriptors` for the properties associated to child lists. The properties will be associated automatically from the type of the list item.
> > This components will be used as templates to the new ones that will be dinamically created and referenced in `_NestedBindingSources` if the property `NotifyChangesInNestedPropertiesFromChildlists` is set to True.


  * NotifyChangesInNestedPropertiesFromChildlists
```
Public Property NotifyChangesInNestedPropertiesFromChildlists() As Boolean
```
> > Indicates if the component must offer support for changes in nested properties of child lists, via the listening of `INotifyPropertyChanges`.


> If set to True then it will be created new ObjectBindingSource components to manage that changes (one for each child list to supervise). And also, changes in non nested properties of this child lists will be notified even if that lists don't implement `IBindingList`.
> Otherwise, it will only be used the components in `_RelatedBindingSource`, to facilitate the property accesors.


  * NotifyListChangesFromNestedBindingSources
```
Public Property NotifyListChangesFromNestedBindingSources() As Boolean
```

> Controls whether to raise or not a `ListChange` event on changes over child lists (The `ListChange` events refer to the parent row and the property associated to the child list)
> It is possible that the control don't refresh the child band on response to that event (although the corresponding property is specified).
> That is way it is offered a new event, `ListChangedOnChildList`:


  * ListChangedOnChildList
```
Public Event ListChangedOnChildList(ByVal fila As Integer, 
                                    ByVal sender As Object, ByVal e As ListChangedEventArgs)
```

It will inform that a change as occured in the child list associated to the row indicated. Controlling this event we can refresh de UI control the way needed. For example, with UltraGrid we could do something like:

```

private void ordersBindingSource_ListChangedOnChildList(int row, object sender, ListChangedEventArgs e)
{                   ultraGrid1.Rows[row].ChildBands[0].Rows.Refresh(
                           Infragistics.Win.UltraWinGrid.RefreshRow.RefreshDisplay);
}

```

<br /><br />

# License #

```

' ObjectBindingSource is based in code released by seesharper
'  (http://www.codeproject.com/Members/seesharper) 
' and licensed under The Code Project Open License (CPOL)
'  (http://www.codeproject.com/info/cpol10.aspx)
' The original work of seesharper is available in: 
'   http://www.codeproject.com/KB/cs/ObjectBindingSource.aspx
' You can also download it from: 
'   http://code.google.com/p/object-binding-source/downloads/list
'
' The modifications made by Daniel Prado Velasco (<dpradov at gmail dot com>) 
' are licensed under MIT License, reproduced below (with the 
' prevalence of the restrictions that the original license (CPOL) 
' could impose).
' Note that all the original work in ObjectBindingSource was 
' written in C#.
'
'  MIT License:
'  ======================================================================
'  Copyright (C)  2010  Daniel Prado Velasco <dpradov at gmail dot com>
'
'                         All Rights Reserved
'
' Permission is hereby granted, free of charge, to any person
' obtaining a copy of this software and associated documentation
' files (the "Software"), to deal in the Software without
' restriction, including without limitation the rights to use,
' copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the
' Software is furnished to do so, subject to the following
' conditions:
'
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
' OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
' HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
' WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
' FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
' OTHER DEALINGS IN THE SOFTWARE.
'
'  ======================================================================

```