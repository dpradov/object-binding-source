Imports System.Windows.Forms
Imports System.ComponentModel

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


Public Class FrmNestedProperties

    Private Structure NodeState
        Public WasExpanded As Boolean
        Public PropertyDescriptor As PropertyDescriptor

        Sub New(ByVal wasExpanded As Boolean, ByVal propertyDescriptor As PropertyDescriptor)
            Me.WasExpanded = wasExpanded
            Me.PropertyDescriptor = propertyDescriptor
        End Sub
    End Structure

    Private _BindableNestedProperties As String()
    Private _ItemType As Type


    Sub New(ByVal ItemType As Type, ByVal BindableNestedProperties As String())
        InitializeComponent()

        _ItemType = ItemType
        _BindableNestedProperties = BindableNestedProperties
    End Sub

    Private Sub FrmNestedProperties_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cNestedProperties.Lines = _BindableNestedProperties
        LoadProperties(Nothing, True)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="node">Node on wich to add the properties</param>
    ''' <remarks>
    ''' Node can be Nothing. In this case it will be loaded the properties over the first level of the tree,
    ''' clearing it previously.
    ''' </remarks>
    Private Sub LoadProperties(ByVal Node As TreeNode, Optional ByVal LoadTwoLevels As Boolean = False)
        Dim Nodes As TreeNodeCollection
        Dim ItemType As Type

        If Node Is Nothing Then
            Nodes = cTree.Nodes
            ItemType = _ItemType
            Nodes.Clear()
        Else
            Nodes = Node.Nodes
            ItemType = CType(Node.Tag, NodeState).PropertyDescriptor.PropertyType
        End If

        If ItemType IsNot GetType(Object) Then
            Dim Properties = TypeDescriptor.GetProperties(ItemType)
            Dim newNode As TreeNode
            For Each propertyDescriptor As PropertyDescriptor In Properties
                If propertyDescriptor.IsBrowsable Then
                    Dim nodeState = New NodeState(False, propertyDescriptor)
                    newNode = New TreeNode(propertyDescriptor.Name)
                    newNode.Tag = nodeState
                    Nodes.Add(newNode)
                End If
            Next

            If LoadTwoLevels Then
                ' In this way you can see which nodes are expansible
                For Each newNode In Nodes
                    LoadProperties(newNode)
                Next
            End If

        End If
    End Sub

    Private Sub cTree_BeforeExpand(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles cTree.BeforeExpand
        Dim node = e.Node
        Dim wasExpanded = CType(e.Node.Tag, NodeState).WasExpanded

        If Not wasExpanded Then
            For Each child As TreeNode In node.Nodes
                LoadProperties(child, False)
            Next
            Dim nodeState = CType(node.Tag, NodeState)
            nodeState.WasExpanded = True
            node.Tag = nodeState
        End If
    End Sub

    Private Sub AddNestedProperty()
        Dim node = cTree.SelectedNode
        Dim path As String = ""
        Dim sep As String = ""

        If node IsNot Nothing AndAlso node.Parent IsNot Nothing Then
            Do
                path = node.Text + sep + path
                node = node.Parent
                sep = "."
            Loop Until node Is Nothing

            Dim properties As List(Of String) = cNestedProperties.Lines.ToList
            If Not properties.Contains(path) Then
                properties.Add(path)
                cNestedProperties.Lines = properties.ToArray
            End If
        End If

    End Sub

    Public ReadOnly Property BindableNestedProperties() As String()
        Get
            Dim properties As List(Of String) = cNestedProperties.Lines.ToList
            Dim cleanProperties As New List(Of String)

            For Each prop In properties
                prop = prop.Trim
                If Not String.IsNullOrEmpty(prop) Then
                    cleanProperties.Add(prop)
                End If
            Next
            Return cleanProperties.ToArray
        End Get
    End Property


    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddNestedProperty()
    End Sub

    Private Sub cTree_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles cTree.NodeMouseDoubleClick
        AddNestedProperty()
    End Sub

End Class