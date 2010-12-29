Option Strict On

Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports System.Security.Permissions

'#  Released under the MIT license, reproduced below:
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


<PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
Public Class NestedPropertiesEditor
    Inherits UITypeEditor

    Private edSvc As IWindowsFormsEditorService


    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (((Not context Is Nothing) And (Not context.Instance Is Nothing)) And (Not provider Is Nothing)) Then
            Dim domainAux As AppDomain = Nothing
            Dim form As FrmNestedProperties
            Me.edSvc = DirectCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)

            If (Not Me.edSvc Is Nothing) Then
                Dim OBS As ObjectBindingSource = DirectCast(context.Instance, ObjectBindingSource)
                Try
                    Dim ItemType As Type = OBS._itemType
                    form = New FrmNestedProperties(ItemType, DirectCast(value, String()))
                    If Me.edSvc.ShowDialog(form) = DialogResult.OK Then
                        value = form.BindableNestedProperties
                    End If
                Finally
                    If domainAux IsNot Nothing Then AppDomain.Unload(domainAux)
                End Try

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

