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

        AddHandler Proc.OnByteReceived, AddressOf ByteReceived
        AddHandler Proc.OnUInt16Received, AddressOf UInt16Received
        AddHandler Proc.OnUInt32Received, AddressOf UInt32Received
        AddHandler Proc.OnUInt64Received, AddressOf UInt64Received
        AddHandler Proc.OnSByteReceived, AddressOf SByteReceived
        AddHandler Proc.OnInt16Received, AddressOf Int16Received
        AddHandler Proc.OnInt32Received, AddressOf Int32Received
        AddHandler Proc.OnInt64Received, AddressOf Int64Received
        AddHandler Proc.OnArrayReceived, AddressOf ArrayReceived
        AddHandler Proc.OnStringReceived, AddressOf StringReceived

        AddHandler Proc.OnGetByte, AddressOf GetByte
        AddHandler Proc.OnGetUInt16, AddressOf GetUInt16
        AddHandler Proc.OnGetUInt32, AddressOf GetUInt32
        AddHandler Proc.OnGetUInt64, AddressOf GetUInt64
        AddHandler Proc.OnGetSByte, AddressOf GetSByte
        AddHandler Proc.OnGetInt16, AddressOf GetInt16
        AddHandler Proc.OnGetInt32, AddressOf GetInt32
        AddHandler Proc.OnGetInt64, AddressOf GetInt64
        AddHandler Proc.OnGetArray, AddressOf GetArray
        AddHandler Proc.OnGetString, AddressOf GetString
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
    Private Sub ByteReceived(Sender As Object, Data As Byte)
        Trace("Byte received: " + Data.ToString("X2") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub UInt16Received(Sender As Object, Data As UInt16)
        Trace("UInt16 received: " + Data.ToString("X4") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub UInt32Received(Sender As Object, Data As UInt32)
        Trace("UInt32 received: " + Data.ToString("X8") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub UInt64Received(Sender As Object, Data As UInt64)
        Trace("UInt64 received: " + Data.ToString("X16") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub SByteReceived(Sender As Object, Data As SByte)
        Trace("SByte received: " + Data.ToString("X2") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Int16Received(Sender As Object, Data As Int16)
        Trace("Int16 received: " + Data.ToString("X4") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Int32Received(Sender As Object, Data As Int32)
        Trace("Int32 received: " + Data.ToString("X8") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub Int64Received(Sender As Object, Data As Int64)
        Trace("Int64 received: " + Data.ToString("X16") + " (" + Data.ToString() + ")")
    End Sub

    Private Sub ArrayReceived(Sender As Object, Data As Byte())
        Trace("Array received: " + Data.Length.ToString())
        Dim Hex As String = BitConverter.ToString(Data)
        Hex = Hex.Replace("-", "")
        lbLog.Items.Add(Hex)
    End Sub

    Private Sub StringReceived(Sender As Object, Data As String)
        Trace("String received: " + Data)
    End Sub
#End Region

#Region "Get data."
    Private Sub GetByte(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteByte(&HF5)
    End Sub

    Private Sub GetUInt16(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteUInt16(&HF551)
    End Sub

    Private Sub GetUInt32(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteUInt32(&HF5515253)
    End Sub

    Private Sub GetUInt64(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteUInt64(&HF551525354555657)
    End Sub

    Private Sub GetSByte(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteSByte(&HF5)
    End Sub

    Private Sub GetInt16(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteInt16(&HF551)
    End Sub

    Private Sub GetInt32(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteInt32(&HF5515253)
    End Sub

    Private Sub GetInt64(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteInt64(&HF551525354555657)
    End Sub

    Private Sub GetArray(Sender As Object)
        Dim Arr As Byte() = New Byte(511) {}
        For i As UInt16 = 0 To 511
            Arr(i) = wclHelpers.LoByte(i)
        Next
        Dim Res As Int32 = CType(Sender, ServerDataProcessor).WriteArray(Arr)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            Trace("Write failed", Res)
        End If
    End Sub

    Private Sub GetString(Sender As Object)
        CType(Sender, ServerDataProcessor).WriteString("Answer from server")
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
        FServer.Service = ServiceUuid
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
