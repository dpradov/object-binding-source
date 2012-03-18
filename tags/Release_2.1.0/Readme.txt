====================================================
  http://code.google.com/p/object-binding-source/
====================================================

ObjectBindingSource
************************
    .NET Component derived from BindingSource that offers nested property binding and manages the PropertyChanged 
    events of nested objects. It's possible to select the nested properties in design time exploring the browsable
    properties of the type of the BindingSource. 


Introduction
-------------
In his article Nested Property Binding, resharper describes a component he has developed for the following reason:

    << I'm currently in the process of developing an object relation mapper for use with Microsoft SQL Server. During
    this process, I've been faced with a number of challenges related to the fact that I will be working with objects
    rather than datatables. If you've ever tried binding a datagrid to a list of objects, I'm sure you have come across
    the problem where you wanted to display properties that are not part of the object type itself. 

    Example: You have a list of orders, but you would like to display the customer's name and billing address along with
    the properties belonging to the Order type. 

    This is normally referred to as nested property binding. Many people create special view objects when this becomes
    a necessity. I wanted a smoother solution and I wanted it design time. The component I've created derives from 
    Bindingsource and is called ObjectBindingSource. >> 


I was looking for something like that component. I thought initially that it could be available in the embedded BindingSource
component, but it isn't. I have developed a framework to be used in smart client applications that wraps data inside DataSets
(data corresponding to the data model of the application, that moves among layers, including a webservice layer and persistence
 through Enterprise Library) and expose it as custom entity objects and collections, with the help of entity factories. 
Once of the things I missed was precisely what Seesharper indicates at the beginning of his article, the possibility of 
databind not only to the properties of the object itself but also the properties of any of the objects accesible through
that properties.

The component of SeeSharper works ok. However I have done a small number of changes to that code, like not using a 
BindingList(Of BindableProperty) where BindableProperty is just a serializable class with a string property. I have
preferred to use just a String array. I have renamed it _BindableNestedProperties instead of _bindableProperties. 
Among other benefits the form designer will now show the nested properties defined in the ObjectBindingSource component 
as clear strings.

For example, instead of:

this.ordersBindingSource.BindableProperties.Add(((System.Core.ComponentModel.BindableProperty)(resources.GetObject("ordersBindingSource.BindableProperties"))));
            this.ordersBindingSource.BindableProperties.Add(((System.Core.ComponentModel.BindableProperty)(resources.GetObject("ordersBindingSource.BindableProperties1"))));
...
            this.ordersBindingSource.BindableProperties.Add(((System.Core.ComponentModel.BindableProperty)(resources.GetObject("ordersBindingSource.BindableProperties8"))));

and resources like the following (in .resx):

</value>
  </data>
  <data name="orderlinesBindingSource.BindableProperties3" mimetype="application/x-microsoft.net.object.binary.base64">
    <value>
        AAEAAAD/////AQAAAAAAAAAMAgAAAEJTeXN0ZW0uQ29yZSwgVmVyc2lvbj0xLjAuMC4wLCBDdWx0dXJl
        PW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGwFAQAAACtTeXN0ZW0uQ29yZS5Db21wb25lbnRNb2Rl
        bC5CaW5kYWJsZVByb3BlcnR5AQAAAAVfbmFtZQECAAAABgMAAAARUHJvZHVjdC5Vbml0UHJpY2UL
</value>

for something like (in Designer):

     this.ordersBindingSource.BindableNestedProperties = new string[] {
         "Customer.Name",
         "Customer.BillingAddress.StreetAddress",
         "Customer.BillingAddress.City",
         "DeliveryAddress.StreetAddress",
         "DeliveryAddress.City"};


Another change: in the method CreatePropertyDescriptors I don't clear the collection of nested bindable properties. 
The following sentence gave me problems because it did not distinguish between design time and executing time and so
I found many times that the properties I had defined had dissapeared.

         //Something is wrong in the property path of one or more properties
         _bindableProperties.Clear();                                      

Also, this component exposes only the browsable properties, like the original BindingSource does.



INotifyPropertyChanged management
---------------------------------
But the importants changes are related with one the comments that someone did to that component:

    << nice stuff! But after a couple of time using your solution I realize the ObjectBindingSource ignores the PropertyChanged 
    event of nested objects. 

    E.g. I've got a class 'Foo' with two properties named 'Name' and 'Bar'. 'Name' is a string an 'Bar' reference an instance
    of class 'Bar', which has a 'Name' property of type string too and both classes implements INotifyPropertyChanged. 

    With your binding source reading and writing with both properties ('Name' and 'Bar_Name') works fine but the PropertyChanged
    event works only for the 'Name' property, because the binding source listen only for events of 'Foo'. 

    One workaround is to retrigger the PropertyChanged event in the appropriate class (here 'Foo'). What's very unclean! 
    The other approach would be to extend ObjectBindingSource so that all owner of nested property which implements
    INotifyPropertyChanged get used for receive changes, but how? >> 


I have done the second: extend the ObjectBindingSource to listen to the NotifyPropertyChanged event of the objects implementing 
that interface that are involved in the nested properties, like the class 'Bar' of the comment. The component subscribes or 
unsubscribes from events depending on the object to be or not in use from any of the properties.

I have used a structure that saves the object hierarchy involved in the distinct nested properties, to detect when a change 
in a property forces to listen to a new object and to stop listening to the older one: _NestedObjects. Of course, the component
will raise the corresponding ListChanged events. To limit the search of objects inside that structure to the properties that 
can host the affected objects, I have included an array of Types associated to the nested properties: _TypesInNestedProperties.


AutoCreateObjects and CreatingObject event
---------------------------------
The component raises a new event, CreatingObject that indicates that a new object will be created, according to the property 
AutoCreateObjects, when setting the values. It is similar to the event AddingNew, but the object to create can be of any type.
The new event has the following signature:

Public Event CreatingObject(ByVal Sender As Object, ByVal ObjectType As Type, ByRef Obj As Object)

It allows us to create a custom object of the type indicated in ObjectType. If we don't set the parameter 'Obj' then a new
object of that type will be created automatically by reflection.


I have also used in the test solution some pointers on the form of WeakReference to verify that the objects can be recollected 
from GC, ensuring that the component frees correctly all the used resources, including eventhandlers. To further verify it I 
have used ANTS memory profiler.


Design time exploring of the nested properties
---------------------------------------------
For convenience I have added a form editor that let us to select the properties in design time exploring the browsable 
properties of the type of the BindingSource. When you expand a property it will show the browsable properties corresponding 
to the type of that property, and so on.


Final notes
-----------
The component ObjectBindingSource itself have been rewriten in VB. The auxiliary classes, like DynamicAccessor or 
HybridCollection are maintened in C#, in an auxiliary dll, UtilesC (ToolsC). With the help of Reflector anyone could
convert to C#, if needed.

The name of the methods and variable are written in english, but there are some comments in spanish. I have not traslated
all that comments, sorry for it. But with a free program like Lingoes you can easily translate to your language :) 