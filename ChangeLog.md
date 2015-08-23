
---

Version: 2.1.1   - date: 30/mar/2012

Fixes several bugs:

  * Modification on RelatedObjectBindingSources property didn't trigger ListChanged(PropertyDescriptorChanged) event, and so   hierarchical controls (ej. UltraGrid) were not reflecting at design time that changes.

  * If a component was not using nested properties then the related components defined in RelatedObjectBindingSources were completely ignored. In that case, changes in the nested properties, eg, of these related components were not considered and not reflected in the bands   of hierarchical components.

  * Fixed a bug related to components not using BindableNestedProperties nor NonNestedPropertiesToSupervise, with NotifyPropertyChanges=True and where the data source was a list that did not generate ListChanged events (eg a single IList, not a IBindingList).
> >> This last error could be (not sure) the source of an unhandled exception in the Visual Studio that forced to reboot the IDE.



---

Version: 2.1.0   - date: 18/mar/2012

  * Added new property: **NotifyPropertyChanges**, that Indicates if the component must offer additional support for changes in properties. Base BindingSource communicate changes in properties of items of the datasource list (if that items implements INotifyPropertyChanged). If it is necessary to notify changes in other properties, such as nested properties then this property, NotifyPropertyChanges must be set to True. [Default: False]
> If set to True then many objects will be hooked to its PropertyChanged event. If this is not necessary then keeping to False will avoid having to inspect certain properties of the datasource objects. If the datasource list has properties expensive to obtain, this way we will avoid that calculation, unless needed.

> + Important: Previous version behaved as if NotifyPropertyChanges was True.

  * Added new property: **NonNestedPropertiesToSupervise**. Gets or sets non nested properties (implementing INotifyPropertyChanged) that must be listened in order to detect changes in its properties.
> This could be interesting when those properties could be used in controls like combo, and the modification of one of their properties could affect the value showed as display member.
> Only non nested properties implementing !INotifyPropertyChanged that are explicitly defined here will be considered. Otherwise, perhaps many properties should be queried at PropertyChanged events, and ocassionally the queries could be expensive.

> + Important: Previous version considered all non nested properties implementing INotifyPropertyChanged.



---

Version: 2.0.0   - date: 01/feb/2012

Multiple improvements and optimizations, achieved from an important redesign of the code. It has been revised the source code of BindingSource component, from which it inherits. Among the improvements, it is necessary to emphasize the proper treatment of changes in design time (and runtime) of properties as BindableNestedProperties so that controls binded to this datasources reflect changes immediately, and also the management of nested components. Also it is now properly managed the use of nested properties on bands with more than two levels deep. It is also correctly treated the case where this component uses as datasource another BindingSource component. If that component is also an ObjectBindingSource then it will take into account correctly all the properties that it can offer.
It has been improved the Test projects for a better assistance in debugging the component


  * A new property has been created, **ChildListsToConsider**, that makes it possible to indicate the list properties (implementing IList) for which nested components must be created in order to detect changes in nested properties of that child lists. If not set, then a component will be created for each list property (if NotifyChangesFromChildLists is set to True).
> The old version listened (with the creation of nested components) to every list type  property which had a registered component in RelatedObjectBindingSources with the same type of list item. Now that dependency hasn’t changed but it is also possible to restrict to a selection of those properties.


  * It has been created a new property, **ConsiderChildsOnlyInCurrent**, considered only when the component must provide support for the detection of changes in nested properties of child lists (if NotifyChangesInNestedPropertiesFromChildlists=True), that allows you to choose whether this changes should be monitored only in child lists of Current record or for all records of the base list.
> The behavior of the old version component oversaw all rows. Now the default value of this property is True, and so only Current row is monitored by default.
> If this property is set to False then the number of nested components ObjectBindingSource will vary depending on the number of items in the list.


  * Added new event, **ConfigurationChanged**, that indicates that one of the properties that affects to the component behaviour (specific to nested binding) has been modified. It is primarily used internally so that a component can act on such changes in those components included as its RelatedObjectBindingSources.

  * Added new event, **NestedError**, that indicates that an error has been detected (example: invalid nested property).


  * Property **NotifyListChangesFromNestedBindingSources** has been discarded, because it did not seem to be useful the ListChanged event that, based on that property, could be generated when it was detected a change in a child list. It has been considered enough to manage the ListChangedOnChildList event, that is generated when there are changes  (if NotifyChangesInNestedPropertiesFromChildlists = True)


  * The methods ValidatePropertyGetter and ValidatePropertySetter of **DynamicAccessor** have been modified to consider only properties without in-parameters. Thus the presence of another property with the same name but with parameters does not preclude the use of the first one. In this case, it caused an exception: It has been found an ambiguous match.



---

Version: 1.0.5   - date: 28/nov/2011

Bugfixes:

  * Nested properties offered for child bands (in hierchical data) with the help of the property RelatedObjectBindingSources worked only with the first child band. Childs of this last band were not including nested properties defined in a related object binding source. 'ListAccesors' property in GetItemProperties was not used correctly.

Other changes:
  * ObjectBindingSource now behaves like normal BindingSource when connected to a DataSet or to a DataTable


---

Version: 1.0.3   - date: 03/nov/2011

Fix memory leak problem disposing BindingSource, as indicated in the following pages:

  * BindingSource disposing and related BindingSources
(http://connect.microsoft.com/VisualStudio/feedback/details/434798
/bindingsource-disposing-and-related-bindingsources)
  * BindingSource on Disposing does not reset !lastCurrentItem field
(https://connect.microsoft.com/VisualStudio/feedback/details/434746
/bindingsource-on-disposing-does-not-reset-lastcurrentitem-field)

Other changes:
  * Included check on OnListChanged when ListChangedType.ItemAdded to verify that E.NewIndex <= MyBase.Count - 1


---

Version: 1.0.2   - date: 13/mar/2011

Bugfixes:

  * [Issue #4](https://code.google.com/p/object-binding-source/issues/detail?id=#4): PropertyChanged with propertyName is null parameter is not working
  * [Issue #5](https://code.google.com/p/object-binding-source/issues/detail?id=#5): At design-time: "the object xxx returned a null value for the property 'RelatedObjectBindingSources', but it is not allowed'


---

Version: 1.0.1   - date: 22/jan/2011

> With this version the component supports controls that can present, and optionally edit, both flat data (containing a single set of rows and columns) as well as hierarchical data. An example of that kind of controls is the Infragistics UltraGrid control.


---

Version: 1.0.0 -  date: 29/dic/2010

First version of ObjectBindingSource released by Daniel Prado