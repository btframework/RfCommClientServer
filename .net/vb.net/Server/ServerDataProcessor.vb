Friend Class ServerDataProcessor
    Inherits wclCustomServerClientDataProcessor

    Private FDecoder As CommandDecoder

#Region "Data decoder events."
    Private Sub GetArray(Sender As Object)
        If OnGetArrayEvent IsNot Nothing Then
            RaiseEvent OnGetArray(Me)
        End If
    End Sub

    Private Sub GetInt64(Sender As Object)
        If OnGetInt64Event IsNot Nothing Then
            RaiseEvent OnGetInt64(Me)
        End If
    End Sub

    Private Sub GetInt32(Sender As Object)
        If OnGetInt32Event IsNot Nothing Then
            RaiseEvent OnGetInt32(Me)
        End If
    End Sub

    Private Sub GetInt16(Sender As Object)
        If OnGetInt16Event IsNot Nothing Then
            RaiseEvent OnGetInt16(Me)
        End If
    End Sub

    Private Sub GetSByte(Sender As Object)
        If OnGetSByteEvent IsNot Nothing Then
            RaiseEvent OnGetSByte(Me)
        End If
    End Sub

    Private Sub GetUInt64(Sender As Object)
        If OnGetUInt64Event IsNot Nothing Then
            RaiseEvent OnGetUInt64(Me)
        End If
    End Sub

    Private Sub GetUInt32(Sender As Object)
        If OnGetUInt32Event IsNot Nothing Then
            RaiseEvent OnGetUInt32(Me)
        End If
    End Sub

    Private Sub GetUInt16(Sender As Object)
        If OnGetUInt16Event IsNot Nothing Then
            RaiseEvent OnGetUInt16(Me)
        End If
    End Sub

    Private Sub GetByte(Sender As Object)
        If OnGetByteEvent IsNot Nothing Then
            RaiseEvent OnGetByte(Me)
        End If
    End Sub

    Private Sub GetString(Sender As Object)
        If OnGetStringEvent IsNot Nothing Then
            RaiseEvent OnGetString(Me)
        End If
    End Sub

    Private Sub ArrayReceived(Sender As Object, Data As Byte())
        If OnArrayReceivedEvent IsNot Nothing Then
            RaiseEvent OnArrayReceived(Me, Data)
        End If
    End Sub

    Private Sub Int64Received(Sender As Object, Data As Int64)
        If OnInt64ReceivedEvent IsNot Nothing Then
            RaiseEvent OnInt64Received(Me, Data)
        End If
    End Sub

    Private Sub Int32Received(Sender As Object, Data As Int32)
        If OnInt32ReceivedEvent IsNot Nothing Then
            RaiseEvent OnInt32Received(Me, Data)
        End If
    End Sub

    Private Sub Int16Received(Sender As Object, Data As Int16)
        If OnInt16ReceivedEvent IsNot Nothing Then
            RaiseEvent OnInt16Received(Me, Data)
        End If
    End Sub

    Private Sub SByteReceived(Sender As Object, Data As SByte)
        If OnSByteReceivedEvent IsNot Nothing Then
            RaiseEvent OnSByteReceived(Me, Data)
        End If
    End Sub

    Private Sub UInt64Received(Sender As Object, Data As UInt64)
        If OnUInt64ReceivedEvent IsNot Nothing Then
            RaiseEvent OnUInt64Received(Me, Data)
        End If
    End Sub

    Private Sub UInt32Received(Sender As Object, Data As UInt32)
        If OnUInt32ReceivedEvent IsNot Nothing Then
            RaiseEvent OnUInt32Received(Me, Data)
        End If
    End Sub

    Private Sub UInt16Received(Sender As Object, Data As UInt16)
        If OnUInt16ReceivedEvent IsNot Nothing Then
            RaiseEvent OnUInt16Received(Me, Data)
        End If
    End Sub

    Private Sub ByteReceived(Sender As Object, Data As Byte)
        If OnByteReceivedEvent IsNot Nothing Then
            RaiseEvent OnByteReceived(Me, Data)
        End If
    End Sub

    Private Sub StringReceived(Sender As Object, Data As String)
        If OnStringReceivedEvent IsNot Nothing Then
            RaiseEvent OnStringReceived(Me, Data)
        End If
    End Sub
