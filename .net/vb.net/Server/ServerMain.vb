Public Class fmServerMain
    Private FManager As wclBluetoothManager
    Private FServer As wclRfCommServer

#Region "Helper methods."
    Private Overloads Sub Trace(str As String)
        lbLog.Items.Add(str)
    End Sub

    Private Overloads Sub Trace(str As String, [Error] As Int32)
        Trace(str + ": &H" + [Error].ToString("X8"))
    End Sub
#End Region

#Region "Bluetooth Manager events."
    Private Sub FManager_BeforeClose(sender As Object, e As EventArgs)
        Trace("Bluetooth Manager closed")
    End Sub

    Private Sub FManager_AfterOpen(sender As Object, e As EventArgs)
        Trace("Bluetooth Manager opened.")
    End Sub
#End Region

#Region "Server events."
    Private Sub FServer_OnListen(sender As Object, e As EventArgs)
        Trace("Server listening")
    End Sub

    Private Sub FServer_OnDisconnect(Sender As Object, Client As wclRfCommServerClientConnection, Reason As Int32)
        Trace("Client disconnected", Reason)
    End Sub

    Private Sub FServer_OnDestroyProcessor(Sender As Object, Connection As wclServerClientDataConnection)
        If Connection.Processor IsNot Nothing Then
            Trace("Destroy data processor")
            Connection.Processor.Dispose()
        End If
    End Sub

    Private Sub FServer_OnCreateProcessor(Sender As Object, Connection As wclServerClientDataConnection)
        Trace("Creating data processor")
        Dim Proc As ServerDataProcessor = New ServerDataProcessor(Connection)

        AddHandler Proc.OnByteReceived, AddressOf Proc_OnByteReceived
        AddHandler Proc.OnUInt16Received, AddressOf Proc_OnUInt16Received
        AddHandler Proc.OnUInt32Received, AddressOf Proc_OnUInt32Received
        AddHandler Proc.OnUInt64Received, AddressOf Proc_OnUInt64Received
        AddHandler Proc.OnSByteReceived, AddressOf Proc_OnSByteReceived
        AddHandler Proc.OnInt16Received, AddressOf Proc_OnInt16Received
        AddHandler Proc.OnInt32Received, AddressOf Proc_OnInt32Received
        AddHandler Proc.OnInt64Received, AddressOf Proc_OnInt64Received
        AddHandler Proc.OnArrayReceived, AddressOf Proc_OnArrayReceived
        AddHandler Proc.OnStringReceived, AddressOf Proc_OnStringReceived

        AddHandler Proc.OnGetByte, AddressOf Proc_OnGetByte
        AddHandler Proc.OnGetUIn16, AddressOf Proc_OnGetUIn16
        AddHandler Proc.OnGetUIn32, AddressOf Proc_OnGetUIn32
        AddHandler Proc.OnGetUInt64, AddressOf Proc_OnGetUInt64
        AddHandler Proc.OnGetSByte, AddressOf Proc_OnGetSByte
        AddHandler Proc.OnGetInt16, AddressOf Proc_OnGetInt16
        AddHandler Proc.OnGetInt32, AddressOf Proc_OnGetInt32
        AddHandler Proc.OnGetInt64, AddressOf Proc_OnGetInt64
        AddHandler Proc.OnGetArray, AddressOf Proc_OnGetArray
        AddHandler Proc.OnGetString, AddressOf Proc_OnGetString
    End Sub

    Private Sub FServer_OnConnect(Sender As Object, Client As wclRfCommServerClientConnection, [Error] As Int32)
        Trace("Client connect", [Error])
    End Sub

    Private Sub FServer_OnClosed(Sender As Object, Reason As Int32)
        Trace("Server closed")
    End Sub
#End Region

