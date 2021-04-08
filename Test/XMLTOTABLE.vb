Public Class XMLTOTABLE
    Private Sub OpenButton_Click(sender As Object, e As EventArgs) Handles OpenButton.Click
        Dim openfile As New System.Windows.Forms.OpenFileDialog

        If openfile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim CLSXML As New TestCore.BussinesLayer.CLS_XML()

            Dim ds As DataSet = CLSXML.getTable(openfile.FileName)
            xmlEncDataGridView.DataSource = ds.Tables(0)
            xmlDataGridView.DataSource = ds.Tables(1)

            CLSXML.SaveToTxt(ds, openfile.FileName)
            ''CLSXML.SaveToTxtCatalogos(ds, openfile.FileName)
            'saveToTxt(ds, openfile.FileName)
            'saveCTToTxt(ds, openfile.FileName)
        End If

    End Sub
End Class