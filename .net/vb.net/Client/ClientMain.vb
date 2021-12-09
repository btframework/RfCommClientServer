Public Class fmClientMain
    Private FManager As wclBluetoothManager
    Private FClient As wclRfCommClient

#Region "Helper methods."
    Private Overloads Sub Trace(str As String)
        lbLog.Items.Add(str)
    End Sub

    Private Overloads Sub Trace(str As String, [Error] As Int32)
        Trace(str + ": 0x" + [Error].ToString("X8"))
    End Sub
#End Region

#Region "Bluetooth Manager events."
    Private Sub FManager_BeforeClose(sender As Object, e As EventArgs)
        Trace("Bluetooth Manager closed")
    End Sub

    Private Sub FManager_AfterOpen(sender As Object, e As EventArgs)
        Trace("Bluetooth Manager opened.")
    End Sub

    Private Sub FManager_OnDiscoveringStarted(Sender As Object, Radio As wclBluetoothRadio)
        Trace("Discovering started")
        lvDevices.Items.Clear()
    End Sub

    Private Sub FManager_OnDeviceFound(Sender As Object, Radio As wclBluetoothRadio, Address As Int64)
        lvDevices.Items.Add(Address.ToString("X12"))
    End Sub

    Private Sub FManager_OnDiscoveringCompleted(Sender As Object, Radio As wclBluetoothRadio, [Error] As Int32)
        Trace("Discovering completed with result", [Error])
        If lvDevices.Items.Count = 0 Then
            Trace("No Bluetooth devices found")
        Else
            For Each Item As ListViewItem In lvDevices.Items
                Dim Mac As Int64 = Convert.ToInt64(Item.Text, 16)
                Dim Name As String = ""
                Dim Res As Int32 = Radio.GetRemoteName(Mac, Name)
                If Res <> wclErrors.WCL_E_SUCCESS Then
                    Name = "Error: 0x" + Res.ToString("X8")
                End If
                Item.SubItems.Add(Name)
            Next
        End If
    End Sub
#End Region

#Region "Client events."
    Private Sub FClient_OnConnect(Sender As Object, [Error] As Int32)
        If [Error] = wclErrors.WCL_E_SUCCESS Then
            Trace("Client connected")
        Else
            Trace("Connect failed", [Error])
        End If
    End Sub

    Private Sub FClient_OnDisconnect(Sender As Object, Reason As Int32)
        Trace("Client disconnected", Reason)
    End Sub

    Private Sub FClient_OnCreateProcessor(Sender As Object, Connection As wclClientDataConnection)
        Trace("Creating data processor")
        Dim Proc As ClientDataProcessor = New ClientDataProcessor(Connection)

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

        AddHandler Proc.OnError, AddressOf Proc_OnError
    End Sub

    Private Sub FClient_OnDestroyProcessor(Sender As Object, Connection As wclClientDataConnection)
        If Connection.Processor IsNot Nothing Then
            Trace("Destroy data processor")
            Connection.Processor.Dispose()
        End If
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

#Region "Error received"
    Private Sub Proc_OnError(Sender As Object, [Error] As Int32)
        Trace("Error received", [Error])
    End Sub
#End Region
#End Region

#Region "Form events."
    Private Sub fmClientMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FManager = New wclBluetoothManager()
        AddHandler FManager.AfterOpen, AddressOf FManager_AfterOpen
        AddHandler FManager.BeforeClose, AddressOf FManager_BeforeClose
        AddHandler FManager.OnDiscoveringStarted, AddressOf FManager_OnDiscoveringStarted
        AddHandler FManager.OnDeviceFound, AddressOf FManager_OnDeviceFound
        AddHandler FManager.OnDiscoveringCompleted, AddressOf FManager_OnDiscoveringCompleted

        FClient = New wclRfCommClient()
        ' Disable authentication And encryption.
        FClient.Authentication = False
        FClient.Encryption = False
        ' Use custom service's UUID.
        FClient.Service = ServiceUuid
        AddHandler FClient.OnConnect, AddressOf FClient_OnConnect
        AddHandler FClient.OnDisconnect, AddressOf FClient_OnDisconnect
        AddHandler FClient.OnCreateProcessor, AddressOf FClient_OnCreateProcessor
        AddHandler FClient.OnDestroyProcessor, AddressOf FClient_OnDestroyProcessor

        Dim Res As Int32 = FManager.Open()
        If Res <> wclErrors.WCL_E_SUCCESS Then
            Trace("Bluetooth Manager open failed", Res)
        End If
    End Sub

    Private Sub fmClientMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If FClient.State <> wclClientState.csDisconnected Then
            FClient.Disconnect()
        End If
        FClient = Nothing

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

#Region "Discovering"
    Private Sub btDiscover_Click(sender As Object, e As EventArgs) Handles btDiscover.Click
        Dim Radio As wclBluetoothRadio = Nothing
        Dim Res As Int32 = FManager.GetClassicRadio(Radio)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            Trace("Get classic radio failed", Res)
        Else
            Res = Radio.Discover(10, wclBluetoothDiscoverKind.dkClassic)
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Start classic discovering failed", Res)
            End If
        End If
    End Sub
#End Region

