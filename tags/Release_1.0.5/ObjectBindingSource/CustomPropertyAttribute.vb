Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Reflection
Imports System.Text

'#-------------------------------
'# File rewritten in vb.net from its original code in C#, of seesharper
'# (http://www.codeproject.com/Members/seesharper) as available in: 
'#   http://www.codeproject.com/KB/cs/ObjectBindingSource.aspx
'# You can also download it from: http://code.google.com/p/object-binding-source/downloads/list
'#
'# The modifications made by Daniel Prado Velasco (<dpradov@gmail.com>) are licensed under MIT License
'# included in the file LICENSE.txt, with the prevalence of the restrictions that the original license
'# could impose
'# -----------------
'# URLs:
'#  http://code.google.com/p/object-binding-source/



Public Class CustomPropertyAttribute
    Inherits Attribute

    Private _propertyDescriptior As PropertyDescriptor
    Private _propertyPath As String
    Private _rootType As Type


    Public Sub New(ByVal rootType As Type, ByVal propertyPath As String, ByVal propertyDescriptor As PropertyDescriptor)
        _rootType = rootType
        _propertyPath = propertyPath
        _propertyDescriptior = propertyDescriptor
    End Sub


    Public ReadOnly Property PropertyInfo() As PropertyDescriptor
        Get
            Return _propertyDescriptior
        End Get
    End Property

    Public ReadOnly Property PropertyPath() As String
        Get
            Return _propertyPath
        End Get
    End Property

    Public ReadOnly Property RootType() As Type
        Get
            Return _rootType
        End Get
    End Property


End Class

