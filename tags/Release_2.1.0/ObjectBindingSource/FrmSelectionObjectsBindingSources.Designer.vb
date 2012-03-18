<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSelectionObjectsBindingSources
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnAccept = New System.Windows.Forms.Button
        Me.cList = New System.Windows.Forms.CheckedListBox
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(103, 179)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(73, 21)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnAccept
        '
        Me.btnAccept.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnAccept.Location = New System.Drawing.Point(24, 179)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(73, 21)
        Me.btnAccept.TabIndex = 5
        Me.btnAccept.Text = "&OK"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'cList
        '
        Me.cList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cList.CheckOnClick = True
        Me.cList.FormattingEnabled = True
        Me.cList.Location = New System.Drawing.Point(24, 24)
        Me.cList.Name = "cList"
        Me.cList.Size = New System.Drawing.Size(152, 139)
        Me.cList.TabIndex = 4
        '
        'FrmSelectionObjectsBindingSources
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(199, 212)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAccept)
        Me.Controls.Add(Me.cList)
        Me.Name = "FrmSelectionObjectsBindingSources"
        Me.Text = "FrmNestedBindingSources"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents cList As System.Windows.Forms.CheckedListBox
End Class
