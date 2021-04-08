<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SendMail
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
        Me.serverTextBox = New System.Windows.Forms.TextBox()
        Me.loginTextBox = New System.Windows.Forms.TextBox()
        Me.puertoTextBox = New System.Windows.Forms.TextBox()
        Me.passwordTextBox = New System.Windows.Forms.TextBox()
        Me.asuntoTextBox = New System.Windows.Forms.TextBox()
        Me.senderTextBox = New System.Windows.Forms.TextBox()
        Me.displaynameTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.mensajeTextBox = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.sslCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.fromTextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'serverTextBox
        '
        Me.serverTextBox.Location = New System.Drawing.Point(74, 21)
        Me.serverTextBox.Name = "serverTextBox"
        Me.serverTextBox.Size = New System.Drawing.Size(301, 20)
        Me.serverTextBox.TabIndex = 0
        '
        'loginTextBox
        '
        Me.loginTextBox.Location = New System.Drawing.Point(74, 48)
        Me.loginTextBox.Name = "loginTextBox"
        Me.loginTextBox.Size = New System.Drawing.Size(301, 20)
        Me.loginTextBox.TabIndex = 1
        '
        'puertoTextBox
        '
        Me.puertoTextBox.Location = New System.Drawing.Point(458, 22)
        Me.puertoTextBox.Name = "puertoTextBox"
        Me.puertoTextBox.Size = New System.Drawing.Size(330, 20)
        Me.puertoTextBox.TabIndex = 2
        '
        'passwordTextBox
        '
        Me.passwordTextBox.Location = New System.Drawing.Point(458, 51)
        Me.passwordTextBox.Name = "passwordTextBox"
        Me.passwordTextBox.Size = New System.Drawing.Size(330, 20)
        Me.passwordTextBox.TabIndex = 3
        '
        'asuntoTextBox
        '
        Me.asuntoTextBox.Location = New System.Drawing.Point(5, 114)
        Me.asuntoTextBox.Name = "asuntoTextBox"
        Me.asuntoTextBox.Size = New System.Drawing.Size(370, 20)
        Me.asuntoTextBox.TabIndex = 4
        '
        'senderTextBox
        '
        Me.senderTextBox.Location = New System.Drawing.Point(404, 114)
        Me.senderTextBox.Name = "senderTextBox"
        Me.senderTextBox.Size = New System.Drawing.Size(384, 20)
        Me.senderTextBox.TabIndex = 5
        '
        'displaynameTextBox
        '
        Me.displaynameTextBox.Location = New System.Drawing.Point(5, 178)
        Me.displaynameTextBox.Name = "displaynameTextBox"
        Me.displaynameTextBox.Size = New System.Drawing.Size(370, 20)
        Me.displaynameTextBox.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Server"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Email"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(401, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Server"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(401, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Puerto"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(401, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Password"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(2, 98)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Asunto"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(401, 98)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Sender"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(2, 162)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "DisplayName"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(401, 162)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Mensaje"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'mensajeTextBox
        '
        Me.mensajeTextBox.Location = New System.Drawing.Point(404, 178)
        Me.mensajeTextBox.Name = "mensajeTextBox"
        Me.mensajeTextBox.Size = New System.Drawing.Size(384, 20)
        Me.mensajeTextBox.TabIndex = 14
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(74, 215)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(251, 33)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "Aceptar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(440, 215)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(251, 33)
        Me.Button2.TabIndex = 17
        Me.Button2.Text = "Limpiar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'sslCheckBox
        '
        Me.sslCheckBox.AutoSize = True
        Me.sslCheckBox.Location = New System.Drawing.Point(5, 74)
        Me.sslCheckBox.Name = "sslCheckBox"
        Me.sslCheckBox.Size = New System.Drawing.Size(46, 17)
        Me.sslCheckBox.TabIndex = 18
        Me.sslCheckBox.Text = "SSL"
        Me.sslCheckBox.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(2, 142)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(29, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Para"
        '
        'fromTextBox
        '
        Me.fromTextBox.Location = New System.Drawing.Point(58, 139)
        Me.fromTextBox.Name = "fromTextBox"
        Me.fromTextBox.Size = New System.Drawing.Size(730, 20)
        Me.fromTextBox.TabIndex = 19
        '
        'SendMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 265)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.fromTextBox)
        Me.Controls.Add(Me.sslCheckBox)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.mensajeTextBox)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.displaynameTextBox)
        Me.Controls.Add(Me.senderTextBox)
        Me.Controls.Add(Me.asuntoTextBox)
        Me.Controls.Add(Me.passwordTextBox)
        Me.Controls.Add(Me.puertoTextBox)
        Me.Controls.Add(Me.loginTextBox)
        Me.Controls.Add(Me.serverTextBox)
        Me.Name = "SendMail"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents serverTextBox As TextBox
    Friend WithEvents loginTextBox As TextBox
    Friend WithEvents puertoTextBox As TextBox
    Friend WithEvents passwordTextBox As TextBox
    Friend WithEvents asuntoTextBox As TextBox
    Friend WithEvents senderTextBox As TextBox
    Friend WithEvents displaynameTextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents mensajeTextBox As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents sslCheckBox As CheckBox
    Friend WithEvents Label10 As Label
    Friend WithEvents fromTextBox As TextBox
End Class
