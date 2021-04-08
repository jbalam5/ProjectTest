<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XMLTOTABLE
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
        Me.OpenButton = New System.Windows.Forms.Button()
        Me.xmlDataGridView = New System.Windows.Forms.DataGridView()
        Me.xmlEncDataGridView = New System.Windows.Forms.DataGridView()
        CType(Me.xmlDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xmlEncDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenButton
        '
        Me.OpenButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.OpenButton.Location = New System.Drawing.Point(0, 0)
        Me.OpenButton.Name = "OpenButton"
        Me.OpenButton.Size = New System.Drawing.Size(800, 23)
        Me.OpenButton.TabIndex = 2
        Me.OpenButton.Text = "Seleccionar archivo"
        Me.OpenButton.UseVisualStyleBackColor = True
        '
        'xmlDataGridView
        '
        Me.xmlDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.xmlDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xmlDataGridView.Location = New System.Drawing.Point(0, 23)
        Me.xmlDataGridView.Name = "xmlDataGridView"
        Me.xmlDataGridView.Size = New System.Drawing.Size(800, 427)
        Me.xmlDataGridView.TabIndex = 3
        '
        'xmlEncDataGridView
        '
        Me.xmlEncDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.xmlEncDataGridView.Dock = System.Windows.Forms.DockStyle.Top
        Me.xmlEncDataGridView.Location = New System.Drawing.Point(0, 23)
        Me.xmlEncDataGridView.Name = "xmlEncDataGridView"
        Me.xmlEncDataGridView.Size = New System.Drawing.Size(800, 95)
        Me.xmlEncDataGridView.TabIndex = 4
        '
        'XMLTOTABLE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.xmlEncDataGridView)
        Me.Controls.Add(Me.xmlDataGridView)
        Me.Controls.Add(Me.OpenButton)
        Me.Name = "XMLTOTABLE"
        Me.Text = "XMLTOTABLE"
        CType(Me.xmlDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xmlEncDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents OpenButton As Button
    Friend WithEvents xmlDataGridView As DataGridView
    Friend WithEvents xmlEncDataGridView As DataGridView
End Class
