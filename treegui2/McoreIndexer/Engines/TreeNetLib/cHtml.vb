' Version 1.0.0.0

Public Class cHtml

    Public Enum ListType
        Numbered
        Bulletted
    End Enum

    Public Function GetDocType() As String
        Return fGetText("html.txt")
    End Function

    Public Function GetValidXhtmlLine(ByVal line As String) As String

        If line IsNot Nothing Then

            line = line.Replace("&", "&amp;") 'Replace & 
            line = line.Replace("™", "&trade;") ' Replace ™
            line = line.Replace("©", "&copy;") ' Replace ©
            line = line.Replace("®", "&reg;") ' Replace ®

        End If

        Return line

    End Function


    Public Function GetJavaScript(ByVal filePath As String) As String

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<script type=""text/javascript"">")
        sb.AppendLine("// <![CDATA[")

        If System.IO.File.Exists(filePath) Then
            Using sr As New IO.StreamReader(filePath)
                sb.AppendLine(sr.ReadToEnd)
            End Using
        Else
            sb.AppendLine(fGetText("domCollapse.js"))
        End If

        sb.AppendLine("// ]]>")
        sb.AppendLine("</script>")

        Return sb.ToString

    End Function

    Public Function GetCollapseJs() As String

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<script type=""text/javascript"">")
        sb.AppendLine("// <![CDATA[")
        sb.AppendLine(fGetText("domCollapse.js"))
        sb.AppendLine("// ]]>")
        sb.AppendLine("</script>")

        Return sb.ToString

    End Function


    Public Function GetCollapseCss() As String

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<style type=""text/css"">")
        sb.AppendLine(fGetText("domCollapse.css"))
        sb.AppendLine("</style>")

        Return sb.ToString

    End Function

    Public Function GetCssStyle(ByVal filePath As String) As String

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<style type=""text/css"">")
        If System.IO.File.Exists(filePath) Then
            Using sr As New IO.StreamReader(filePath)
                sb.AppendLine(sr.ReadToEnd)
            End Using
        Else
            sb.AppendLine(fGetText("Default.css"))
        End If
        sb.AppendLine("</style>")

        Return sb.ToString

    End Function

    Public Function OpenDiv() As String
        Return "<div>"
    End Function

    Public Function CloseDiv() As String
        Return "</div>"
    End Function

    Public Function OpenBulletedList() As String        
        Return "<ul>"
    End Function

    Public Function CloseBulletedList() As String
        Return "</ul>"
    End Function

    Public Function OpenNumberedList() As String
        Return "<ol>"        
    End Function


    Public Function CloseNumberedList() As String
        Return "</ol>"
    End Function

    Public Function GetTitle(ByVal title As String) As String
        Return "<title>" + title + "</title>"
    End Function

    Public Function GetSpan(ByVal lText As String, ByVal lClass As String) As String
        Return "<span class=""" + lClass + """>" + lText + "</span>"
    End Function
    Public Function CloseHead() As String
        Return "</head>"
    End Function

    Public Function OpenBody() As String
        Return "<body>"
    End Function

    Public Function CloseBody() As String
        Return "</body></html>"
    End Function

    Public Function AddBreak() As String
        Return "<br />"
    End Function

    Public Function GetPara(ByVal msg As String) As String
        Return OpenPara() & GetValidXhtmlLine(msg) & ClosePara()
    End Function


    Public Function GetBreak() As String
        Return OpenPara() + AddBreak() + ClosePara()
    End Function

    Public Function OpenPara(Optional ByVal span As String = "") As String
        If span.Length > 0 Then
            Return String.Format("<p class=""{0}"">", span)
        Else
            Return "<p>"
        End If
    End Function

    Public Function ClosePara() As String
        Return "</p>"
    End Function

    Public Function GetWarning(ByVal msg As String) As String
        Return "<p class=""warning"">" & GetValidXhtmlLine(msg) & ClosePara()
    End Function

    Public Function fGetText(ByVal strName As String) As String

        Try
            ' get the current assembly
            Dim oAsm As System.Reflection.Assembly = _
            System.Reflection.Assembly.GetExecutingAssembly()
            Dim oStrm As IO.Stream = _
            oAsm.GetManifestResourceStream("TreeGUI." + strName)
            'System.Windows.Forms.MessageBox.Show(oAsm.GetName.Name + "." + strName)
            ' read contents of embedded file
            Dim oRdr As IO.StreamReader = New IO.StreamReader(oStrm)
            Return oRdr.ReadToEnd

        Catch ex As Exception
            Throw ex

        End Try

    End Function

    Public Function GetList(ByVal msg As String) As String
        Return "<li>" & GetValidXhtmlLine(msg) & "</li>"
    End Function

    Public Function GetHeading(ByVal msg As String, ByVal order As Integer) As String

        Return String.Format("<h{0}>{1}</h{0}>", order, GetValidXhtmlLine(msg))

    End Function

    Public Function OpenList(ByVal type As ListType) As String

        Select Case type
            Case ListType.Bulletted
                Return OpenBulletedList()
            Case ListType.Numbered
                Return OpenNumberedList()
            Case Else
                Return OpenNumberedList()
        End Select

    End Function

    Public Function CloseList(ByVal type As ListType) As String

        Select Case type
            Case ListType.Bulletted
                Return CloseBulletedList()
            Case ListType.Numbered
                Return CloseNumberedList()
            Case Else
                Return CloseNumberedList()
        End Select

    End Function


End Class
