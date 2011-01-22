Imports System.Windows.Forms.Design
Imports System.Windows.Forms
Imports System.ComponentModel

Public Class FrmSelectionObjectsBindingSources

    Private _editorService As IWindowsFormsEditorService
    Private _container As IContainer

    Public NewListOBS As List(Of ObjectBindingSource)

    Sub New(ByVal OBS As ObjectBindingSource, ByVal ActualListOBS As ObjectBindingSource(), ByVal editorService As IWindowsFormsEditorService)
        InitializeComponent()
        _editorService = editorService
        _container = OBS.Container

        PopulateList(OBS, ActualListOBS)
    End Sub

    Sub PopulateList(ByVal OBS As ObjectBindingSource, ByVal ActualListOBS As ObjectBindingSource())
        For Each comp As Object In _container.Components
            If TypeOf comp Is ObjectBindingSource AndAlso comp IsNot OBS Then
                If ActualListOBS Is Nothing Then
                    cList.Items.Add(TypeDescriptor.GetComponentName(comp), False)    ' El nombre también se podría obtener así: comp.Site.Name
                Else
                    cList.Items.Add(TypeDescriptor.GetComponentName(comp), ActualListOBS.Contains(CType(comp, ObjectBindingSource)))
                End If
            End If
        Next
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        ' Return the new property value from the editor
        NewListOBS = New List(Of ObjectBindingSource)
        For Each compName As String In cList.CheckedItems
            NewListOBS.Add(CType(_container.Components.Item(compName), ObjectBindingSource))
        Next
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cList_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cList.ItemCheck
        Dim comp As ObjectBindingSource
        Dim newItemType, itemType As Type

        comp = CType(_container.Components.Item(CType(cList.Items(e.Index), String)), ObjectBindingSource)
        newItemType = ListBindingHelper.GetListItemType(comp.DataSource, comp.DataMember)

        For Each index As Integer In cList.CheckedIndices
            If index = e.Index Then Continue For

            comp = CType(_container.Components.Item(CType(cList.Items(index), String)), ObjectBindingSource)
            itemType = ListBindingHelper.GetListItemType(comp.DataSource, comp.DataMember)
            If newItemType Is itemType Then
                MsgBox("Ya ha seleccionado un ObjectBindingSource con el mismo tipo base: " + itemType.ToString, MsgBoxStyle.Exclamation)
                e.NewValue = e.CurrentValue
                Exit Sub
            End If
        Next
    End Sub
End Class