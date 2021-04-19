Public Class OCRPDF
    Private Sub OpenFileButton_Click(sender As Object, e As EventArgs) Handles OpenFileButton.Click
        ''Dim PDFOcr As New TestCore.BussinesLayer.CLS_PDF()
        ''PDFOcr.ReadTextPDF("D:\NASA_DOCS\Modulo Fiscal\Estados de Cuenta Ejemplos\EC PDF y Movimientos Qualtia Ene-Feb 2021\ECMensual - Qualtia Ene-Feb 2021\012021BNT0222189952A03.PDF")

        Dim path As String = "D:\NASA_DOCS\Modulo Fiscal\Estados de Cuenta Ejemplos\EC PDF y Movimientos Qualtia Ene-Feb 2021\ECMensual - Qualtia Ene-Feb 2021\012021BNT0617198747A03.PDF"

        Dim OCRPDF As New TestCore.BussinesLayer.CLS_OCRPDF()
        ''OCRPDF.GetTextIronOCR(path)

        Dim ocr As New TestCore.BussinesLayer.CLS_PDFOCR()

        Dim TextPDF As String = ocr.Load(path)


        Dim JSON = ocr.TextToJSON(TextPDF)
        ResultadoTextBox.Text = TextPDF

        Dim rootNode As New TreeNode("Root")
        JSONTreeView.Nodes.Add(rootNode)

        BuildTree(ocr.Deserialize(JSON), rootNode)

    End Sub

    Public Sub BuildTree(dictionary As Dictionary(Of String, Object), node As TreeNode)

        For Each item As KeyValuePair(Of String, Object) In dictionary


            Dim parentNode As New TreeNode(item.Key)
            node.Nodes.Add(parentNode)

            Try
                BuildTree(item.Value, parentNode)
                
            Catch dicE As InvalidCastException

                Try

                    Dim List As System.Collections.ArrayList = item.Value
                    For Each value As String In List

                        Dim finalNode As New TreeNode(value)
                        finalNode.ForeColor = Color.Blue
                        parentNode.Nodes.Add(finalNode)
                    Next

                Catch ex As InvalidCastException

                    Dim finalNode As New TreeNode(item.Value.ToString())
                    finalNode.ForeColor = Color.Blue
                    parentNode.Nodes.Add(finalNode)
                End Try

            End Try
            Next
    End Sub

End Class