#End Region

    Protected Overrides Sub ProcessData(Data As Byte())
        FDecoder.ProcessData(Data)
    End Sub

    Public Sub New(Connection As wclServerClientDataConnection)
        MyBase.New(Connection)

        FDecoder = New CommandDecoder()

        AddHandler FDecoder.OnByteReceived, AddressOf ByteReceived
        AddHandler FDecoder.OnUInt16Received, AddressOf UInt16Received
        AddHandler FDecoder.OnUInt32Received, AddressOf UInt32Received
        AddHandler FDecoder.OnUInt64Received, AddressOf UInt64Received
        AddHandler FDecoder.OnSByteReceived, AddressOf SByteReceived
        AddHandler FDecoder.OnInt16Received, AddressOf Int16Received
        AddHandler FDecoder.OnInt32Received, AddressOf Int32Received
        AddHandler FDecoder.OnInt64Received, AddressOf Int64Received
        AddHandler FDecoder.OnArrayReceived, AddressOf ArrayReceived
        AddHandler FDecoder.OnStringReceived, AddressOf StringReceived

        AddHandler FDecoder.OnGetByte, AddressOf GetByte
        AddHandler FDecoder.OnGetUInt16, AddressOf GetUInt16
        AddHandler FDecoder.OnGetUInt32, AddressOf GetUInt32
        AddHandler FDecoder.OnGetUInt64, AddressOf GetUInt64
        AddHandler FDecoder.OnGetSByte, AddressOf GetSByte
        AddHandler FDecoder.OnGetInt16, AddressOf GetInt16
        AddHandler FDecoder.OnGetInt32, AddressOf GetInt32
        AddHandler FDecoder.OnGetInt64, AddressOf GetInt64
        AddHandler FDecoder.OnGetArray, AddressOf GetArray
        AddHandler FDecoder.OnGetString, AddressOf GetString

        OnByteReceivedEvent = Nothing
        OnUInt16ReceivedEvent = Nothing
        OnUInt32ReceivedEvent = Nothing
        OnUInt64ReceivedEvent = Nothing
        OnSByteReceivedEvent = Nothing
        OnInt16ReceivedEvent = Nothing
        OnInt32ReceivedEvent = Nothing
        OnInt64ReceivedEvent = Nothing
        OnArrayReceivedEvent = Nothing
        OnStringReceivedEvent = Nothing

        OnGetByteEvent = Nothing
        OnGetUInt16Event = Nothing
        OnGetUInt32Event = Nothing
        OnGetUInt64Event = Nothing
        OnGetSByteEvent = Nothing
        OnGetInt16Event = Nothing
        OnGetInt32Event = Nothing
        OnGetInt64Event = Nothing
        OnGetArrayEvent = Nothing
        OnGetStringEvent = Nothing
    End Sub

#Region "Write data"
    Public Function WriteByte(Data As Byte) As Int32
        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Function WriteSByte(Data As SByte) As Int32
        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Function WriteUInt16(Data As UInt16) As Int32
        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Function WriteInt16(Data As Int16) As Int32
        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Function WriteUInt32(Data As UInt32) As Int32
        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Function WriteInt32(Data As Int32) As Int32
        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Function WriteUInt64(Data As UInt64) As Int32
        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Function WriteInt64(Data As Int64) As Int32
        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Function WriteArray(Data As Byte()) As Int32
        If Data Is Nothing Or Data.Length = 0 Or CUInt(Data.Length) > UInt16.MaxValue - 3 Then
            Return wclErrors.WCL_E_INVALID_ARGUMENT
        End If

        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Function WriteString(Data As String) As Int32
        If Data Is Nothing Or Data.Length = 0 Or CUInt(Data.Length) > UInt16.MaxValue - 3 Then
            Return wclErrors.WCL_E_INVALID_ARGUMENT
        End If

        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Function WriteError([Error] As Int32) As Int32
        Return Write(CommandBuilder.CreateError([Error]))
    End Function
#End Region

#Region "Events."
#Region "Received events."
    Public Event OnByteReceived As ByteReceived
    Public Event OnUInt16Received As UInt16Received
    Public Event OnUInt32Received As UInt32Received
    Public Event OnUInt64Received As UInt64Received
    Public Event OnSByteReceived As SByteReceived
    Public Event OnInt16Received As Int16Received
    Public Event OnInt32Received As Int32Received
    Public Event OnInt64Received As Int64Received
    Public Event OnArrayReceived As ArrayReceived
    Public Event OnStringReceived As StringReceived
#End Region

#Region "Get events."
    Public Event OnGetByte As DataEvent
    Public Event OnGetUInt16 As DataEvent
    Public Event OnGetUInt32 As DataEvent
    Public Event OnGetUInt64 As DataEvent
    Public Event OnGetSByte As DataEvent
    Public Event OnGetInt16 As DataEvent
    Public Event OnGetInt32 As DataEvent
    Public Event OnGetInt64 As DataEvent
    Public Event OnGetArray As DataEvent
    Public Event OnGetString As DataEvent
#End Region
#End Region
End Class