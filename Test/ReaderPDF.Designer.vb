<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReaderPDF
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
        Me.DeclaracionDataGridView = New System.Windows.Forms.DataGridView()
        CType(Me.DeclaracionDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenButton
        '
        Me.OpenButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.OpenButton.Location = New System.Drawing.Point(0, 0)
        Me.OpenButton.Name = "OpenButton"
        Me.OpenButton.Size = New System.Drawing.Size(800, 23)
        Me.OpenButton.TabIndex = 1
        Me.OpenButton.Text = "Seleccionar archivo"
        Me.OpenButton.UseVisualStyleBackColor = True
        '
        'DeclaracionDataGridView
        '
        Me.DeclaracionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DeclaracionDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DeclaracionDataGridView.Location = New System.Drawing.Point(0, 23)
        Me.DeclaracionDataGridView.Name = "DeclaracionDataGridView"
        Me.DeclaracionDataGridView.Size = New System.Drawing.Size(800, 427)
        Me.DeclaracionDataGridView.TabIndex = 2
        '
        'ReaderPDF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.DeclaracionDataGridView)
        Me.Controls.Add(Me.OpenButton)
        Me.Name = "ReaderPDF"
        Me.Text = "ReaderPDF"
        CType(Me.DeclaracionDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenButton As Button
    Friend WithEvents DeclaracionDataGridView As DataGridView
End Class
