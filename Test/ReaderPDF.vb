Public Class ReaderPDF
    Private Sub OpenButton_Click(sender As Object, e As EventArgs) Handles OpenButton.Click
        Try
            Dim openfile As New System.Windows.Forms.OpenFileDialog
            If openfile.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim ReaderPDF = New TestCore.BussinesLayer.CLS_PDF()
                Dim _AllText = ReaderPDF.GetText_1(openfile.FileName)
                Dim __AllText = ReaderPDF.GetText_2(openfile.FileName)
                Dim AllText = ReaderPDF.getText(openfile.FileName)
                DeclaracionDataGridView.DataSource = ReaderPDF.GetData(AllText)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class