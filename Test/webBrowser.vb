Public Class webBrowser
    Dim wb As TestCore.BussinesLayer.CLS_webBrowser

    Private Async Sub webBrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        wb = New TestCore.BussinesLayer.CLS_webBrowser(Nothing)
        wb.webBrowser.Url = New Uri("https://www.sat.gob.mx/home")
        ''wb.Navigate("https://www.sat.gob.mx/home")

        Await Task.Delay(10000)

        Do While wb.isLoadComplete
            WebBrowser1 = wb.webBrowser
            WebBrowser1.Refresh()
            Application.DoEvents()
            wb.isLoadComplete = False
        Loop

    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs)
        wb.Navigate("google.com")

        Await Task.Delay(10000)

        Do While wb.isLoadComplete
            WebBrowser1 = wb.webBrowser
            wb.isLoadComplete = False
        Loop

    End Sub
End Class