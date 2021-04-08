Imports System.Net
Imports System.Net.Mail

Public Class SendMail
    Private Sub SendMail_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Function SendEmailMessage(ByVal SmtpServer As String, ByVal SmtpPort As Integer, ByVal SmtpLogin As String, ByVal SmtpPass As String, ByVal strFrom As String, ByVal displayNameFrom As String, ByVal strTo As List(Of String), ByVal strSubject As String, ByVal strMessage As String, ByVal HighPriority As Boolean, ByVal UseHTML As Boolean, ByVal AttachFileList() As String, ByVal EnableSSL As Boolean) As Integer
        'This procedure takes string array parameters for multiple recipients and files
        Try
            If Not String.IsNullOrEmpty(SmtpServer) Then
                If (SmtpPort > 0) Then
                    For Each item As String In strTo
                        Try
                            If Not String.IsNullOrEmpty(item) Then

                                'For each to address create a mail message
                                Dim MailMsg As New MailMessage()
                                ''MailMsg.From = New MailAddress(strFrom.Trim, displayNameFrom.Trim)
                                MailMsg.From = New MailAddress($"{displayNameFrom} <{strFrom}>")
                                MailMsg.To.Add(item.Trim())
                                MailMsg.BodyEncoding = System.Text.Encoding.Default
                                MailMsg.Subject = strSubject.Trim
                                MailMsg.Body = strMessage.Trim & vbCrLf
                                If HighPriority Then MailMsg.Priority = MailPriority.High
                                MailMsg.IsBodyHtml = UseHTML

                                'attach each file attachment
                                If AttachFileList IsNot Nothing AndAlso AttachFileList.Length > 0 Then
                                    For Each strfile As String In AttachFileList
                                        If Not strfile Is Nothing AndAlso Not strfile = "" Then
                                            Dim MsgAttach As New Attachment(strfile)
                                            MailMsg.Attachments.Add(MsgAttach)
                                        End If
                                    Next
                                End If

                                'Smtpclient to send the mail message
                                Dim SMtpMail As New SmtpClient(SmtpServer)
                                ''SMtpMail.Host = SmtpServer
                                If Not (EnableSSL AndAlso SmtpPort = 465) Then SMtpMail.Port = SmtpPort
                                SMtpMail.EnableSsl = EnableSSL

                                'Se especifican las credenciales del correo
                                If Not String.IsNullOrEmpty(SmtpLogin) AndAlso Not String.IsNullOrEmpty(SmtpPass) Then
                                    Dim mycredentials As New NetworkCredential(SmtpLogin, SmtpPass)
                                    SMtpMail.Credentials = mycredentials
                                End If

                                SMtpMail.Send(MailMsg)
                                MailMsg.Attachments.Dispose()
                                MailMsg.Dispose()
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                Else
                    Throw New Exception(String.Format("No se ha especificado Puerto de SMTP ({0}).", SmtpPort.ToString))
                End If
            Else
                Throw New Exception("No recibí el SmtpServer.")
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            Throw ex
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim listTo As New List(Of String)
            listTo.Add(fromTextBox.Text)

            SendEmailMessage(serverTextBox.Text, puertoTextBox.Text, loginTextBox.Text, passwordTextBox.Text, senderTextBox.Text, displaynameTextBox.Text, listTo, asuntoTextBox.Text, mensajeTextBox.Text, False, True, Nothing, sslCheckBox.Checked)
            MessageBox.Show("Enviado correctamente")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            serverTextBox.Text = ""
            puertoTextBox.Text = ""
            loginTextBox.Text = ""
            passwordTextBox.Text = ""
            senderTextBox.Text = ""
            displaynameTextBox.Text = ""
            asuntoTextBox.Text = ""
            mensajeTextBox.Text = ""
            sslCheckBox.Checked = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub loginTextBox_TextChanged(sender As Object, e As EventArgs) Handles loginTextBox.TextChanged
        senderTextBox.Text = sender.Text
    End Sub
End Class
