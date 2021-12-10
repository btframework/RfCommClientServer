<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fmClientMain
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
        Me.btGetString = New System.Windows.Forms.Button()
        Me.btSendString = New System.Windows.Forms.Button()
        Me.btGetArray = New System.Windows.Forms.Button()
        Me.btGetInt64 = New System.Windows.Forms.Button()
        Me.btGetInt32 = New System.Windows.Forms.Button()
        Me.btGetInt16 = New System.Windows.Forms.Button()
        Me.btGetSByte = New System.Windows.Forms.Button()
        Me.btGetUInt64 = New System.Windows.Forms.Button()
        Me.btGetUInt32 = New System.Windows.Forms.Button()
        Me.btGetUInt16 = New System.Windows.Forms.Button()
        Me.btGetByte = New System.Windows.Forms.Button()
        Me.btSendArray = New System.Windows.Forms.Button()
        Me.btSendInt64 = New System.Windows.Forms.Button()
        Me.btSendInt32 = New System.Windows.Forms.Button()
        Me.btSendInt16 = New System.Windows.Forms.Button()
        Me.btSendUInt64 = New System.Windows.Forms.Button()
        Me.btSendUInt32 = New System.Windows.Forms.Button()
        Me.btSendUInt16 = New System.Windows.Forms.Button()
        Me.btSendByte = New System.Windows.Forms.Button()
        Me.btClear = New System.Windows.Forms.Button()
        Me.lbLog = New System.Windows.Forms.ListBox()
        Me.btDisconnect = New System.Windows.Forms.Button()
        Me.btConnect = New System.Windows.Forms.Button()
        Me.chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAddress = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btSendSByte = New System.Windows.Forms.Button()
        Me.lvDevices = New System.Windows.Forms.ListView()
        Me.btDiscover = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btGetString
        '
        Me.btGetString.Location = New System.Drawing.Point(350, 250)
        Me.btGetString.Name = "btGetString"
        Me.btGetString.Size = New System.Drawing.Size(75, 23)
        Me.btGetString.TabIndex = 51
        Me.btGetString.Text = "Get String"
        Me.btGetString.UseVisualStyleBackColor = True
        '
        'btSendString
        '
        Me.btSendString.Location = New System.Drawing.Point(350, 181)
        Me.btSendString.Name = "btSendString"
        Me.btSendString.Size = New System.Drawing.Size(75, 23)
        Me.btSendString.TabIndex = 50
        Me.btSendString.Text = "Send String"
        Me.btSendString.UseVisualStyleBackColor = True
        '
        'btGetArray
        '
        Me.btGetArray.Location = New System.Drawing.Point(350, 221)
        Me.btGetArray.Name = "btGetArray"
        Me.btGetArray.Size = New System.Drawing.Size(75, 23)
        Me.btGetArray.TabIndex = 49
        Me.btGetArray.Text = "Get Array"
        Me.btGetArray.UseVisualStyleBackColor = True
        '
        'btGetInt64
        '
        Me.btGetInt64.Location = New System.Drawing.Point(255, 250)
        Me.btGetInt64.Name = "btGetInt64"
        Me.btGetInt64.Size = New System.Drawing.Size(75, 23)
        Me.btGetInt64.TabIndex = 48
        Me.btGetInt64.Text = "Get Int64"
        Me.btGetInt64.UseVisualStyleBackColor = True
        '
        'btGetInt32
        '
        Me.btGetInt32.Location = New System.Drawing.Point(174, 250)
        Me.btGetInt32.Name = "btGetInt32"
        Me.btGetInt32.Size = New System.Drawing.Size(75, 23)
        Me.btGetInt32.TabIndex = 47
        Me.btGetInt32.Text = "Get Int32"
        Me.btGetInt32.UseVisualStyleBackColor = True
        '
        'btGetInt16
        '
        Me.btGetInt16.Location = New System.Drawing.Point(93, 250)
        Me.btGetInt16.Name = "btGetInt16"
        Me.btGetInt16.Size = New System.Drawing.Size(75, 23)
        Me.btGetInt16.TabIndex = 46
        Me.btGetInt16.Text = "Get Int16"
        Me.btGetInt16.UseVisualStyleBackColor = True
        '
        'btGetSByte
        '
        Me.btGetSByte.Location = New System.Drawing.Point(10, 250)
        Me.btGetSByte.Name = "btGetSByte"
        Me.btGetSByte.Size = New System.Drawing.Size(75, 23)
        Me.btGetSByte.TabIndex = 45
        Me.btGetSByte.Text = "Get SByte"
        Me.btGetSByte.UseVisualStyleBackColor = True
        '
        'btGetUInt64
        '
        Me.btGetUInt64.Location = New System.Drawing.Point(255, 221)
        Me.btGetUInt64.Name = "btGetUInt64"
        Me.btGetUInt64.Size = New System.Drawing.Size(75, 23)
        Me.btGetUInt64.TabIndex = 44
        Me.btGetUInt64.Text = "Get UInt64"
        Me.btGetUInt64.UseVisualStyleBackColor = True
        '
        'btGetUInt32
        '
        Me.btGetUInt32.Location = New System.Drawing.Point(174, 221)
        Me.btGetUInt32.Name = "btGetUInt32"
        Me.btGetUInt32.Size = New System.Drawing.Size(75, 23)
        Me.btGetUInt32.TabIndex = 43
        Me.btGetUInt32.Text = "Get UInt32"
        Me.btGetUInt32.UseVisualStyleBackColor = True
        '
        'btGetUInt16
        '
        Me.btGetUInt16.Location = New System.Drawing.Point(93, 221)
        Me.btGetUInt16.Name = "btGetUInt16"
        Me.btGetUInt16.Size = New System.Drawing.Size(75, 23)
        Me.btGetUInt16.TabIndex = 42
        Me.btGetUInt16.Text = "Get UInt16"
        Me.btGetUInt16.UseVisualStyleBackColor = True
        '
        'btGetByte
        '
        Me.btGetByte.Location = New System.Drawing.Point(10, 221)
        Me.btGetByte.Name = "btGetByte"
        Me.btGetByte.Size = New System.Drawing.Size(75, 23)
        Me.btGetByte.TabIndex = 41
        Me.btGetByte.Text = "Get Byte"
        Me.btGetByte.UseVisualStyleBackColor = True
        '
        'btSendArray
        '
        Me.btSendArray.Location = New System.Drawing.Point(350, 152)
        Me.btSendArray.Name = "btSendArray"
        Me.btSendArray.Size = New System.Drawing.Size(75, 23)
        Me.btSendArray.TabIndex = 40
        Me.btSendArray.Text = "Send Array"
        Me.btSendArray.UseVisualStyleBackColor = True
        '
        'btSendInt64
        '
        Me.btSendInt64.Location = New System.Drawing.Point(255, 181)
        Me.btSendInt64.Name = "btSendInt64"
        Me.btSendInt64.Size = New System.Drawing.Size(75, 23)
        Me.btSendInt64.TabIndex = 39
        Me.btSendInt64.Text = "Send Int64"
        Me.btSendInt64.UseVisualStyleBackColor = True
        '
        'btSendInt32
        '
        Me.btSendInt32.Location = New System.Drawing.Point(174, 181)
        Me.btSendInt32.Name = "btSendInt32"
        Me.btSendInt32.Size = New System.Drawing.Size(75, 23)
        Me.btSendInt32.TabIndex = 38
        Me.btSendInt32.Text = "Send Int32"
        Me.btSendInt32.UseVisualStyleBackColor = True
        '
        'btSendInt16
        '
        Me.btSendInt16.Location = New System.Drawing.Point(93, 181)
        Me.btSendInt16.Name = "btSendInt16"
        Me.btSendInt16.Size = New System.Drawing.Size(75, 23)
        Me.btSendInt16.TabIndex = 37
        Me.btSendInt16.Text = "Send Int16"
        Me.btSendInt16.UseVisualStyleBackColor = True
        '
        'btSendUInt64
        '
        Me.btSendUInt64.Location = New System.Drawing.Point(255, 152)
        Me.btSendUInt64.Name = "btSendUInt64"
        Me.btSendUInt64.Size = New System.Drawing.Size(75, 23)
        Me.btSendUInt64.TabIndex = 35
        Me.btSendUInt64.Text = "Send UInt64"
        Me.btSendUInt64.UseVisualStyleBackColor = True
        '
        'btSendUInt32
        '
        Me.btSendUInt32.Location = New System.Drawing.Point(174, 152)
        Me.btSendUInt32.Name = "btSendUInt32"
        Me.btSendUInt32.Size = New System.Drawing.Size(75, 23)
        Me.btSendUInt32.TabIndex = 34
        Me.btSendUInt32.Text = "Send UInt32"
        Me.btSendUInt32.UseVisualStyleBackColor = True
        '
        'btSendUInt16
        '
        Me.btSendUInt16.Location = New System.Drawing.Point(93, 152)
        Me.btSendUInt16.Name = "btSendUInt16"
        Me.btSendUInt16.Size = New System.Drawing.Size(75, 23)
        Me.btSendUInt16.TabIndex = 33
        Me.btSendUInt16.Text = "Send UInt16"
        Me.btSendUInt16.UseVisualStyleBackColor = True
        '
        'btSendByte
        '
        Me.btSendByte.Location = New System.Drawing.Point(12, 152)
        Me.btSendByte.Name = "btSendByte"
        Me.btSendByte.Size = New System.Drawing.Size(75, 23)
        Me.btSendByte.TabIndex = 32
        Me.btSendByte.Text = "Send Byte"
        Me.btSendByte.UseVisualStyleBackColor = True
        '
        'btClear
        '
        Me.btClear.Location = New System.Drawing.Point(350, 290)
        Me.btClear.Name = "btClear"
        Me.btClear.Size = New System.Drawing.Size(75, 23)
        Me.btClear.TabIndex = 31
        Me.btClear.Text = "Clear"
        Me.btClear.UseVisualStyleBackColor = True
        '
        'lbLog
        '
        Me.lbLog.FormattingEnabled = True
        Me.lbLog.Location = New System.Drawing.Point(10, 316)
        Me.lbLog.Name = "lbLog"
        Me.lbLog.Size = New System.Drawing.Size(413, 173)
        Me.lbLog.TabIndex = 30
        '
        'btDisconnect
        '
        Me.btDisconnect.Location = New System.Drawing.Point(350, 70)
        Me.btDisconnect.Name = "btDisconnect"
        Me.btDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.btDisconnect.TabIndex = 29
        Me.btDisconnect.Text = "Disconnect"
        Me.btDisconnect.UseVisualStyleBackColor = True
        '
        'btConnect
        '
        Me.btConnect.Location = New System.Drawing.Point(350, 41)
        Me.btConnect.Name = "btConnect"
        Me.btConnect.Size = New System.Drawing.Size(75, 23)
        Me.btConnect.TabIndex = 28
        Me.btConnect.Text = "Connect"
        Me.btConnect.UseVisualStyleBackColor = True
        '
        'chName
        '
        Me.chName.Text = "Name"
        Me.chName.Width = 150
        '
        'chAddress
        '
        Me.chAddress.Text = "Address"
        Me.chAddress.Width = 150
        '
        'btSendSByte
        '
        Me.btSendSByte.Location = New System.Drawing.Point(12, 181)
        Me.btSendSByte.Name = "btSendSByte"
        Me.btSendSByte.Size = New System.Drawing.Size(75, 23)
        Me.btSendSByte.TabIndex = 36
        Me.btSendSByte.Text = "Send SByte"
        Me.btSendSByte.UseVisualStyleBackColor = True
        '
        'lvDevices
        '
        Me.lvDevices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chAddress, Me.chName})
        Me.lvDevices.FullRowSelect = True
        Me.lvDevices.GridLines = True
        Me.lvDevices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvDevices.HideSelection = False
        Me.lvDevices.Location = New System.Drawing.Point(12, 41)
        Me.lvDevices.MultiSelect = False
        Me.lvDevices.Name = "lvDevices"
        Me.lvDevices.Size = New System.Drawing.Size(332, 105)
        Me.lvDevices.TabIndex = 27
        Me.lvDevices.UseCompatibleStateImageBehavior = False
        Me.lvDevices.View = System.Windows.Forms.View.Details
        '
        'btDiscover
        '
        Me.btDiscover.Location = New System.Drawing.Point(12, 12)
        Me.btDiscover.Name = "btDiscover"
        Me.btDiscover.Size = New System.Drawing.Size(75, 23)
        Me.btDiscover.TabIndex = 26
        Me.btDiscover.Text = "Discover"
        Me.btDiscover.UseVisualStyleBackColor = True
        '
        'fmClientMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(437, 501)
        Me.Controls.Add(Me.btGetString)
        Me.Controls.Add(Me.btSendString)
        Me.Controls.Add(Me.btGetArray)
        Me.Controls.Add(Me.btGetInt64)
        Me.Controls.Add(Me.btGetInt32)
        Me.Controls.Add(Me.btGetInt16)
        Me.Controls.Add(Me.btGetSByte)
        Me.Controls.Add(Me.btGetUInt64)
        Me.Controls.Add(Me.btGetUInt32)
        Me.Controls.Add(Me.btGetUInt16)
        Me.Controls.Add(Me.btGetByte)
        Me.Controls.Add(Me.btSendArray)
        Me.Controls.Add(Me.btSendInt64)
        Me.Controls.Add(Me.btSendInt32)
        Me.Controls.Add(Me.btSendInt16)
        Me.Controls.Add(Me.btSendUInt64)
        Me.Controls.Add(Me.btSendUInt32)
        Me.Controls.Add(Me.btSendUInt16)
        Me.Controls.Add(Me.btSendByte)
        Me.Controls.Add(Me.btClear)
        Me.Controls.Add(Me.lbLog)
        Me.Controls.Add(Me.btDisconnect)
        Me.Controls.Add(Me.btConnect)
        Me.Controls.Add(Me.btSendSByte)
        Me.Controls.Add(Me.lvDevices)
        Me.Controls.Add(Me.btDiscover)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "fmClientMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RFCOMM Client Demo Application"
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents btGetString As Button
    Private WithEvents btSendString As Button
    Private WithEvents btGetArray As Button
    Private WithEvents btGetInt64 As Button
    Private WithEvents btGetInt32 As Button
    Private WithEvents btGetInt16 As Button
    Private WithEvents btGetSByte As Button
    Private WithEvents btGetUInt64 As Button
    Private WithEvents btGetUInt32 As Button
    Private WithEvents btGetUInt16 As Button
    Private WithEvents btGetByte As Button
    Private WithEvents btSendArray As Button
    Private WithEvents btSendInt64 As Button
    Private WithEvents btSendInt32 As Button
    Private WithEvents btSendInt16 As Button
    Private WithEvents btSendUInt64 As Button
    Private WithEvents btSendUInt32 As Button
    Private WithEvents btSendUInt16 As Button
    Private WithEvents btSendByte As Button
    Private WithEvents btClear As Button
    Private WithEvents lbLog As ListBox
    Private WithEvents btDisconnect As Button
    Private WithEvents btConnect As Button
    Private WithEvents chName As ColumnHeader
    Private WithEvents chAddress As ColumnHeader
    Private WithEvents btSendSByte As Button
    Private WithEvents lvDevices As ListView
    Private WithEvents btDiscover As Button
End Class
