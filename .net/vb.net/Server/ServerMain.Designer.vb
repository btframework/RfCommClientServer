<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fmServerMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btClear = New System.Windows.Forms.Button()
        Me.lbLog = New System.Windows.Forms.ListBox()
        Me.btClose = New System.Windows.Forms.Button()
        Me.btListen = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btClear
        '
        Me.btClear.Location = New System.Drawing.Point(526, 12)
        Me.btClear.Name = "btClear"
        Me.btClear.Size = New System.Drawing.Size(75, 23)
        Me.btClear.TabIndex = 7
        Me.btClear.Text = "Clear"
        Me.btClear.UseVisualStyleBackColor = True
        '
        'lbLog
        '
        Me.lbLog.FormattingEnabled = True
        Me.lbLog.Location = New System.Drawing.Point(12, 41)
        Me.lbLog.Name = "lbLog"
        Me.lbLog.Size = New System.Drawing.Size(589, 394)
        Me.lbLog.TabIndex = 6
        '
        'btClose
        '
        Me.btClose.Location = New System.Drawing.Point(93, 12)
        Me.btClose.Name = "btClose"
        Me.btClose.Size = New System.Drawing.Size(75, 23)
        Me.btClose.TabIndex = 5
        Me.btClose.Text = "Close"
        Me.btClose.UseVisualStyleBackColor = True
        '
        'btListen
        '
        Me.btListen.Location = New System.Drawing.Point(12, 12)
        Me.btListen.Name = "btListen"
        Me.btListen.Size = New System.Drawing.Size(75, 23)
        Me.btListen.TabIndex = 4
        Me.btListen.Text = "Listen"
        Me.btListen.UseVisualStyleBackColor = True
        '
        'fmServerMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(613, 450)
        Me.Controls.Add(Me.btClear)
        Me.Controls.Add(Me.lbLog)
        Me.Controls.Add(Me.btClose)
        Me.Controls.Add(Me.btListen)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "fmServerMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RFCOMM Server Demo Application"
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents btClear As Button
    Private WithEvents lbLog As ListBox
    Private WithEvents btClose As Button
    Private WithEvents btListen As Button
End Class
