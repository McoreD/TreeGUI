Imports System.IO
Public Class frmRelNotes
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtVersionHistory As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtVersionHistory = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'txtVersionHistory
        '
        Me.txtVersionHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVersionHistory.Location = New System.Drawing.Point(16, 16)
        Me.txtVersionHistory.Multiline = True
        Me.txtVersionHistory.Name = "txtVersionHistory"
        Me.txtVersionHistory.ReadOnly = True
        Me.txtVersionHistory.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtVersionHistory.Size = New System.Drawing.Size(416, 360)
        Me.txtVersionHistory.TabIndex = 0
        Me.txtVersionHistory.TabStop = False
        Me.txtVersionHistory.Text = "TextBox1"
        '
        'frmRelNotes
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(448, 390)
        Me.Controls.Add(Me.txtVersionHistory)
        Me.Name = "frmRelNotes"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form3"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim re As New McoreSystem.ResourceExtracter
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles MyBase.Load

        Me.Text = Application.ProductName + " Release Notes"
        Dim f As New Form1
        Me.Icon = f.Icon
        Dim thisAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly
        txtVersionHistory.Text = re.GetText(thisAssembly, "ReleaseNotes.txt")
    End Sub


End Class
