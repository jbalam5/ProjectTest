<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OCRPDF
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
        Me.JSONTreeView = New System.Windows.Forms.TreeView()
        Me.ResultadoTextBox = New System.Windows.Forms.TextBox()
        Me.OpenFileButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'JSONTreeView
        '
        Me.JSONTreeView.Dock = System.Windows.Forms.DockStyle.Left
        Me.JSONTreeView.Location = New System.Drawing.Point(0, 0)
        Me.JSONTreeView.Name = "JSONTreeView"
        Me.JSONTreeView.Size = New System.Drawing.Size(526, 555)
        Me.JSONTreeView.TabIndex = 0
        '
        'ResultadoTextBox
        '
        Me.ResultadoTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResultadoTextBox.Location = New System.Drawing.Point(0, 0)
        Me.ResultadoTextBox.Multiline = True
        Me.ResultadoTextBox.Name = "ResultadoTextBox"
        Me.ResultadoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ResultadoTextBox.Size = New System.Drawing.Size(497, 555)
        Me.ResultadoTextBox.TabIndex = 1
        '
        'OpenFileButton
        '
        Me.OpenFileButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.OpenFileButton.Location = New System.Drawing.Point(0, 0)
        Me.OpenFileButton.Name = "OpenFileButton"
        Me.OpenFileButton.Size = New System.Drawing.Size(1023, 23)
        Me.OpenFileButton.TabIndex = 2
        Me.OpenFileButton.Text = "Abrir"
        Me.OpenFileButton.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.JSONTreeView)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1023, 555)
        Me.Panel1.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ResultadoTextBox)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(526, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(497, 555)
        Me.Panel2.TabIndex = 2
        '
        'OCRPDF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1023, 578)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.OpenFileButton)
        Me.Name = "OCRPDF"
        Me.Text = "OCRPDF"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents JSONTreeView As TreeView
    Friend WithEvents ResultadoTextBox As TextBox
    Friend WithEvents OpenFileButton As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
End Class
