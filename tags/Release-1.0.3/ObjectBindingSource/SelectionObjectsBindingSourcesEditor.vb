Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports System.ComponentModel
Imports System.Drawing.Design

Public Class SelectionObjectsBindingSourcesEditor
    Inherits UITypeEditor

    Private edSvc As IWindowsFormsEditorService


    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (((Not context Is Nothing) And (Not context.Instance Is Nothing)) And (Not provider Is Nothing)) Then
            Dim domainAux As AppDomain = Nothing

            Me.edSvc = DirectCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)

            If (Not Me.edSvc Is Nothing) Then
                ' Create an instance of the UI editor, passing a reference to the editor service
                Dim OBS As ObjectBindingSource = DirectCast(context.Instance, ObjectBindingSource)
                Dim ActualListOBS As ObjectBindingSource() = CType(value, ObjectBindingSource())
                Dim form As New FrmSelectionObjectsBindingSources(OBS, ActualListOBS, edSvc)

                If Me.edSvc.ShowDialog(form) = DialogResult.OK Then
                    value = form.NewListOBS.ToArray
                End If
            End If

        End If
        Return value
    End Function

    Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        If ((Not context Is Nothing) And (Not context.Instance Is Nothing)) Then
            Return UITypeEditorEditStyle.Modal
        End If
        Return MyBase.GetEditStyle(context)
    End Function

End Class
