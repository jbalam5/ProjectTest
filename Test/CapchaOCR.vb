Imports Tesseract
Imports Patagames.Ocr


Public Class CapchaOCR
    Private Sub CapchaOCR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim path As String = "C:\Users\Jesus Noh\Pictures\captcha.jpg"
            Dim pathfinal As String = "C:\Users\Jesus Noh\Pictures\captcha_fondo.jpg"

            Dim imagen As Byte() = System.IO.File.ReadAllBytes(path)
            Dim image1 As Image = Image.FromFile(path)
            Dim imagen2 As Bitmap = New Bitmap(path)



            imagen2 = Transparent2Color(imagen2, Color.White)
            imagen2.Save(pathfinal)
            Dim imagen3 As Bitmap = New Bitmap(pathfinal)

            Dim r As String = NASA.OCR.Iron_ocr.GetText(imagen3, False)
            Dim r2 As String = NASA.OCR.Iron_ocr.GetText(imagen3, True)
            Dim r3 As String = NASA.OCR.Google_vision.GetText(imagen)

            'Using engine As New Tesseract.TesseractEngine("./tessdata", "eng", Tesseract.EngineMode.Default)

            '    Using img = Tesseract.Pix.LoadFromFile(pathfinal)

            '        Using page = engine.Process(img)

            '            Dim Text = page.GetText()
            '            Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence())

            '            Console.WriteLine("Text (GetText): \r\n{0}", Text)
            '            Console.WriteLine("Text (iterator):")
            '            Using iter = page.GetIterator()

            '                iter.Begin()
            '                Do

            '                    Do

            '                        Do

            '                            Do

            '                                If (iter.IsAtBeginningOf(PageIteratorLevel.Block)) Then

            '                                    Console.WriteLine("<BLOCK>")
            '                                End If

            '                                Console.Write(iter.GetText(PageIteratorLevel.Word))
            '                                Console.Write(" ")

            '                                If (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word)) Then
            '                                    Console.WriteLine()
            '                                End If
            '                            Loop While (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word))

            '                            If (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine)) Then

            '                                Console.WriteLine()
            '                            End If
            '                        Loop While (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
            '                    Loop While (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para))
            '                Loop While (iter.Next(PageIteratorLevel.Block))

            '            End Using
            '        End Using
            '    End Using
            'End Using


            Using api = OcrApi.Create()
                Try
                    api.Init(Patagames.Ocr.Enums.Languages.English)
                    Dim plaintText = api.GetTextFromImage(pathfinal)

                    Dim resultado As String = plaintText
                Catch ex As Exception

                    Throw New Exception(ex.Message)
                End Try
            End Using

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function Transparent2Color(bmp1 As Bitmap, target As Color) As Bitmap

        Dim bmp2 As New Bitmap(bmp1.Width, bmp1.Height)
        Dim rect As New Rectangle(Point.Empty, bmp1.Size)
        Using G As Graphics = Graphics.FromImage(bmp2)
            G.Clear(target)
            G.DrawImageUnscaledAndClipped(bmp1, rect)
        End Using
        Return bmp2

    End Function


End Class