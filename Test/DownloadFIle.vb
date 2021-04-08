Imports System.IO
Imports System.Net

Public Class DownloadFIle
    Private Sub DownloadButton_Click(sender As Object, e As EventArgs) Handles DownloadButton.Click
        Try
            Dim client As New WebClient()

            client.DownloadFile(URLFileTextBox.Text, FileNameTextBox.Text)

            Dim PDFFile As New FileInfo(FileNameTextBox.Text)
            Console.WriteLine(PDFFile.CreationTime)

        Catch ex As Exception

        End Try
    End Sub
End Class