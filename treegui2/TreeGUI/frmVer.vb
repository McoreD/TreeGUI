Public Class frmVer

    Private Sub frmVer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Icon = My.Forms.frmForm1.Icon

        If My.Forms.frmForm1.TopMost = True Then
            My.Forms.frmForm1.ToggleFormAlwaysOnTop()
        End If

        Me.Text = Application.ProductName + " Version History"

        txtVer.Text = GetText("VersionHistory.txt")

    End Sub



End Class