#Region "Connection"
    Private Sub btConnect_Click(sender As Object, e As EventArgs) Handles btConnect.Click
        If FClient.State <> wclClientState.csDisconnected Then
            Trace("Client connected or connecting")
        Else
            If lvDevices.SelectedItems Is Nothing Or lvDevices.SelectedItems.Count = 0 Then
                Trace("Select device")
            Else
                Dim Radio As wclBluetoothRadio = Nothing
                Dim Res As Int32 = FManager.GetClassicRadio(Radio)
                If Res <> wclErrors.WCL_E_SUCCESS Then
                    Trace("Get classic radio failed", Res)
                Else
                    FClient.Address = Convert.ToInt64(lvDevices.SelectedItems(0).Text, 16)
                    Res = FClient.Connect(Radio)
                    If Res <> wclErrors.WCL_E_SUCCESS Then
                        Trace("Connect failed", Res)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btDisconnect_Click(sender As Object, e As EventArgs) Handles btDisconnect.Click
        If FClient.State = wclClientState.csDisconnected Then
            Trace("Client disconnected")
        Else
            Dim Res As Int32 = FClient.Disconnect()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Disconnect failed", Res)
            End If
        End If
    End Sub
#End Region

#Region "Send data"
    Private Sub btSendByte_Click(sender As Object, e As EventArgs) Handles btSendByte.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).WriteData(CByte(&HF1))
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Write failed", Res)
            End If
        End If
    End Sub

    Private Sub btSendUInt16_Click(sender As Object, e As EventArgs) Handles btSendUInt16.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).WriteData(CUShort(&HF112))
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Write failed", Res)
            End If
        End If
    End Sub

    Private Sub btSendUInt32_Click(sender As Object, e As EventArgs) Handles btSendUInt32.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).WriteData(CUInt(&HF1121314UI))
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Write failed", Res)
            End If
        End If
    End Sub

    Private Sub btSendUInt64_Click(sender As Object, e As EventArgs) Handles btSendUInt64.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).WriteData(CULng(&HF112131415161718UL))
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Write failed", Res)
            End If
        End If
    End Sub

    Private Sub btSendSByte_Click(sender As Object, e As EventArgs) Handles btSendSByte.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).WriteData(CSByte(&HF1))
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Write failed", Res)
            End If
        End If
    End Sub

    Private Sub btSendInt16_Click(sender As Object, e As EventArgs) Handles btSendInt16.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).WriteData(CShort(&HF112))
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Write failed", Res)
            End If
        End If
    End Sub

    Private Sub btSendInt32_Click(sender As Object, e As EventArgs) Handles btSendInt32.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).WriteData(CInt(&HF1121314))
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Write failed", Res)
            End If
        End If
    End Sub

    Private Sub btSendInt64_Click(sender As Object, e As EventArgs) Handles btSendInt64.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).WriteData(CLng(&HF112131415161718L))
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Write failed", Res)
            End If
        End If
    End Sub

    Private Sub btSendArray_Click(sender As Object, e As EventArgs) Handles btSendArray.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Arr As Byte() = New Byte(511) {}
            For i As UInt16 = 0 To 511
                Arr(i) = wclHelpers.LoByte(i)
            Next
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).WriteData(Arr)
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Write failed", Res)
            End If
        End If
    End Sub

    Private Sub btSendString_Click(sender As Object, e As EventArgs) Handles btSendString.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).WriteData("Request from client")
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Write failed", Res)
            End If
        End If
    End Sub
#End Region

#Region "Get data"
    Private Sub btGetByte_Click(sender As Object, e As EventArgs) Handles btGetByte.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).GetByte()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Get failed", Res)
            End If
        End If
    End Sub

    Private Sub btGetUInt16_Click(sender As Object, e As EventArgs) Handles btGetUInt16.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).GetUInt16()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Get failed", Res)
            End If
        End If
    End Sub

    Private Sub btGetUInt32_Click(sender As Object, e As EventArgs) Handles btGetUInt32.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).GetUInt32()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Get failed", Res)
            End If
        End If
    End Sub

    Private Sub btGetUInt64_Click(sender As Object, e As EventArgs) Handles btGetUInt64.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).GetUInt64()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Get failed", Res)
            End If
        End If
    End Sub

    Private Sub btGetSByte_Click(sender As Object, e As EventArgs) Handles btGetSByte.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).GetSByte()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Get failed", Res)
            End If
        End If
    End Sub

    Private Sub btGetInt16_Click(sender As Object, e As EventArgs) Handles btGetInt16.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).GetInt16()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Get failed", Res)
            End If
        End If
    End Sub

    Private Sub btGetInt32_Click(sender As Object, e As EventArgs) Handles btGetInt32.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).GetInt32()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Get failed", Res)
            End If
        End If
    End Sub

    Private Sub btGetInt64_Click(sender As Object, e As EventArgs) Handles btGetInt64.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).GetInt64()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Get failed", Res)
            End If
        End If
    End Sub

    Private Sub btGetArray_Click(sender As Object, e As EventArgs) Handles btGetArray.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).GetArray()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Get failed", Res)
            End If
        End If
    End Sub

    Private Sub btGetString_Click(sender As Object, e As EventArgs) Handles btGetString.Click
        If FClient.Processor Is Nothing Then
            Trace("Data processor not created")
        Else
            Dim Res As Int32 = CType(FClient.Processor, ClientDataProcessor).GetString()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                Trace("Get failed", Res)
            End If
        End If
    End Sub
#End Region
#End Region
End Class