#Region "Data processor events."
#Region "Data received."
    Private Sub Proc_OnByteReceived(Sender As Object, Data As Byte)
        Trace("Byte received: " + Data.ToString("X2") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Proc_OnUInt16Received(Sender As Object, Data As UInt16)
        Trace("UInt16 received: " + Data.ToString("X4") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Proc_OnUInt32Received(Sender As Object, Data As UInt32)
        Trace("UInt32 received: " + Data.ToString("X8") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Proc_OnUInt64Received(Sender As Object, Data As UInt64)
        Trace("UInt64 received: " + Data.ToString("X16") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Proc_OnSByteReceived(Sender As Object, Data As SByte)
        Trace("SByte received: " + Data.ToString("X2") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Proc_OnInt16Received(Sender As Object, Data As Int16)
        Trace("Int16 received: " + Data.ToString("X4") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Proc_OnInt32Received(Sender As Object, Data As Int32)
        Trace("Int32 received: " + Data.ToString("X8") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Proc_OnInt64Received(Sender As Object, Data As Int64)
        Trace("Int64 received: " + Data.ToString("X16") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Proc_OnArrayReceived(Sender As Object, Data As Byte())
        Trace("Array received: " + Data.Length.ToString())
        Dim Hex As String = BitConverter.ToString(Data)
        Hex = Hex.Replace("-", "")
        lbLog.Items.Add(Hex)
    End Sub

    Private Sub Proc_OnStringReceived(Sender As Object, Data As String)
        Trace("String received: " + Data)
    End Sub
#End Region

#Region "Get data."
    Private Sub Proc_OnGetByte(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteData(CByte(&HF5))
    End Sub

    Private Sub Proc_OnGetUIn16(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteData(CUShort(&HF551))
    End Sub

    Private Sub Proc_OnGetUIn32(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteData(CUInt(&HF5515253))
    End Sub

    Private Sub Proc_OnGetUInt64(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteData(CULng(&HF551525354555657))
    End Sub

    Private Sub Proc_OnGetSByte(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteData(CSByte(&HF5))
    End Sub

    Private Sub Proc_OnGetInt16(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteData(CShort(&HF551))
    End Sub

    Private Sub Proc_OnGetInt32(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteData(CInt(&HF5515253))
    End Sub

    Private Sub Proc_OnGetInt64(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteData(CLng(&HF551525354555657))
    End Sub

    Private Sub Proc_OnGetArray(Sender As Object)
        Dim Arr As Byte() = New Byte(511) {}
        For i As UInt16 = 0 To 511
            Arr(i) = wclHelpers.LoByte(i)
        Next
        Dim Res As Int32 = CType(Sender, ServerDataProcessor).WriteData(Arr)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            Trace("Write failed", Res)
        End If
    End Sub

    Private Sub Proc_OnGetString(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteData("Answer from server")
    End Sub
#End Region
#End Region

#Region "Form events."
    Private Sub fmServerMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FManager = New wclBluetoothManager()
        AddHandler FManager.AfterOpen, AddressOf FManager_AfterOpen
        AddHandler FManager.BeforeClose, AddressOf FManager_BeforeClose

        FServer = New wclRfCommServer()
        ' Disable authentication And encryption.
        FServer.Authentication = False
        FServer.Encryption = False
        ' Use custom service's UUID.
        FServer.Service = New Guid("{CA80C97C-06B3-4E65-9CEE-65BB0B11BC92}")
        AddHandler FServer.OnClosed, AddressOf FServer_OnClosed
        AddHandler FServer.OnConnect, AddressOf FServer_OnConnect
        AddHandler FServer.OnCreateProcessor, AddressOf FServer_OnCreateProcessor
        AddHandler FServer.OnDestroyProcessor, AddressOf FServer_OnDestroyProcessor
        AddHandler FServer.OnDisconnect, AddressOf FServer_OnDisconnect
        AddHandler FServer.OnListen, AddressOf FServer_OnListen

        Dim Res As Int32 = FManager.Open()
        If Res <> wclErrors.WCL_E_SUCCESS Then
            Trace("Bluetooth Manager open failed", Res)
        End If
    End Sub

    Private Sub fmServerMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If FServer.State <> wclCommunication.wclServerState.ssClosed Then
            FServer.Close()
        End If
        FServer = Nothing

        If FManager.Active Then
            FManager.Close()
        End If
        FManager = Nothing
    End Sub
#End Region

#Region "Buttons events."
#Region "Clear"
    Private Sub btClear_Click(sender As Object, e As EventArgs) Handles btClear.Click
        lbLog.Items.Clear()
    End Sub
#End Region

#Region "Server buttons"
    Private Sub btListen_Click(sender As Object, e As EventArgs) Handles btListen.Click
        Dim Radio As wclBluetoothRadio = Nothing
        Dim Res As Int32 = FManager.GetClassicRadio(Radio)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            Trace("Get classic radio failed", Res)
        Else
            ' Do Not forget to switch to connectable And discoverable mode!
            Res = Radio.SetConnectable(True)
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Set connectable failed", Res)
            End If
            Res = Radio.SetDiscoverable(True)
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Set discoverable failed", Res)
            End If
            Res = FServer.Listen(Radio)
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Listen failed", Res)
            End If
        End If
    End Sub

    Private Sub btClose_Click(sender As Object, e As EventArgs) Handles btClose.Click
        Dim Res As Int32 = FServer.Close()
        If Res <> wclErrors.WCL_E_SUCCESS Then
            Trace("Close failed", Res)
        End If
    End Sub
#End Region
#End Region
End Class
